using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    class FileNameCache
    {
        private static Dictionary<string, List<string>> Cache = new Dictionary<string, List<string>>();

        public static List<string> GetFilesWithExtension(string ext)
        {
            return GetFilesWithExtension(new string[] { ext });
        }

        public static List<string> GetFilesWithExtension(string[] exts)
        {
            List<string> result = new List<string>();

            foreach (string extRaw in exts)
            {
                string ext = extRaw.ToLower();

                if (Cache.ContainsKey(ext))
                    result.Concat(Cache[ext]);
            }

            return result;
        }

        public static void StoreFileName(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if (extension == null || extension == string.Empty)
                return;

            extension = extension.Substring(1).ToLower();

            if (!Cache.ContainsKey(extension))
            {
                Cache.Add(extension, new List<string>());
                Log.Write("FileNameCache: Registered new extension {0}.", extension);
            }

            Cache[extension].Add(fileName);
        }
    }
}
