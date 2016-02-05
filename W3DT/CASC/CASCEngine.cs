using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using W3DT.Events;
using W3DT.Hashing;

namespace W3DT.CASC
{
    public class IndexEntry
    {
        public int Index;
        public int Offset;
        public int Size;
    }

    public class CASCEngine
    {
        private LocalIndexHandler LocalIndex;
        private CDNIndexHandler CDNIndex;

        private EncodingHandler EncodingHandler;
        private DownloadHandler DownloadHandler;
        private InstallHandler InstallHandler;

        private static readonly Jenkins96 Hasher = new Jenkins96();

        public EncodingHandler Encoding { get { return EncodingHandler; } }
        public DownloadHandler Download { get { return DownloadHandler; } }
        public RootHandler RootHandler { get; private set; }
        public InstallHandler Install { get { return InstallHandler; } }

        public CASCConfig Config { get; private set; }

        private CASCEngine(CASCConfig config)
        {
            Config = config;

            Log.Write("CASC: Loading indexes...");
            CDNIndex = CDNIndexHandler.Initialize(config);

            Log.Write("CASC: Loaded {0} indexes", CDNIndex.Count);

            if (!Program.Settings.UseRemote)
            {
                CDNIndexHandler.Cache.Enabled = false;

                Log.Write("CASC: Loading indexes from local storage...");
                LocalIndex = LocalIndexHandler.Initialize(config);

                Log.Write("CASC: Loaded {0} indexes from local storage", LocalIndex.Count);
            }
            EventManager.Trigger_LoadStepDone();

            Log.Write("CASC: Loading encoding data...");
            using (var stream = OpenEncodingFile())
                EncodingHandler = new EncodingHandler(stream);

            Log.Write("CASC: Loaded {0} encoding data entries", EncodingHandler.Count);

            if ((CASCConfig.LoadFlags & LoadFlags.Download) != 0)
            {
                Log.Write("CASC: Loading download data...");

                using (var stream = OpenDownloadFile())
                    DownloadHandler = new DownloadHandler(stream);

                Log.Write("CASC: Loaded {0} download data entries", EncodingHandler.Count);
            }
            EventManager.Trigger_LoadStepDone();

            Log.Write("CASC: Loading root data...");

            using (var stream = OpenRootFile())
                RootHandler = new RootHandler(stream);

            Log.Write("CASC: Loaded {0} root data entries", RootHandler.Count);

            if ((CASCConfig.LoadFlags & LoadFlags.Install) != 0)
            {
                Log.Write("CASC: Loading install data...");

                using (var stream = OpenInstallFile())
                    InstallHandler = new InstallHandler(stream);

                Log.Write("CASC: Loaded {0} install data entries", InstallHandler.Count);
            }
            EventManager.Trigger_LoadStepDone();
        }

        private BinaryReader OpenInstallFile()
        {
            var encInfo = EncodingHandler.GetEntry(Config.InstallMD5);

            if (encInfo == null)
                throw new FileNotFoundException("encoding info for install file missing!");

            return new BinaryReader(OpenFile(encInfo.Key));
        }

        private BinaryReader OpenDownloadFile()
        {
            var encInfo = EncodingHandler.GetEntry(Config.DownloadMD5);

            if (encInfo == null)
                throw new FileNotFoundException("encoding info for download file missing!");

            return new BinaryReader(OpenFile(encInfo.Key));
        }

        private BinaryReader OpenRootFile()
        {
            var encInfo = EncodingHandler.GetEntry(Config.RootMD5);

            if (encInfo == null)
                throw new FileNotFoundException("encoding info for root file missing!");

            return new BinaryReader(OpenFile(encInfo.Key));
        }

        private BinaryReader OpenEncodingFile()
        {
            return new BinaryReader(OpenFile(Config.EncodingKey));
        }

        public Stream OpenFile(byte[] key)
        {
            try
            {
                if (Program.Settings.UseRemote)
                    return OpenFileOnline(key);
                else
                    return OpenFileLocal(key);
            }
            catch
            {
                return OpenFileOnline(key);
            }
        }

        private Stream OpenFileOnline(byte[] key)
        {
            IndexEntry idxInfo = CDNIndex.GetIndexInfo(key);

            if (idxInfo != null)
            {
                using (Stream s = CDNIndex.OpenDataFile(idxInfo))
                using (BLTEHandler blte = new BLTEHandler(s, key))
                {
                    return blte.OpenFile(true);
                }
            }
            else
            {
                using (Stream s = CDNIndex.OpenDataFileDirect(key))
                using (BLTEHandler blte = new BLTEHandler(s, key))
                {
                    return blte.OpenFile(true);
                }
            }
        }

        private Stream OpenFileLocal(byte[] key)
        {
            Stream stream = GetLocalIndexData(key);

            using (BLTEHandler blte = new BLTEHandler(stream, key))
            {
                return blte.OpenFile(true);
            }
        }

        private Stream GetLocalIndexData(byte[] key)
        {
            IndexEntry idxInfo = LocalIndex.GetIndexInfo(key);

            if (idxInfo == null)
                throw new Exception("local index missing");

            Stream dataStream = GetDataStream(idxInfo.Index);

            if (dataStream.CanSeek)
                dataStream.Position = idxInfo.Offset;

            using (BinaryReader reader = new BinaryReader(dataStream, System.Text.Encoding.ASCII))
            {
                byte[] md5 = reader.ReadBytes(16);
                Array.Reverse(md5);

                if (!md5.EqualsTo(key))
                    throw new Exception("Corrupted local data!");

                int size = reader.ReadInt32();

                if (size != idxInfo.Size)
                    throw new Exception("Corrupted local data!");

                dataStream.Position += 10;

                byte[] data = reader.ReadBytes(idxInfo.Size - 30);

                return new MemoryStream(data);
            }
        }

