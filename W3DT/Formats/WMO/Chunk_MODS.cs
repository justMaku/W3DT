using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MODS : Chunk_Base
    {
        // MODS WMO Chunk
        // Defines sets of doodads.

        public const UInt32 Magic = 0x4D4F4453;
        public DoodadSet[] sets { get; private set; }

        public Chunk_MODS(WMOFile file) : base(file, "MODS")
        {
            ChunkID = Magic;

            int setCount = (int)ChunkSize / 32;
            sets = new DoodadSet[setCount];

            for (int i = 0; i < setCount; i++)
            {
                sets[i] = DoodadSet.Read(file);
                file.skip(4); // Unused
            }
        }
    }
}
