using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace W3DT.CASC
{
    public class KeyValueConfig
    {
        Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();

        public static KeyValueConfig ReadKeyValueConfig(Stream stream)
        {
            return ReadKeyValueConfig(new StreamReader(stream));
        }

        public static KeyValueConfig ReadKeyValueConfig(TextReader reader)
        {
            var result = new KeyValueConfig();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                string[] tokens = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length != 2)
                    throw new Exception("KeyValueConfig: Invalid length, expected 2.");

                var values = tokens[1].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var valuesList = values.ToList();
                result.Data.Add(tokens[0].Trim(), valuesList);
            }
            return result;
        }

        public List<string> this[string key]
        {
            get { return Data[key]; }
        }
    }
}
