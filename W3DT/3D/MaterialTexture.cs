using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class MaterialTexture
    {
        public UInt32 offset { get; set; }
        public UInt32 colour { get; set; }
        public UInt32 flags { get; set; }

        private MaterialTexture(UInt32 offset, UInt32 colour, UInt32 flags)
        {
            this.offset = offset;
            this.colour = colour;
            this.flags = flags;
        }

        public static MaterialTexture Read(FormatBase input)
        {
            return new MaterialTexture(input.readUInt32(), input.readUInt32(), input.readUInt32());
        }
    }
}
