using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class FacePosition
    {
        public UInt16 point1 { get; private set; }
        public UInt16 point2 { get; private set; }
        public UInt16 point3 { get; private set; }

        public FacePosition(UInt16 p1, UInt16 p2, UInt16 p3)
        {
            point1 = p1;
            point2 = p2;
            point3 = p3;
        }

        public static FacePosition Read(FormatBase input)
        {
            return new FacePosition(input.readUInt16(), input.readUInt16(), input.readUInt16());
        }
    }
}
