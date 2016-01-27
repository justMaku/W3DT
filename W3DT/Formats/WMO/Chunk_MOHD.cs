using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOHD : Chunk_Base
    {
        public const UInt32 Magic = 0x4D4F4844;

        public UInt32 nTextures { get; set; }
        public UInt32 nGroups { get; set; }
        public UInt32 nPortals { get; set; }
        public UInt32 nLights { get; set; }
        public UInt32 nDoodadNames { get; set; }
        public UInt32 nDoodadRefs { get; set; }
        public UInt32 nDoodadSets { get; set; }
        public int colourR { get; set; }
        public int colourG { get; set; }
        public int colourB { get; set; }
        public int colourX { get; set; }
        public UInt32 wmoID { get; set; }
        public Position boundingBoxLow { get; set; }
        public Position boundingBoxHigh { get; set; }
        public UInt32 flags { get; set; }

        public Chunk_MOHD(WMOFile file) : base(file, "MOHD")
        {
            ChunkID = Magic;
            Stuffer.Stuff(this, file, GetLogPrefix());
        }
    }
}
