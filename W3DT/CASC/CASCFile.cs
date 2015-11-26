using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    public class CASCFile : ICASCEntry, IComparable<ICASCEntry>
    {
        private ulong hash;

        public CASCFile(ulong hash)
        {
            this.hash = hash;
        }

        public string Name
        {
            get { return Path.GetFileName(FullName); }
        }

        public string FullName
        {
            get { return CASCEngine.FileNames[hash]; }
        }

        public ulong Hash
        {
            get { return hash; }
        }

        public int CompareTo(ICASCEntry other)
        {
            if (other is CASCFile)
                return Name.CompareTo(other.Name);
            else
                return this is CASCFile ? 1 : -1;
        }
    }
}
