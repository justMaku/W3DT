using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MMDX : Chunk_Base
    {
        public const UInt32 Magic = 0x4D4D4458;
        public StringBlock Models { get; private set; }

        public Chunk_MMDX(ADTFile file) : base(file, "MMDX")
        {
            ChunkID = Magic;
            Models = new StringBlock(file, (int)ChunkSize);
        }
    }
}
