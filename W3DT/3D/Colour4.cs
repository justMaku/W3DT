using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class Colour4
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
        public int a { get; set; }

        public Colour4(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static Colour4 Read(FormatBase input)
        {
            return new Colour4(input.readUInt8(), input.readUInt8(), input.readUInt8(), input.readUInt8());
        }
    }
}
