using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace W3DT.CASC
{
    public class CASCFile : ICASCEntry
    {
        private ulong hash;

        public CASCFile(ulong hash)
        {
            this.hash = hash;
        }

        public String Name => Path.GetFileName(FullName);
        public ulong Hash => hash;

        public string FullName
        {
            get { return FileNames[hash]; }
            set { FileNames[hash] = value; }
        }

        public int GetSize(CASCEngine casc)
        {
            EncodingEntry entry;
            return casc.GetEncodingEntry(hash, out entry) ? entry.Size : 0;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(ICASCEntry other, int col, CASCEngine casc)
        {
            int result = 0;

            if (other is CASCFolder)
                return 1;

            switch (col)
            {
                case 0:
                    result = Name.CompareTo(other.Name);
                    break;
                case 1:
                    result = Path.GetExtension(Name).CompareTo(Path.GetExtension(other.Name));
                    break;
                case 2:
                    {
                        var e1 = casc.RootHandler.GetEntries(Hash);
                        var e2 = casc.RootHandler.GetEntries(other.Hash);

                        var flags1 = e1.Any() ? e1.First().LocaleFlags : LocaleFlags.None;
                        var flags2 = e2.Any() ? e2.First().LocaleFlags : LocaleFlags.None;

                        result = flags1.CompareTo(flags2);
                    }
                    break;
                case 3:
                    {
                        var e1 = casc.RootHandler.GetEntries(Hash);
                        var e2 = casc.RootHandler.GetEntries(other.Hash);

                        var flags1 = e1.Any() ? e1.First().ContentFlags : ContentFlags.None;
                        var flags2 = e2.Any() ? e2.First().ContentFlags : ContentFlags.None;

                        result = flags1.CompareTo(flags2);
                    }
                    break;
                case 4:
                    var size1 = GetSize(casc);
                    var size2 = (other as CASCFile).GetSize(casc);

                    if (size1 == size2)
                        result = 0;
                    else
                        result = size1 < size2 ? -1 : 1;
                    break;
            }

            return result;
        }

        public static readonly Dictionary<ulong, string> FileNames = new Dictionary<ulong, string>();
    }
}
