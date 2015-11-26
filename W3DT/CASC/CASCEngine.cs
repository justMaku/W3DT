using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using W3DT.Events;

namespace W3DT.CASC
{
    [Flags]
    public enum LocaleFlags
    {
        All = -1,
        None = 0,
        Unk_1 = 0x1,
        enUS = 0x2,
        koKR = 0x4,
        Unk_8 = 0x8,
        frFR = 0x10,
        deDE = 0x20,
        zhCN = 0x40,
        esES = 0x80,
        zhTW = 0x100,
        enGB = 0x200,
        enCN = 0x400,
        enTW = 0x800,
        esMX = 0x1000,
        ruRU = 0x2000,
        ptBR = 0x4000,
        itIT = 0x8000,
        ptPT = 0x10000
    }

    public class RootBlock
    {
        public uint Unk1;
        public LocaleFlags Flags;
    }

    public class RootEntry
    {
        public RootBlock Block;
        public int Unk1;
        public byte[] MD5;
        public ulong Hash;

        public override string ToString()
        {
            return String.Format("Block: {0:X8} {1:X8}, File: {2:X8} {3}", Block.Unk1, Block.Flags, Unk1, MD5.ToHexString());
        }
    }

    public class EncodingEntry
    {
        public int Size;
        public List<byte[]> Keys;

        public EncodingEntry()
        {
            Keys = new List<byte[]>();
        }
    }

    public class IndexEntry
    {
        public int Index;
        public int Offset;
        public int Size;
    }

    public class CASCEngine
    {
        // CASC hash -> name cache
        public static readonly Dictionary<ulong, string> FileNames = new Dictionary<ulong, string>();
        public static readonly Dictionary<ulong, string> FolderNames = new Dictionary<ulong, string>();

        private static readonly ByteArrayComparer comparer = new ByteArrayComparer();
        private readonly Dictionary<ulong, List<RootEntry>> RootData = new Dictionary<ulong, List<RootEntry>>();
        private readonly Dictionary<byte[], EncodingEntry> EncodingData = new Dictionary<byte[], EncodingEntry>(comparer);
        private readonly Dictionary<byte[], IndexEntry> LocalIndexData = new Dictionary<byte[], IndexEntry>(comparer);

        public readonly Dictionary<int, FileStream> DataStreams = new Dictionary<int, FileStream>();

        public CASCFolder Root { get; private set; }

        public CASCEngine(CASCFolder rootFolder)
        {
            Log.Write("Initializing CASC data storage...");

            Root = rootFolder;

            if (!Program.Settings.UseRemote)
            {
                Log.Write("CASC: Processing index files.");
                List<string> indexFiles = GetIdxFiles();

                if (indexFiles.Count == 0)
                    throw new CASCException("Local CASC data store is missing index files.");

                foreach (string indexFile in indexFiles)
                {
                    Log.Write("CASC: Processing index file {0}.", indexFile);
                    using (var fileStream = new FileStream(indexFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = new BinaryReader(fileStream))
                        {
                            int h2Len = reader.ReadInt32();
                            int h2Check = reader.ReadInt32();
                            byte[] h2 = reader.ReadBytes(h2Len);

                            long padPos = (8 + h2Len + 0x0F) & 0xFFFFFFF0;
                            fileStream.Position = padPos;

                            int dataLen = reader.ReadInt32();
                            int dataCheck = reader.ReadInt32();

                            int numBlocks = dataLen / 18;

                            for (int i = 0; i < numBlocks; i++)
                            {
                                IndexEntry info = new IndexEntry();
                                byte[] key = reader.ReadBytes(9);
                                int indexHigh = reader.ReadByte();
                                int indexLow = reader.ReadInt32BE();

                                info.Index = (int)((byte)(indexHigh << 2) | ((indexLow & 0xC0000000) >> 30));
                                info.Offset = (indexLow & 0x3FFFFFFF);
                                info.Size = reader.ReadInt32();

                                if (!LocalIndexData.ContainsKey(key))
                                    LocalIndexData.Add(key, info);
                            }

                            padPos = (dataLen + 0x0FFF) & 0xFFFFF000;
                            fileStream.Position = padPos;
                            fileStream.Position += numBlocks * 18;
                        }
                    }
                }

                Log.Write("CASC: {0} index files processed.", LocalIndexData.Count);
                EventManager.Trigger_LoadStepDone();
            }

            Log.Write("CASC: Loading encoding data...");
            using (var fileStream = OpenEncodingFile())
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    reader.ReadBytes(2); // EN
                    byte b1 = reader.ReadByte();
                    byte b2 = reader.ReadByte();
                    byte b3 = reader.ReadByte();
                    ushort s1 = reader.ReadUInt16();
                    ushort s2 = reader.ReadUInt16();
                    int numEntries = reader.ReadInt32BE();
                    int i1 = reader.ReadInt32BE();
                    byte b4 = reader.ReadByte();
                    int entriesOfs = reader.ReadInt32BE();

                    fileStream.Position += entriesOfs;
                    fileStream.Position += numEntries * 32;

                    for (int i = 0; i < numEntries; ++i)
                    {
                        ushort keysCount;

                        while ((keysCount = reader.ReadUInt16()) != 0)
                        {
                            int fileSize = reader.ReadInt32BE();
                            byte[] md5 = reader.ReadBytes(16);

                            var entry = new EncodingEntry();
                            entry.Size = fileSize;

                            for (int ki = 0; ki < keysCount; ++ki)
                            {
                                byte[] key = reader.ReadBytes(16);

                                entry.Keys.Add(key);
                            }

                            EncodingData.Add(md5, entry);
                        }

                        while (reader.PeekChar() == 0)
                            fileStream.Position++;
                    }

                    Log.Write("CASC: {0} encoding entries loaded.", EncodingData.Count);
                    EventManager.Trigger_LoadStepDone();
                }
            }

