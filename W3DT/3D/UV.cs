using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class UV
    {
        public float U;
        public float V;

        public UV(float u, float v)
        {
            this.U = u;
            this.V = v;
        }

        public static UV Read(FormatBase input)
        {
            return new UV(input.readFloat(), input.readFloat());
        }
    }
}
