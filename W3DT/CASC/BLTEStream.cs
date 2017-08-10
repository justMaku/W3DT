using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using W3DT.Hashing.MD5;

namespace W3DT.CASC
{
    class BLTEDecoderException : Exception
    {
        public BLTEDecoderException(string message)
            : base(message)
        {
        }

        public BLTEDecoderException(string fmt, params object[] args)
            : base(string.Format(fmt, args))
        {
        }
    }

    class DataBlock
    {
        public int CompSize;
        public int DecompSize;
        public MD5Hash Hash;
        public byte[] Data;
    }

    class BLTEStream : Stream
    {
        private BinaryReader reader;
        private MD5 hash = MD5.Create();
        private MemoryStream streamMemory;
        private DataBlock[] dataBlocks;
        private Stream stream;

        private int blocksIndex;
        private long length;

        private const byte ENCRYPTION_SALSA20 = 0x53;
        private const byte ENCRYPTION_ARC4 = 0x41;
        private const int BLTE_MAGIC = 0x45544c42;

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => false;
        public override long Length => length;

        public override long Position
        {
            get { return streamMemory.Position; }
            set
            {
                while (value > streamMemory.Length)
                    if (!ProcessNextBlock())
                        break;

                streamMemory.Position = value;
            }
        }

        public BLTEStream(Stream source, MD5Hash hash)
        {
            stream = source;
            reader = new BinaryReader(source);

            Parse(hash);
        }

        private void Parse(MD5Hash hash)
        {
            int size = (int)reader.BaseStream.Length;

            if (size < 8)
                throw new BLTEDecoderException("Invalid data length: {0}", 8);

            int magic = reader.ReadInt32();
            if (magic != BLTE_MAGIC)
                throw new BLTEDecoderException("Mis-matched header (magic)");

            int headerSize = reader.ReadInt32BE();
            if (CASCConfig.ValidateData)
            {
                long oldPos = reader.BaseStream.Position;
                reader.BaseStream.Position = 0;

                byte[] newHash = this.hash.ComputeHash(reader.ReadBytes(headerSize > 0 ? headerSize : size));
                if (!hash.EqualsTo(newHash))
                    throw new BLTEDecoderException("Corrupted data?");

                reader.BaseStream.Position = oldPos;
            }

            int numBlocks = 1;
            if (headerSize > 0)
            {
                if (size < 12)
                    throw new BLTEDecoderException("Not enough data: {0}", 12);

                byte[] bytes = reader.ReadBytes(4);
                numBlocks = bytes[1] << 16 | bytes[2] << 8 | bytes[3] << 0;

                if (bytes[0] != 0x0F || numBlocks == 0)
                    throw new BLTEDecoderException("Invalid table format 0x{0:x2}, numBlocks {1}", bytes[0], numBlocks);

                int frameHeaderSize = 24 * numBlocks + 12;
                if (headerSize != frameHeaderSize)
                    throw new BLTEDecoderException("Header size mis-match");

                if (size < frameHeaderSize)
                    throw new BLTEDecoderException("Not enough data: {0}", frameHeaderSize);
            }

            dataBlocks = new DataBlock[numBlocks];
            for (int i = 0; i < numBlocks; i++)
            {
                DataBlock block = new DataBlock();

                if (headerSize != 0)
                {
                    block.CompSize = reader.ReadInt32BE();
                    block.DecompSize = reader.ReadInt32BE();
                    block.Hash = reader.Read<MD5Hash>();
                }
                else
                {
                    block.CompSize = size - 8;
                    block.DecompSize = size - 8 - 1;
                    block.Hash = default(MD5Hash);
                }

                dataBlocks[i] = block;
            }

            streamMemory = new MemoryStream(dataBlocks.Sum(b => b.DecompSize));
            ProcessNextBlock();

            length = headerSize == 0 ? streamMemory.Length : streamMemory.Capacity;
        }

