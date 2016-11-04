using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using W3DT.Hashing;
using W3DT.Hashing.MD5;
using W3DT.Formats.DB5;

namespace W3DT.CASC
{
    [Flags]
    public enum LocaleFlags : uint
    {
        All = 0xFFFFFFFF,
        None = 0,
        //Unk_1 = 0x1,
        enUS = 0x2,
        koKR = 0x4,
        //Unk_8 = 0x8,
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
        ptPT = 0x10000,
        enSG = 0x20000000, // custom
        plPL = 0x40000000, // custom
        All_WoW = enUS | koKR | frFR | deDE | zhCN | esES | zhTW | enGB | esMX | ruRU | ptBR | itIT | ptPT
    }

    [Flags]
    public enum ContentFlags : uint
    {
        None = 0,
        F00000001 = 0x1,
        F00000002 = 0x2,
        F00000004 = 0x4,
        LowViolence = 0x80, // many models have this flag
        F10000000 = 0x10000000,
        F20000000 = 0x20000000, // added in 21737
        Bundle = 0x40000000,
        NoCompression = 0x80000000 // sounds have this flag
    }

    public struct RootEntry
    {
        public MD5Hash MD5;
        public ContentFlags ContentFlags;
        public LocaleFlags LocaleFlags;
    }

    public class RootHandler
    {
        public MultiDictionary<ulong, RootEntry> RootData = new MultiDictionary<ulong, RootEntry>();
        private Dictionary<int, ulong> FileDataStore = new Dictionary<int, ulong>();
        private Dictionary<ulong, int> FileDataStoreReverse = new Dictionary<ulong, int>();
        private Dictionary<ulong, bool> UnknownFiles = new Dictionary<ulong, bool>();

        protected CASCFolder Root;

        protected readonly Jenkins96 Hasher = new Jenkins96();

        public int Count => RootData.Count;
        public int CountTotal => RootData.Sum(re => re.Value.Count);
        public int CountUnknown { get { return UnknownFiles.Count; } }
        public int CountSelect { get; protected set; }

        public LocaleFlags Locale { get; protected set; }
        public ContentFlags Content { get; protected set; }

        private static readonly char[] PathDelimiters = new char[] { '/', '\\' };

        public RootHandler(BinaryReader stream)
        {
            while (stream.BaseStream.Position < stream.BaseStream.Length)
            {
                int count = stream.ReadInt32();

                ContentFlags contentFlags = (ContentFlags)stream.ReadUInt32();
                LocaleFlags localeFlags = (LocaleFlags)stream.ReadUInt32();

                if (localeFlags == LocaleFlags.None)
                    throw new Exception("localeFlags == LocaleFlags.None");

                if (contentFlags != ContentFlags.None && (contentFlags & (ContentFlags.LowViolence | ContentFlags.NoCompression)) == 0)
                    throw new Exception("contentFlags != ContentFlags.None");

                RootEntry[] entries = new RootEntry[count];
                int[] fileDataIDs = new int[count];
                int fileDataIndex = 0;

                for (var i = 0; i < count; ++i)
                {
                    entries[i].LocaleFlags = localeFlags;
                    entries[i].ContentFlags = contentFlags;

                    fileDataIDs[i] = fileDataIndex + (int)stream.ReadUInt32();
                    fileDataIndex = fileDataIDs[i] + 1;
                }

                for (var i = 0; i < count; ++i)
                {
                    entries[i].MD5 = stream.Read<MD5Hash>();
                    ulong hash = stream.ReadUInt64();

                    RootData.Add(hash, entries[i]);

                    ulong hash2;
                    int fileDataID = fileDataIDs[i];

                    if (FileDataStore.TryGetValue(fileDataID, out hash2) && hash2 != hash)
                        Log.Write("CASC: Hash collision for file ID {0}", fileDataID);

                    FileDataStore.Add(fileDataID, hash);
                    FileDataStoreReverse.Add(hash, fileDataID);
                }
            }
        }

        public IEnumerable<RootEntry> GetAllEntriesByFileDataId(int fileDataId)
        {
            return GetAllEntries(GetHashByFileDataId(fileDataId));
        }

        public IEnumerable<KeyValuePair<ulong, RootEntry>> GetAllEntries()
        {
            foreach (var set in RootData)
                foreach (var entry in set.Value)
                    yield return new KeyValuePair<ulong, RootEntry>(set.Key, entry);
        }

        public IEnumerable<RootEntry> GetAllEntries(ulong hash)
        {
            List<RootEntry> result;
            RootData.TryGetValue(hash, out result);

            if (result == null)
                yield break;

            foreach (var entry in result)
                yield return entry;
        }

