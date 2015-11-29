using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public class CASCFolder : ICASCEntry
    {
        private string _name;
        private ulong _hash;

        public Dictionary<string, ICASCEntry> Entries { get; set; }

        public CASCFolder(string name)
        {
            Entries = new Dictionary<string, ICASCEntry>(StringComparer.OrdinalIgnoreCase);
            _name = name;
            _hash = 0;
        }

        public string Name
        {
            get { return _name; }
        }

        public ulong Hash
        {
            get { return _hash; }
        }

        public ICASCEntry GetEntry(string name)
        {
            ICASCEntry entry;
            Entries.TryGetValue(name, out entry);
            return entry;
        }

        public IEnumerable<CASCFile> GetFiles(IEnumerable<int> selection = null, bool recursive = true)
        {
            if (selection != null)
            {
                foreach (int index in selection)
                {
                    var entry = Entries.ElementAt(index);

                    if (entry.Value is CASCFile)
                    {
                        yield return entry.Value as CASCFile;
                    }
                    else
                    {
                        if (recursive)
                        {
                            foreach (var file in (entry.Value as CASCFolder).GetFiles())
                            {
                                yield return file;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var entry in Entries)
                {
                    if (entry.Value is CASCFile)
                    {
                        yield return entry.Value as CASCFile;
                    }
                    else
                    {
                        if (recursive)
                        {
                            foreach (var file in (entry.Value as CASCFolder).GetFiles())
                            {
                                yield return file;
                            }
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
