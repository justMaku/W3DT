using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_MODN : Chunk_Base
    {
        public const UInt32 Magic = 0x4D4F444E;
        public StringBlock data { get; private set; }

        public Chunk_MODN(WMOFile file) : base(file, "MODN")
        {
            ChunkID = Magic;
            data = new StringBlock(file, (int) ChunkSize);
        }
    }
}
