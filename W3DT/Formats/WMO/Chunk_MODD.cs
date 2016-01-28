using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MODD : Chunk_Base
    {
        // MODD WMO Chunk
        // Contains doodad spawn instances for the WMO.

        public const UInt32 Magic = 0x4D4F4444;
        public Doodad[] doodads { get; private set; }

        public Chunk_MODD(WMOFile file) : base(file, "MODD")
        {
            ChunkID = Magic;

            int doodadCount = (int)ChunkSize / 40;
            doodads = new Doodad[doodadCount];

            for (int i = 0; i < doodadCount; i++)
                doodads[i] = Doodad.Read(file);

            LogWrite("Loaded " + doodadCount + " doodads.");
        }
    }
}
