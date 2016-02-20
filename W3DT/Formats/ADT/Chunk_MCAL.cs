using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCAL : Chunk_Base, IChunkSoup
    {
        public const uint Magic = 0x4D43414C;
        private byte[] data;

        public Chunk_MCAL(ADTFile file) : base(file, "MCAL", Magic)
        {
            data = file.readBytes((int)ChunkSize);
        }
    }
}