            // ToDo: Root
            EventManager.Trigger_LoadStepDone();

            // ToDo: Files
            EventManager.Trigger_LoadStepDone();

            // Jobs done, trigger event to let splash know.
            EventManager.Trigger_CASCLoadDone();
        }

        private Stream OpenEncodingFile()
        {
            return OpenFile(CASCConfig.EncodingKey);
        }

        private Stream OpenFile(byte[] key)
        {
            try
            {
                if (Program.Settings.UseRemote)
                    throw new Exception();

                var idxInfo = GetLocalIndexInfo(key);

                if (idxInfo == null)
                    throw new Exception("local index missing");

                var stream = GetDataStream(idxInfo.Index);

                stream.Position = idxInfo.Offset;

                stream.Position += 30;

                using (BLTEHandler blte = new BLTEHandler(stream, idxInfo.Size - 30))
                    return blte.OpenFile();
            }
            catch
            {
                if (key.EqualsTo(CASCConfig.EncodingKey))
                {
                    int len;
                    using (Stream s = CDNHandler.OpenDataFileDirect(key, out len))
                    using (BLTEHandler blte = new BLTEHandler(s, len))
                        return blte.OpenFile();
                }
                else
                {
                    var idxInfo = CDNHandler.GetCDNIndexInfo(key);

                    if (idxInfo == null)
                        throw new Exception("CDN index missing");

                    using (Stream s = CDNHandler.OpenDataFile(key))
                    using (BLTEHandler blte = new BLTEHandler(s, idxInfo.Size))
                        return blte.OpenFile();
                }
            }
        }

        public void ExtractFile(byte[] key, string path, string name)
        {
            try
            {
                if (Program.Settings.UseRemote)
                    throw new Exception();

                var idxInfo = GetLocalIndexInfo(key);

                if (idxInfo == null)
                    throw new CASCException("Index file missing from local data source.");

                var stream = GetDataStream(idxInfo.Index);

                stream.Position = idxInfo.Offset;

                stream.Position += 30;

                using (BLTEHandler blte = new BLTEHandler(stream, idxInfo.Size - 30))
                    blte.ExtractToFile(path, name);
            }
            catch
            {
                if (key.EqualsTo(CASCConfig.EncodingKey))
                {
                    int len;
                    using (Stream s = CDNHandler.OpenDataFileDirect(key, out len))
                    using (BLTEHandler blte = new BLTEHandler(s, len))
                        blte.ExtractToFile(path, name);
                }
                else
                {
                    var idxInfo = CDNHandler.GetCDNIndexInfo(key);

                    if (idxInfo == null)
                        throw new Exception("CDN index missing");

                    using (Stream s = CDNHandler.OpenDataFile(key))
                    using (BLTEHandler blte = new BLTEHandler(s, idxInfo.Size))
                        blte.ExtractToFile(path, name);
                }
            }
        }

        ~CASCEngine()
        {
            foreach (var stream in DataStreams)
                stream.Value.Close();
        }

        private List<string> GetIdxFiles()
        {
            List<string> indexFiles = new List<string>();

            for (int i = 0; i < 0x10; ++i)
            {
                var files = Directory.EnumerateFiles(Path.Combine(Program.Settings.WoWDirectory, @"Data\data\"), string.Format("{0:X2}*.idx", i));

                if (files.Count() > 0)
                    indexFiles.Add(files.Last());
            }

            return indexFiles;
        }

        public List<RootEntry> GetRootInfo(ulong hash)
        {
            if (RootData.ContainsKey(hash))
                return RootData[hash];
            return null;
        }

        public EncodingEntry GetEncodingInfo(byte[] md5)
        {
            if (EncodingData.ContainsKey(md5))
                return EncodingData[md5];
            return null;
        }

        public IndexEntry GetLocalIndexInfo(byte[] key)
        {
            byte[] temp = key.Copy(9);
            if (LocalIndexData.ContainsKey(temp))
                return LocalIndexData[temp];

            return null;
        }

        public FileStream GetDataStream(int index)
        {
            if (DataStreams.ContainsKey(index))
                return DataStreams[index];

            string dataFile = Path.Combine(Program.Settings.WoWDirectory, String.Format(@"Data\data\data.{0:D3}", index));

            var fs = new FileStream(dataFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            DataStreams[index] = fs;

            return fs;
        }
    }
}
