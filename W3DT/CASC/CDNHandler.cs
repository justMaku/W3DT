using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using W3DT.Hashing;
using W3DT.Events;

namespace W3DT.CASC
{
    class CDNHandler
    {
        private static readonly ByteArrayComparer comparer = new ByteArrayComparer();
        private static Dictionary<byte[], IndexEntry> CDNIndexData = new Dictionary<byte[], IndexEntry>(comparer);

        public static void Initialize()
        {
            for (int i = 0; i < CASCConfig.CDNConfig["archives"].Count; ++i)
            {
                string index = CASCConfig.CDNConfig["archives"][i];

                if (Program.Settings.UseRemote)
                    DownloadFile(index, i);
                else
                    OpenFile(index, i);
            }

            EventManager.Trigger_LoadStepDone();
            Log.Write("CASC: CDN loaded {0} indexes", CDNIndexData.Count);
        }

        private static void ParseIndex(Stream stream, int i)
        {
            using (var reader = new BinaryReader(stream))
            {
                stream.Seek(-12, SeekOrigin.End);
                int count = reader.ReadInt32();
                stream.Seek(0, SeekOrigin.Begin);

                for (int j = 0; j < count; ++j)
                {
                    byte[] key = reader.ReadBytes(16);

                    if (key.IsZeroed())
                        key = reader.ReadBytes(16);

                    if (key.IsZeroed())
                        throw new CASCException("Key was zero twice!?");

                    IndexEntry entry = new IndexEntry();
                    entry.Index = i;
                    entry.Size = reader.ReadInt32BE();
                    entry.Offset = reader.ReadInt32BE();

                    CDNIndexData.Add(key, entry);
                }
            }
        }

        private static void DownloadFile(string index, int i)
        {
            try
            {
                Log.Write("CASC: Processing remote index file " + index);
                var url = CASCConfig.CDNUrl + "/data/" + index.Substring(0, 2) + "/" + index.Substring(2, 2) + "/" + index + ".index";

                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(url))
                    {
                        using (MemoryStream mStream = new MemoryStream())
                        {
                            stream.CopyTo(mStream);
                            ParseIndex(mStream, i);
                        }
                    }
                }
            }
            catch
            {
                throw new CASCException("Unable to download file " + index);
            }
        }

        private static void OpenFile(string index, int i)
        {
            try
            {
                var path = Path.Combine(Program.Settings.WoWDirectory, @"Data\indices\", index + ".index");

                using (FileStream stream = new FileStream(path, FileMode.Open))
                    ParseIndex(stream, i);
            }
            catch
            {
                throw new CASCException("Unable to open file " + index);
            }
        }

        public static Stream OpenDataFile(byte[] key)
        {
            var indexEntry = CDNIndexData[key];

            var index = CASCConfig.CDNConfig["archives"][indexEntry.Index];
            var url = CASCConfig.CDNUrl + "/data/" + index.Substring(0, 2) + "/" + index.Substring(2, 2) + "/" + index;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AddRange(indexEntry.Offset, indexEntry.Offset + indexEntry.Size - 1);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp.GetResponseStream();
        }

        public static Stream OpenDataFileDirect(byte[] key, out int len)
        {
            var file = key.ToHexString().ToLower();
            var url = CASCConfig.CDNUrl + "/data/" + file.Substring(0, 2) + "/" + file.Substring(2, 2) + "/" + file;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            len = (int)resp.ContentLength;
            return resp.GetResponseStream();
        }

        public static Stream OpenConfigFileDirect(string key)
        {
            var url = CASCConfig.CDNUrl + "/config/" + key.Substring(0, 2) + "/" + key.Substring(2, 2) + "/" + key;
            return OpenFileDirect(url);
        }

        public static Stream OpenFileDirect(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                return resp.GetResponseStream();
            }
            catch (WebException ex)
            {
                throw new CASCException(ex.Message);
            }
        }

        public static IndexEntry GetCDNIndexInfo(byte[] key)
        {
            if (CDNIndexData.ContainsKey(key))
                return CDNIndexData[key];

            Log.Write("CASC: Missing index {0}", key.ToHexString());

            return null;
        }
    }
}