        public void ExtractFile(byte[] key, string path, string name)
        {
            try
            {
                if (Program.Settings.UseRemote)
                    ExtractFileOnline(key, path, name);
                else
                    ExtractFileLocal(key, path, name);
            }
            catch
            {
                ExtractFileOnline(key, path, name);
            }
        }

        private void ExtractFileOnline(byte[] key, string path, string name)
        {
            IndexEntry idxInfo = CDNIndex.GetIndexInfo(key);

            if (idxInfo != null)
            {
                using (Stream s = CDNIndex.OpenDataFile(idxInfo))
                using (BLTEHandler blte = new BLTEHandler(s, key))
                {
                    blte.ExtractToFile(path, name);
                }
            }
            else
            {
                using (Stream s = CDNIndex.OpenDataFileDirect(key))
                using (BLTEHandler blte = new BLTEHandler(s, key))
                {
                    blte.ExtractToFile(path, name);
                }
            }
        }

        private void ExtractFileLocal(byte[] key, string path, string name)
        {
            Stream stream = GetLocalIndexData(key);

            using (BLTEHandler blte = new BLTEHandler(stream, key))
            {
                blte.ExtractToFile(path, name);
            }
        }

        private Stream GetDataStream(int index)
        {
            Stream stream;

            string dataFile = Path.Combine(Program.Settings.WoWDirectory, string.Format("Data\\data\\data.{0:D3}", index));
            stream = new FileStream(dataFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            return stream;
        }

        public static CASCEngine OpenStorage(CASCConfig config)
        {
            return Open(config);
        }

        public static CASCEngine OpenLocalStorage()
        {
            CASCConfig config = CASCConfig.LoadLocalStorageConfig();
            return Open(config);
        }

        public static CASCEngine OpenOnlineStorage()
        {
            CASCConfig config = CASCConfig.LoadOnlineStorageConfig();
            return Open(config);
        }

        private static CASCEngine Open(CASCConfig config)
        {
            return new CASCEngine(config);
        }

        public bool FileExists(int fileDataId)
        {
            if (RootHandler != null)
            {
                var hash = RootHandler.GetHashByFileDataId(fileDataId);
                return FileExists(hash);
            }

            return false;
        }

        public bool FileExists(string file)
        {
            var hash = Hasher.ComputeHash(file);
            return FileExists(hash);
        }

        public bool FileExists(ulong hash)
        {
            var rootInfos = RootHandler.GetAllEntries(hash);
            return rootInfos != null && rootInfos.Any();
        }

        public EncodingEntry GetEncodingEntry(ulong hash)
        {
            var rootInfos = RootHandler.GetEntries(hash);
            if (rootInfos.Any())
                return EncodingHandler.GetEntry(rootInfos.First().MD5);

            if ((CASCConfig.LoadFlags & LoadFlags.Install) != 0)
            {
                var installInfos = Install.GetEntries().Where(e => Hasher.ComputeHash(e.Name) == hash);
                if (installInfos.Any())
                    return EncodingHandler.GetEntry(installInfos.First().MD5);
            }

            return null;
        }

        public Stream OpenFile(int fileDataId)
        {
            if (RootHandler != null)
            {
                var hash = RootHandler.GetHashByFileDataId(fileDataId);
                return OpenFile(hash, "FileData_" + fileDataId.ToString());
            }

            if (CASCConfig.ThrowOnFileNotFound)
                throw new FileNotFoundException("FileData: " + fileDataId.ToString());
            return null;
        }

        public Stream OpenFile(string fullName)
        {
            var hash = Hasher.ComputeHash(fullName);

            return OpenFile(hash, fullName);
        }

        public Stream OpenFile(ulong hash, string fullName)
        {
            EncodingEntry encInfo = GetEncodingEntry(hash);

            if (encInfo != null)
                return OpenFile(encInfo.Key);

            if (CASCConfig.ThrowOnFileNotFound)
                throw new FileNotFoundException(fullName);
            return null;
        }

        public void SaveFileTo(string fullName, string extractPath)
        {
            var hash = Hasher.ComputeHash(fullName);

            SaveFileTo(hash, extractPath, fullName);
        }

        public void SaveFileTo(ulong hash, string extractPath, string fullName)
        {
            EncodingEntry encInfo = GetEncodingEntry(hash);

            if (encInfo != null)
            {
                ExtractFile(encInfo.Key, extractPath, fullName);
                return;
            }

            if (CASCConfig.ThrowOnFileNotFound)
                throw new FileNotFoundException(fullName);
        }

        public void Clear()
        {
            CDNIndex.Clear();
            CDNIndex = null;

            EncodingHandler.Clear();
            EncodingHandler = null;

            if (InstallHandler != null)
            {
                InstallHandler.Clear();
                InstallHandler = null;
            }

            if (LocalIndex != null)
            {
                LocalIndex.Clear();
                LocalIndex = null;
            }

            RootHandler.Clear();
            RootHandler = null;

            if (DownloadHandler != null)
            {
                DownloadHandler.Clear();
                DownloadHandler = null;
            }
        }
    }
}
