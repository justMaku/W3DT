using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOTV : Chunk_Base, IChunkSoup
    {
        // MOTV WMO Chunk
        // Defines UV data.

        public const UInt32 Magic = 0x4D4F5456;
        public UV[] uvData { get; private set; }

        public Chunk_MOTV(WMOFile file) : base(file, "MOTV")
        {
            ChunkID = Magic;

            int uvCount = (int)ChunkSize / 8;
            uvData = new UV[uvCount];

            for (int i = 0; i < uvCount; i++)
                uvData[i] = UV.Read(file);

            LogWrite("Loaded " + uvCount + " UVs.");
        }
    }
}
