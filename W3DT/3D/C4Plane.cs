using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class C4Plane
    {
        public float C1 { get; private set; }
        public float C2 { get; private set; }
        public float C3 { get; private set; }
        public float C4 { get; private set; }

        public C4Plane(float c1, float c2, float c3, float c4)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
        }

        public static C4Plane Read(FormatBase input)
        {
            return new C4Plane(input.readFloat(), input.readFloat(), input.readFloat(), input.readFloat());
        }
    }
}
