using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using W3DT.Hashing.MD5;

namespace W3DT.CASC
{
    public class CDNIndexHandler
    {
        private static readonly MD5HashComparer comparer = new MD5HashComparer();
        private Dictionary<MD5Hash, IndexEntry> CDNIndexData = new Dictionary<MD5Hash, IndexEntry>(comparer);

        private SyncDownloader downloader;
        private CASCConfig CASCConfig;
        public static CDNCache Cache = new CDNCache("cache");

        public int Count => CDNIndexData.Count;

        private CDNIndexHandler(CASCConfig cascConfig)
        {
            CASCConfig = cascConfig;
            downloader = new SyncDownloader();
        }

        public static CDNIndexHandler Initialize(CASCConfig config)
        {
            CDNIndexHandler handler = new CDNIndexHandler(config);

            for (int i = 0; i < config.Archives.Count; i++)
            {
                string archive = config.Archives[i];

                if (Program.Settings.UseRemote)
                    handler.DownloadIndexFile(archive, i);
                else
                    handler.OpenIndexFile(archive, i);
            }

            return handler;
        }

        private void ParseIndex(Stream stream, int i)
        {
            using (var br = new BinaryReader(stream))
            {
                stream.Seek(-12, SeekOrigin.End);
                int count = br.ReadInt32();
                stream.Seek(0, SeekOrigin.Begin);

                if (count * (16 + 4 + 4) > stream.Length)
                    throw new Exception("ParseIndex failed");

                for (int j = 0; j < count; ++j)
                {
                    MD5Hash key = br.Read<MD5Hash>();

                    if (key.IsZeroed()) // wtf?
                        key = br.Read<MD5Hash>();

                    if (key.IsZeroed()) // wtf?
                        throw new Exception("key.IsZeroed()");

                    IndexEntry entry = new IndexEntry();

                    entry.Index = i;
                    entry.Size = br.ReadInt32BE();
                    entry.Offset = br.ReadInt32BE();

                    CDNIndexData.Add(key, entry);
                }
            }
        }

        private void DownloadIndexFile(string archive, int i)
        {
            try
            {
                string file = Program.Settings.RemoteHostPath + "/data/" + archive.Substring(0, 2) + "/" + archive.Substring(2, 2) + "/" + archive + ".index";
                string url = string.Format("http://{0}/{1}", Program.Settings.RemoteHost, file);

                Stream stream = Cache.OpenFile(file, url, false);

                if (stream != null)
                {
                    ParseIndex(stream, i);
                }
                else
                {
                    using (var fs = downloader.OpenFile(url))
                        ParseIndex(fs, i);
                }
            }
            catch
            {
                throw new Exception("DownloadIndexFile failed!");
            }
        }

        private void OpenIndexFile(string archive, int i)
        {
            try
            {
                string path = Path.Combine(Program.Settings.WoWDirectory, @"Data\indices\", archive + ".index");

                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    ParseIndex(stream, i);
            }
            catch
            {
                throw new Exception("OpenIndexFile failed!");
            }
        }

        public Stream OpenDataFile(IndexEntry entry)
        {
            var archive = CASCConfig.Archives[entry.Index];

            string file = Program.Settings.RemoteHostPath + "/data/" + archive.Substring(0, 2) + "/" + archive.Substring(2, 2) + "/" + archive;
            string url = string.Format("http://{0}/{1}", Program.Settings.RemoteHost, file);

            Stream stream = Cache.OpenFile(file, url, true);

            if (stream != null)
            {
                stream.Position = entry.Offset;
                MemoryStream ms = new MemoryStream(entry.Size);
                stream.CopyBytes(ms, entry.Size);
                ms.Position = 0;
                return ms;
            }

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            req.AddRange(entry.Offset, entry.Offset + entry.Size - 1);
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                MemoryStream ms = new MemoryStream(entry.Size);
                resp.GetResponseStream().CopyBytes(ms, entry.Size);
                ms.Position = 0;
                return ms;
            }
        }

        public Stream OpenDataFileDirect(MD5Hash key)
        {
            var keyStr = key.ToHexString().ToLower();

            string file = Program.Settings.RemoteHostPath + "/data/" + keyStr.Substring(0, 2) + "/" + keyStr.Substring(2, 2) + "/" + keyStr;
            string url = string.Format("http://{0}/{1}", Program.Settings.RemoteHost, file);

            Stream stream = Cache.OpenFile(file, url, false);

            if (stream != null)
                return stream;

            Log.Write(url);
            return downloader.OpenFile(url);
        }

        public static Stream OpenConfigFileDirect(CASCConfig cfg, string key)
        {
            string file = Program.Settings.RemoteHostPath + "/config/" + key.Substring(0, 2) + "/" + key.Substring(2, 2) + "/" + key;
            string url = string.Format("http://{0}/{1}", Program.Settings.RemoteHost, file);

            Stream stream = Cache.OpenFile(file, url, false);

            if (stream != null)
                return stream;

            return OpenFileDirect(url);
        }

        public static Stream OpenFileDirect(string url)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                MemoryStream ms = new MemoryStream();
                resp.GetResponseStream().CopyTo(ms);
                ms.Position = 0;
                return ms;
            }
        }

        public IndexEntry GetIndexInfo(MD5Hash key)
        {
            IndexEntry result;

            if (!CDNIndexData.TryGetValue(key, out result))
                Log.Write("CASC: CDNIndexHandler missing index {0}", key.ToHexString());

            return result;
        }

        public void Clear()
        {
            CDNIndexData.Clear();
            CDNIndexData = null;

            CASCConfig = null;
            downloader = null;
        }
    }
}
