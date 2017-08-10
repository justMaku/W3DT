using System;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCVT : Chunk_Base, IChunkSoup
    {
        // MCVT ADT Chunk
        // Height data for the terrain of this tile chunk.

        public const UInt32 Magic = 0x4D435654;
        public float[] vertices { get; private set; }

        public Chunk_MCVT(ADTFile file) : base(file, "MCVT", Magic)
        {
            vertices = new float[145];
            for (int i = 0; i < 145; i++)
                vertices[i] = file.readFloat();
        }
    }
}
