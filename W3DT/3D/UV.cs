using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class UV
    {
        public float u { get; private set; }
        public float v { get; private set; }

        public UV(float u, float v)
        {
            this.u = u;
            this.v = v;
        }

        public static UV Read(FormatBase input)
        {
            return new UV(input.readFloat(), input.readFloat());
        }
    }
}
