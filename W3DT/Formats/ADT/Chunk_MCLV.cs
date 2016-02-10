using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCLV : Chunk_Base, IChunkSoup
    {
        // MCLV ADT Chunk
        // Vertex shading (+light)

        public const UInt32 Magic = 0x4D434C56;
        public Colour4[] colours { get; private set; }

        public Chunk_MCLV(ADTFile file) : base(file, "MCLV", Magic)
        {
            colours = new Colour4[145];
            for (int i = 0; i < 145; i++)
                colours[i] = Colour4.Read(file);
        }
    }
}