        private void HandleDataBlock(byte[] data, int index)
        {
            switch (data[0])
            {
                case 0x45: // E (encrypted)
                    byte[] decrypt = Decrypt(data, index);
                    HandleDataBlock(decrypt, index);
                    break;

                case 0x46: // F (frame, recursive)
                    throw new BLTEDecoderException("DecoderFrame not implemented");

                case 0x4E: // N (not compressed)
                    streamMemory.Write(data, 1, data.Length - 1);
                    break;

                case 0x5A: // Z (zlib compressed)
                    Decompress(data, streamMemory);
                    break;

                default:
                    throw new BLTEDecoderException("Unknown block type: {0} (0x{1:X2})", (char)data[0], data[0]);
            }
        }

        private static byte[] Decrypt(byte[] data, int index)
        {
            byte keyNameSize = data[1];

            if (keyNameSize == 0 || keyNameSize != 8)
                throw new BLTEDecoderException("keyNameSize == 0 || keyNameSize != 8");

            byte[] keyNameBytes = new byte[keyNameSize];
            Array.Copy(data, 2, keyNameBytes, 0, keyNameSize);

            ulong keyName = BitConverter.ToUInt64(keyNameBytes, 0);

            byte IVSize = data[keyNameSize + 2];

            if (IVSize != 4 || IVSize > 0x10)
                throw new BLTEDecoderException("IVSize != 4 || IVSize > 0x10");

            byte[] IVpart = new byte[IVSize];
            Array.Copy(data, keyNameSize + 3, IVpart, 0, IVSize);

            if (data.Length < IVSize + keyNameSize + 4)
                throw new BLTEDecoderException("data.Length < IVSize + keyNameSize + 4");

            int dataOffset = keyNameSize + IVSize + 3;

            byte encType = data[dataOffset];

            if (encType != ENCRYPTION_SALSA20 && encType != ENCRYPTION_ARC4) // 'S' or 'A'
                throw new BLTEDecoderException("encType != ENCRYPTION_SALSA20 && encType != ENCRYPTION_ARC4");

            dataOffset++;

            // expand to 8 bytes
            byte[] IV = new byte[8];
            Array.Copy(IVpart, IV, IVpart.Length);

            // magic
            for (int shift = 0, i = 0; i < sizeof(int); shift += 8, i++)
                IV[i] ^= (byte)((index >> shift) & 0xFF);

            byte[] key = KeyService.GetKey(keyName);

            if (key == null)
                throw new BLTEDecoderException("unknown keyname {0:X16}", keyName);

            if (encType == ENCRYPTION_SALSA20)
            {
                ICryptoTransform decryptor = KeyService.SalsaInstance.CreateDecryptor(key, IV);
                return decryptor.TransformFinalBlock(data, dataOffset, data.Length - dataOffset);
            }
            else
            {
                // ARC4 ?
                throw new BLTEDecoderException("encType ENCRYPTION_ARC4 not implemented");
            }
        }

        private static void Decompress(byte[] data, Stream outStream)
        {
            // skip first 3 bytes (zlib)
            using (var ms = new MemoryStream(data, 3, data.Length - 3))
            using (var dfltStream = new DeflateStream(ms, CompressionMode.Decompress))
            {
                dfltStream.CopyTo(outStream);
            }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (streamMemory.Position + count > streamMemory.Length && blocksIndex < dataBlocks.Length)
            {
                if (!ProcessNextBlock())
                    return 0;

                return Read(buffer, offset, count);
            }
            else
            {
                return streamMemory.Read(buffer, offset, count);
            }
        }

        private bool ProcessNextBlock()
        {
            if (blocksIndex == dataBlocks.Length)
                return false;

            long oldPos = streamMemory.Position;
            streamMemory.Position = streamMemory.Length;

            DataBlock block = dataBlocks[blocksIndex];

            block.Data = reader.ReadBytes(block.CompSize);

            if (!block.Hash.IsZeroed() && CASCConfig.ValidateData)
            {
                byte[] blockHash = hash.ComputeHash(block.Data);

                if (!block.Hash.EqualsTo(blockHash))
                    throw new BLTEDecoderException("MD5 mismatch");
            }

            HandleDataBlock(block.Data, blocksIndex);
            blocksIndex++;

            streamMemory.Position = oldPos;

            return true;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
            }

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (stream != null)
                        stream.Dispose();
                    if (reader != null)
                        reader.Dispose();
                }
            }
            finally
            {
                stream = null;
                reader = null;

                base.Dispose(disposing);
            }
        }
    }
}
