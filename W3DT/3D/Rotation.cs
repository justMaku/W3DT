using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public struct Rotation
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Rotation(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Rotation Read(FormatBase input)
        {
            return new Rotation(input.readFloat(), input.readFloat(), input.readFloat(), input.readFloat());
        }

        public override string ToString()
        {
            return string.Format("X: [{0}] Y: [{1}] Z: [{2}] W: [{3}]", x, y, z, w);
        }
    }
}
