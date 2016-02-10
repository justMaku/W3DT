using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public struct Colour4
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public Colour4(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public override string ToString()
        {
            return string.Format("R: [{0}] G: [{1}] B: [{2}] A: [{3}]", R, G, B, A);
        }

        public static Colour4 Read(FormatBase input)
        {
            return new Colour4(input.readUInt8(), input.readUInt8(), input.readUInt8(), input.readUInt8());
        }
    }
}
