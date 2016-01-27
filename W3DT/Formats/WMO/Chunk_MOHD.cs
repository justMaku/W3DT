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

        public UInt32 nTextures { get; private set; }
        public UInt32 nGroups { get; private set; }
        public UInt32 nPortals { get; private set; }
        public UInt32 nLights { get; private set; }
        public UInt32 nDoodadNames { get; private set; }
        public UInt32 nDoodadRefs { get; private set; }
        public UInt32 nDoodadSets { get; private set; }
        public UInt32 colourR { get; private set; }
        public UInt32 colourG { get; private set; }
        public UInt32 colourB { get; private set; }
        public UInt32 colourX { get; private set; }
        public UInt32 wmoID { get; private set; }
        public Position boundingBoxLow { get; private set; }
        public Position boundingBoxHigh { get; private set; }
        public UInt32 flags { get; private set; }

        public Chunk_MOHD(WMOFile file) : base(file)
        {
            ChunkID = Magic;

            // ToDo: Populate variables.

            // ToDo: Logging
        }
    }
}
