using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace W3DT.Formats
{
    public class FormatBase
    {
        private byte[] data;
        protected int seek = 0;
        public string FileName { get; private set; }

        public FormatBase(string filePath)
        {
            FileName = Path.GetFileName(filePath);

            if (!File.Exists(filePath))
                throw new Exception("Unable to read binary file: " + filePath);

            data = File.ReadAllBytes(filePath);

            using (SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider())
            {
                string hash = Convert.ToBase64String(hasher.ComputeHash(data));
                Log.Write("Opening binary file {0} [{1}]", FileName, hash);
            }
        }

        public int getSeek()
        {
            return seek;
        }

        public bool isOutOfBounds(int offset)
        {
            return offset < 0 || offset >= data.Length;
        }

        public bool isEndOfStream()
        {
            return seek == data.Length;
        }

        public byte readUInt8()
        {
            return readBytes(1)[0];
        }

        public Int16 readInt16()
        {
            Int16 result = BitConverter.ToInt16(data, seek);
            skip(2);

            return result;
        }

        public Int32 readInt32()
        {
            Int32 result = BitConverter.ToInt32(data, seek);
            skip(4);

            return result;
        }

        public Int64 readInt64()
        {
            Int64 result = BitConverter.ToInt64(data, seek);
            skip(8);

            return result;
        }

        public UInt16 readUInt16()
        {
            UInt16 result = BitConverter.ToUInt16(data, seek);
            skip(2);

            return result;
        }

        public UInt32 readUInt32()
        {
            UInt32 result = BitConverter.ToUInt32(data, seek);
            skip(4);

            return result;
        }

        public UInt64 readUInt64()
        {
            UInt64 result = BitConverter.ToUInt64(data, seek);
            skip(8);

            return result;
        }

        public float readFloat()
        {
            float result = BitConverter.ToSingle(data, seek);
            skip(4);

            return result;
        }

        public double readDouble()
        {
            double result = BitConverter.ToDouble(data, seek);
            skip(8);

            return result;
        }

        public string readString(int length)
        {
            string result = string.Empty;

            foreach (byte b in readBytes(length))
                if (b != 0x0)
                    result += (char) b;

            return result;
        }

        public int seekNonZero()
        {
            int seekAmount = 0;
            while (true)
            {
                if (peekBytes(1)[0] == 0x0)
                {
                    seekAmount++;
                    skip(1);
                }
                else
                {
                    break;
                }
            }

            return seekAmount;
        }

        public void seekPosition(int position)
        {
            this.seek = position;
        }

        public string readString()
        {
            string result = string.Empty;

            while (true)
            {
                if (isEndOfStream())
                    return result;

                byte next = readBytes(1)[0];
                if (next == 0x0)
                    return result;

                result += (char)next;
            }
        }

        public string peekString(int length)
        {
            string result = string.Empty;

            foreach (byte b in peekBytes(length))
                result += (char)b;

            return result;
        }

        public void skip(int count)
        {
            seek += count;
        }

        public byte[] readBytes(int count)
        {
            byte[] outBytes = peekBytes(count);

            skip(count);

            return outBytes;
        }

        public byte[] peekBytes(int count)
        {
            byte[] outBytes = new byte[count];
            for (int i = 0; i < count; i++)
                outBytes[i] = data[seek + i];

            return outBytes;
        }

        public void writeToFile(string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write))
                stream.Write(data, 0, data.Length);
        }
    }
}