        public IEnumerable<RootEntry> GetEntriesByFileDataId(int fileDataId)
        {
            return GetEntries(GetHashByFileDataId(fileDataId));
        }

        public IEnumerable<RootEntry> GetEntries(ulong hash)
        {
            var rootInfos = GetAllEntries(hash);

            if (!rootInfos.Any())
                yield break;

            var rootLocale = rootInfos.Where(re => (re.LocaleFlags & Locale) != 0);
            if (rootLocale.Count() > 1)
            {
                var rootLocaleContent = rootLocale.Where(re => (re.ContentFlags == Content));

                if (rootLocaleContent.Any())
                    rootLocale = rootLocaleContent;
            }

            foreach (var entry in rootLocale)
                yield return entry;
        }

        public ulong GetHashByFileDataId(int fileDataId)
        {
            ulong hash;
            FileDataStore.TryGetValue(fileDataId, out hash);
            return hash;
        }

        public int GetFileDataIdByHash(ulong hash)
        {
            int fid;
            FileDataStoreReverse.TryGetValue(hash, out fid);
            return fid;
        }

        public int GetFileDataIdByName(string name)
        {
            return GetFileDataIdByHash(Hasher.ComputeHash(name));
        }

        private bool LoadPreHashedListFile(string binaryPath, string pathText)
        {
            if (!File.Exists(binaryPath))
                    return false;

            var timebin = File.GetLastWriteTime(binaryPath);
            var timetext = File.GetLastWriteTime(pathText);

            if (timebin != timetext) // text has been modified, recreate pre-hashed file
                return false;

            Log.Write("CASC: Loading files names...");

            using (var fs = new FileStream(binaryPath, FileMode.Open))
            using (var br = new BinaryReader(fs))
            {
                int numFolders = br.ReadInt32();

                for (int i = 0; i < numFolders; i++)
                {
                    string dirName = br.ReadString();
                    int numFiles = br.ReadInt32();

                    for (int j = 0; j < numFiles; j++)
                    {
                        ulong fileHash = br.ReadUInt64();
                        string fileName = br.ReadString();

                        string fileNameFull = dirName != String.Empty ? dirName + "\\" + fileName : fileName;

                        // skip invalid names
                        if (!RootData.ContainsKey(fileHash))
                            continue;

                        CASCFile.FileNames[fileHash] = fileNameFull;
                        //FileNameCache.StoreFileName(new StringHashPair(fileHash, fileNameFull));
                    }
                }

                Log.Write("CASC: Loaded {0} file names!", CASCFile.FileNames.Count);
            }

            return true;
        }

        public void LoadFileDataComplete(CASCEngine casc)
        {
            string dbFileDataComplete = "DBFilesClient\\FileDataComplete.db2";
            if (!casc.FileExists(dbFileDataComplete))
                return;

            Log.Write("CASC: Loading entries from FileDataComplete (DB2)");

            using (var stream = casc.OpenFile(dbFileDataComplete))
            {
                DB5_Reader db = new DB5_Reader(stream);
                foreach (var row in db)
                {
                    string path = row.Value.GetField<string>(0);
                    string name = row.Value.GetField<string>(1);

                    string fullName = path + name;
                    ulong fileHash = Hasher.ComputeHash(fullName);

                    if (!RootData.ContainsKey(fileHash))
                        continue;

                    CASCFile.FileNames[fileHash] = fullName;
                }
            }
        }

