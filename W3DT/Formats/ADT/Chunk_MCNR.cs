using System;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCNR : Chunk_Base, IChunkSoup
    {
        // MCNR ADT Chunk
        // Normals

        public const UInt32 Magic = 0x4D434E52;
        public Position[] normals { get; private set; }

        public Chunk_MCNR(ADTFile file) : base(file, "MCNR", Magic)
        {
            normals = new Position[145];
            for (int i = 0; i < 145; i++)
            {
                float x = file.readUInt8() / 127;
                float z = file.readUInt8() / 127;
                float y = file.readUInt8() / 127;

                // Super-hacky fix? This does not seem right, but it prevents lighting issues?
                if (y == 1.0 && z == 0.0)
                {
                    z = 1.0f;
                    y = 0.0f;
                }

                normals[i] = new Position(x, y, z);
            }
        }
    }
}
