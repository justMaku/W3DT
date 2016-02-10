using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOTX : Chunk_Base
    {
        // MOTX WMO Chunk
        // Texture paths.

        public const UInt32 Magic = 0x4D4F5458;
        public StringBlock textures { get; private set; }

        public Chunk_MOTX(WMOFile file) : base(file, "MOTX", Magic)
        {
            textures = new StringBlock(file, (int)ChunkSize);
        }
    }
}