        public void LoadListFile(string path)
        {
            if (LoadPreHashedListFile(Constants.LIST_FILE_BIN, path))
                return;

            if (!File.Exists(path))
                throw new FileNotFoundException("list file missing!");

            Log.Write("CASC: Loading file names...");

            Dictionary<string, Dictionary<ulong, string>> dirData = new Dictionary<string, Dictionary<ulong, string>>(StringComparer.OrdinalIgnoreCase);
            dirData[""] = new Dictionary<ulong, string>();

            using (var fs = new FileStream(Constants.LIST_FILE_BIN, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            using (var sr = new StreamReader(path))
            {
                string file;

                while ((file = sr.ReadLine()) != null)
                {
                    ulong fileHash = Hasher.ComputeHash(file);

                    // skip invalid names
                    if (!RootData.ContainsKey(fileHash))
                        continue;

                    CASCFile.FileNames[fileHash] = file;
                    //FileNameCache.StoreFileName(new StringHashPair(fileHash, file));
                    int dirSepIndex = file.LastIndexOf('\\');

                    if (dirSepIndex >= 0)
                    {
                        string key = file.Substring(0, dirSepIndex);

                        if (!dirData.ContainsKey(key))
                            dirData[key] = new Dictionary<ulong, string>();

                        dirData[key][fileHash] = file.Substring(dirSepIndex + 1);
                    }
                    else
                    {
                        dirData[""][fileHash] = file;
                    }
                }

                bw.Write(dirData.Count); // Count of directories.

                foreach (var dir in dirData)
                {
                    bw.Write(dir.Key); // Directory name.
                    bw.Write(dirData[dir.Key].Count); // File count.

                    foreach (var fh in dirData[dir.Key])
                    {
                        bw.Write(fh.Key); // File name hash.
                        bw.Write(fh.Value); // File name.
                    }
                }

                Log.Write("CASC: Loaded {0} valid file names", CASCFile.FileNames.Count);
            }

            File.SetLastWriteTime(Constants.LIST_FILE_BIN, File.GetLastWriteTime(path));
        }

        protected CASCFolder CreateStorageTree()
        {
            var root = new CASCFolder("root");

            CountSelect = 0;
            UnknownFiles.Clear();

            // Create new tree based on specified locale
            foreach (var rootEntry in RootData)
            {
                var rootLocale = rootEntry.Value.Where(re => (re.LocaleFlags & Locale) != 0);

                if (rootLocale.Count() > 1)
                {
                    var rootLocaleContent = rootLocale.Where(re => (re.ContentFlags == Content));

                    if (rootLocaleContent.Any())
                        rootLocale = rootLocaleContent;
                }

                if (!rootLocale.Any())
                    continue;

                string file;

                if (!CASCFile.FileNames.TryGetValue(rootEntry.Key, out file))
                {
                    file = "unknown\\" + rootEntry.Key.ToString("X16") + "_" + GetFileDataIdByHash(rootEntry.Key);
                    UnknownFiles[rootEntry.Key] = true;
                }

                CreateSubTree(root, rootEntry.Key, file);
                CountSelect++;
            }

            Log.Write("CASC: {0} file names missing for {1}", CountUnknown, Locale);

            return root;
        }

        protected void CreateSubTree(CASCFolder root, ulong filehash, string file)
        {
            string[] parts = file.Split(PathDelimiters);

            CASCFolder folder = root;

            for (int i = 0; i < parts.Length; ++i)
            {
                bool isFile = (i == parts.Length - 1);

                string entryName = parts[i];

                ICASCEntry entry = folder.GetEntry(entryName);

                if (entry == null)
                {
                    if (isFile)
                    {
                        entry = new CASCFile(filehash);
                        CASCFile.FileNames[filehash] = file;
                    }
                    else
                    {
                        entry = new CASCFolder(entryName);
                    }

                    folder.Entries[entryName] = entry;
                }

                folder = entry as CASCFolder;
            }
        }

        public void MergeInstall(InstallHandler install)
        {
            if (install == null)
                return;

            foreach (var entry in install.GetEntries())
                CreateSubTree(Root, Hasher.ComputeHash(entry.Name), entry.Name);
        }

        public CASCFolder SetFlags(LocaleFlags locale, ContentFlags content, bool createTree = true)
        {
            Locale = locale;
            Content = content;

            if (createTree)
                Root = CreateStorageTree();

            return Root;
        }

        public bool IsUnknownFile(ulong hash)
        {
            return UnknownFiles.ContainsKey(hash);
        }

        public void Clear()
        {
            RootData.Clear();
            RootData = null;

            FileDataStore.Clear();
            FileDataStore = null;

            FileDataStoreReverse.Clear();
            FileDataStoreReverse = null;

            UnknownFiles.Clear();
            UnknownFiles = null;

            Root?.Entries.Clear();
            Root = null;

            CASCFile.FileNames.Clear();
        }

        public void Dump()
        {
            foreach (var fd in RootData.OrderBy(r => GetFileDataIdByHash(r.Key)))
            {
                string name;

                if (!CASCFile.FileNames.TryGetValue(fd.Key, out name))
                    name = fd.Key.ToString("X16");

                Log.Write("{0:D7} {1:X16} {2} {3}", GetFileDataIdByHash(fd.Key), fd.Key, fd.Value.Aggregate(LocaleFlags.None, (a, b) => a | b.LocaleFlags), name);

                foreach (var entry in fd.Value)
                    Log.Write("\t{0} - {1} - {2}", entry.MD5.ToHexString(), entry.LocaleFlags, entry.ContentFlags);
            }
        }
    }
}
