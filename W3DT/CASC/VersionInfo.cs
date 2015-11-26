using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    class VersionInfoEntry
    {
        private Dictionary<string, string> data;

        public VersionInfoEntry()
        {
            data = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            data[key] = value;
        }

        public string Get(string key)
        {
            return data.ContainsKey(key) ? data[key] : null;
        }
    }

    class VersionInfo
    {
        private List<VersionInfoEntry> entries;

        public VersionInfo(Stream file)
        {
            Log.Write("VersionInfo construction {");
            entries = new List<VersionInfoEntry>();

            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                int lineIndex = 0;
                List<string> headers = new List<string>();

                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineParts = line.Split('|');

                    Log.Write("    Line: '{0}' Parts: {1} Index: {2}", line, lineParts.Length, lineIndex);

                    if (lineIndex == 0)
                    {
                        // Header.
                        foreach (string header in lineParts)
                        {
                            string[] headerParts = header.Split('!');
                            headers.Add(headerParts[0]);
                        }
                    }
                    else
                    {
                        // Entries.
                        if (lineParts.Length != headers.Count)
                            continue; // Invalid entry.

                        VersionInfoEntry entry = new VersionInfoEntry();

                        for (int i = 0; i < headers.Count; i++)
                            entry.Add(headers[i], lineParts[i]);

                        entries.Add(entry);
                    }

                    lineIndex++;
                }
            }
            Log.Write("} VersionInfo Construction");
        }

        public string this[string key]
        {
            get
            {
                VersionInfoEntry defaultEntry = this[0];

                if (defaultEntry == null)
                    return null;

                return defaultEntry.Get(key);
            }
        }

        public VersionInfoEntry this[int index]
        {
            get
            {
                return index < entries.Count ? entries[index] : null;
            }
        }
    }
}
