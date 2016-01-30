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
        public Colour4 colour { get; set; }
        public UInt32 flags { get; set; }

        public MaterialTexture(UInt32 offset, Colour4 colour, UInt32 flags)
        {
            this.offset = offset;
            this.colour = colour;
            this.flags = flags;
        }

        public override string ToString()
        {
            return string.Format("offset: {0}, colour: {1}, flags: {2}", offset, colour, flags);
        }

        public static MaterialTexture Read(FormatBase input)
        {
            return new MaterialTexture(input.readUInt32(), Colour4.Read(input), input.readUInt32());
        }
    }
}
