using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public class CASCFolder : ICASCEntry, IComparable<ICASCEntry>
    {
        public Dictionary<ulong, ICASCEntry> SubEntries;
        private ulong hash;

        public CASCFolder(ulong hash)
        {
            SubEntries = new Dictionary<ulong, ICASCEntry>();
            this.hash = hash;
        }

        public string Name
        {
            get { return CASCEngine.FolderNames[hash]; }
        }

        public ulong Hash
        {
            get { return hash; }
        }

        public ICASCEntry GetEntry(ulong hash)
        {
            if (!SubEntries.ContainsKey(hash))
                return null;
            return SubEntries[hash];
        }

        public int CompareTo(ICASCEntry other)
        {
            if (other is CASCFolder)
                return Name.CompareTo(other.Name);
            else
                return this is CASCFolder ? -1 : 1;
        }
    }
}
