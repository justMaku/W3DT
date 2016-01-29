using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class Doodad
    {
        public UInt32 offset { get; private set; }
        public byte flags { get; set; }
        public Position position { get; set; }
        public Rotation rotation { get; set; }
        public float scale { get; set; }
        public Colour4 colour { get; set; }

        public static Doodad Read(FormatBase input)
        {
            Doodad temp = new Doodad();

            // 24-bit offset?
            byte[] b = input.readBytes(3);
            temp.offset = BitConverter.ToUInt32(new byte[4] { b[0], b[1], b[2], 0 }, 0);
            Stuffer.Stuff(temp, input, null, true);

            return temp;
        }
    }
}
