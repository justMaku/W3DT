using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MMID : Chunk_Base
    {
        // MMID ADT Chunk
        // Offsets of the models used in this tile.

        public const UInt32 Magic = 0x4D4D4944;
        public UInt32[] offsets { get; private set; }

        public Chunk_MMID(ADTFile file) : base(file, "MMID", Magic)
        {
            int offsetCount = (int)ChunkSize / 4;
            offsets = new UInt32[offsetCount];

            for (int i = 0; i < offsetCount; i++)
                offsets[i] = file.readUInt32();
        }
    }
}
