using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3DT.Formats
{
    public class M2Exception : Exception
    {
        public M2Exception(string message) : base(message) { }
    }

    public class M2File : FormatBase
    {
        public M2File(string file) : base(file) { }

        public override void parse()
        {
            string magic = readString(4);
            if (magic.Equals("MD20"))
            {
                // Pre-Legion
            }
        }
    }
}
