using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    class FileNameCache
    {
        private static Dictionary<string, List<StringHashPair>> Cache = new Dictionary<string, List<StringHashPair>>();

        public static List<StringHashPair> GetFilesWithExtension(string ext)
        {
            return GetFilesWithExtension(new string[] { ext });
        }

        public static List<StringHashPair> GetFilesWithExtension(string[] exts)
        {
            List<string> extList = new List<string>(exts.Length);
            int size = 0;

            foreach (string ext in exts)
            {
                string extLower = ext.ToLower();

                if (Cache.ContainsKey(extLower))
                {
                    extList.Add(extLower);
                    size += Cache[extLower].Count;
                }
            }

            List<StringHashPair> result = new List<StringHashPair>(size);

            foreach (string ext in extList)
                result.AddRange(Cache[ext]);

            return result;
        }

        public static void StoreFileName(StringHashPair file)
        {
            string extension = Path.GetExtension(file.Value);

            if (extension == null || extension == string.Empty)
                return;

            extension = extension.Substring(1).ToLower();

            if (!Cache.ContainsKey(extension))
            {
                Cache.Add(extension, new List<StringHashPair>());
                Log.Write("FileNameCache: Registered new extension {0}.", extension);
            }

            Cache[extension].Add(file);
        }
    }
}
