using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using W3DT.Hashing.MD5;

namespace W3DT.CASC
{
    class LocalIndexHandler
    {
        private static readonly MD5HashComparer comparer = new MD5HashComparer();
        private Dictionary<MD5Hash, IndexEntry> LocalIndexData = new Dictionary<MD5Hash, IndexEntry>(comparer);
        public int Count => LocalIndexData.Count;

        private LocalIndexHandler() {}

        public static LocalIndexHandler Initialize(CASCConfig config)
        {
            LocalIndexHandler handler = new LocalIndexHandler();
            List<string> idxFiles = GetIdxFiles(config);

            if (idxFiles.Count == 0)
                throw new FileNotFoundException("idx files missing!");

            foreach (string idx in idxFiles)
                handler.ParseIndex(idx);

            Log.Write("CASC: LocalIndexHandler loaded {0} indexes", handler.Count);

            return handler;
        }

        private void ParseIndex(string idx)
        {
            using (var fs = new FileStream(idx, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(fs))
            {
                int h2Len = br.ReadInt32();
                int h2Check = br.ReadInt32();
                byte[] h2 = br.ReadBytes(h2Len);

                long padPos = (8 + h2Len + 0x0F) & 0xFFFFFFF0;
                fs.Position = padPos;

                int dataLen = br.ReadInt32();
                int dataCheck = br.ReadInt32();

                int numBlocks = dataLen / 18;

                for (int i = 0; i < numBlocks; i++)
                {
                    IndexEntry info = new IndexEntry();
                    MD5Hash key = br.Read<MD5Hash>();
                    byte indexHigh = br.ReadByte();
                    int indexLow = br.ReadInt32BE();

                    info.Index = (indexHigh << 2 | (byte)((indexLow & 0xC0000000) >> 30));
                    info.Offset = (indexLow & 0x3FFFFFFF);
                    info.Size = br.ReadInt32();

                    if (!LocalIndexData.ContainsKey(key))
                        LocalIndexData.Add(key, info);
                }

                padPos = (dataLen + 0x0FFF) & 0xFFFFF000;
                fs.Position = padPos;

                fs.Position += numBlocks * 18;
            }
        }

        private static List<string> GetIdxFiles(CASCConfig config)
        {
            List<string> latestIdx = new List<string>();
            string dataPath = Path.Combine(Program.Settings.WoWDirectory, @"Data\data");

            for (int i = 0; i < 0x10; ++i)
            {
                var files = Directory.EnumerateFiles(dataPath, String.Format("{0:X2}*.idx", i));

                if (files.Count() > 0)
                    latestIdx.Add(files.Last());
            }

            return latestIdx;
        }

        public unsafe IndexEntry GetIndexInfo(MD5Hash key)
        {
            ulong* ptr = (ulong*)&key;
            ptr[1] &= 0xFf;

            IndexEntry result;
            if (!LocalIndexData.TryGetValue(key, out result))
                Log.Write("CASC: Missing index {0}", key.ToHexString());

            return result;
        }

        public void Clear()
        {
            LocalIndexData?.Clear();
            LocalIndexData = null;
        }
    }
}
