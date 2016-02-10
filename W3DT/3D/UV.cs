using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public struct UV
    {
        public float u;
        public float v;

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
