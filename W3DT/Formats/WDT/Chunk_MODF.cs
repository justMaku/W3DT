using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WDT
{
    public class Chunk_MODF : Chunk_Base
    {
        // MODF WDT Chunk
        // Placement information for a global WMO object.

        public const UInt32 Magic = 0x4D4F4446;

        public UInt32 id { get; set; }
        public UInt32 instanceID { get; set; }
        public Position position { get; set; }
        public Rotation rotation { get; set; }
        public Position upperExtent { get; set; }
        public Position lowerExtend { get; set; }
        public UInt16 flags { get; set; }
        public UInt16 doodadIndex { get; set; }
        public UInt16 nameSet { get; set; }

        public Chunk_MODF(WDTFile file) : base(file, "MODF", Magic)
        {
            Stuffer.Stuff(this, file, GetLogPrefix());

            // There are 2 bytes (UInt16) of padding here, but we should
            // be safe to ignore as chunks are parsed by size.
        }
    }
}
