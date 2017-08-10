using System;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MONR : Chunk_Base, IChunkSoup
    {
        // MONR WMO Chunk
        // Define normals!

        public const UInt32 Magic = 0x4D4F4E52;
        public Position[] normals { get; private set; }

        public Chunk_MONR(WMOFile file) : base(file, "MONR", Magic)
        {
            int normalCount = (int)ChunkSize / 12;
            normals = new Position[normalCount];

            for (int i = 0; i < normalCount; i++)
            {
                float x = file.readFloat();
                float z = file.readFloat() * -1;
                float y = file.readFloat();

                normals[i] = new Position(x, y, z);
            }

            LogWrite("Loaded " + normalCount + " normals.");
        }
    }
}
