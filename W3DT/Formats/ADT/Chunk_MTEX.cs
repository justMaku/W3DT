using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MTEX : Chunk_Base
    {
        public const UInt32 Magic = 0x4D544558;
        public StringBlock textures { get; private set; }

        public Chunk_MTEX(ADTFile file) : base(file, "MTEX", Magic)
        {
            textures = new StringBlock(file, (int)ChunkSize);
        }
    }
}
