using System.Collections.Generic;
using System.IO;

namespace W3DT.CASC
{
    public class VerBarConfig
    {
        private readonly List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();

        public int Count { get { return Data.Count; } }

        public Dictionary<string, string> this[int index]
        {
            get { return Data[index]; }
        }

        public static VerBarConfig ReadVerBarConfig(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return ReadVerBarConfig(reader);
        }

        public static VerBarConfig ReadVerBarConfig(TextReader reader)
        {
            var result = new VerBarConfig();
            string line;

            int lineNum = 0;

            string[] fields = null;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) // skip empty lines and comments
                    continue;

                string[] tokens = line.Split(new char[] { '|' });

                if (lineNum == 0) // keys
                {
                    fields = new string[tokens.Length];

                    for (int i = 0; i < tokens.Length; ++i)
                    {
                        fields[i] = tokens[i].Split(new char[] { '!' })[0].Replace(" ", "");
                    }
                }
                else // values
                {
                    result.Data.Add(new Dictionary<string, string>());

                    for (int i = 0; i < tokens.Length; ++i)
                    {
                        result.Data[lineNum - 1].Add(fields[i], tokens[i]);
                    }
                }

                lineNum++;
            }

            return result;
        }
    }
}
