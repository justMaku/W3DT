using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public interface ICASCEntry : IComparable<ICASCEntry>
    {
        string Name { get; }
        ulong Hash { get; }
    }
}
