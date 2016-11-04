using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public class CASCFolder : ICASCEntry
    {
        private string name;
        public Dictionary<string, ICASCEntry> Entries { get; set; }

        public CASCFolder(string name)
        {
            Entries = new Dictionary<string, ICASCEntry>(StringComparer.OrdinalIgnoreCase);
            this.name = name;
        }

        public string Name => name;
        public ulong Hash => 0;

        public ICASCEntry GetEntry(string name)
        {
            ICASCEntry entry;
            Entries.TryGetValue(name, out entry);
            return entry;
        }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<CASCFile> GetFiles(IEnumerable<ICASCEntry> entries, IEnumerable<int> selection = null, bool recursive = true)
        {
            if (selection != null)
            {
                foreach (int index in selection)
                {
                    var entry = entries.ElementAt(index);

                    if (entry is CASCFile)
                    {
                        yield return entry as CASCFile;
                    }
                    else
                    {
                        if (recursive)
                        {
                            var folder = entry as CASCFolder;
                            foreach (var file in GetFiles(folder.Entries.Select(kv => kv.Value)))
                                yield return file;
                        }
                    }
                }
            }
            else
            {
                foreach (var entry in entries)
                {
                    if (entry is CASCFile)
                    {
                        yield return entry as CASCFile;
                    }
                    else
                    {
                        if (recursive)
                        {
                            var folder = entry as CASCFolder;
                            foreach (var file in GetFiles(folder.Entries.Select(kv => kv.Value)))
                                yield return file;
                        }
                    }
                }
            }
        }

        public int CompareTo(ICASCEntry other, int col, CASCEngine casc)
        {
            int result = 0;

            if (other is CASCFile)
                return -1;

            switch (col)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    result = Name.CompareTo(other.Name);
                    break;
                case 4:
                    break;
            }

            return result;
        }

        public int CompareTo(ICASCEntry other)
        {
            throw new NotImplementedException();
        }
    }
}
