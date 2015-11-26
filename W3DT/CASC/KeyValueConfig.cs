using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    class KeyValueConfig
    {
        Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();

        public KeyValueConfig(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#") || line.Length == 0) // Skip comments and blank lines.
                        continue;

                    string[] tokens = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length != 2)
                        throw new CASCException("Malformed token length.");

                    var values = tokens[1].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var valuesList = new List<string>();
                    valuesList.AddRange(values);
                    Data.Add(tokens[0].Trim(), valuesList);
                }
            }
        }

        public List<string> this[string key]
        {
            get { return Data[key]; }
        }
    }
}
