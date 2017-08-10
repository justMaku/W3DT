using System;

namespace W3DT.Formats.ADT
{
    public class Chunk_MWID : Chunk_Base
    {
        // MWID ADT Chunk
        // Offsets of WMO objects found on this tile.

        public const UInt32 Magic = 0x4D574944;
        public UInt32[] offsets { get; private set; }

        public Chunk_MWID(ADTFile file) : base(file, "MWID", Magic)
        {
            int objectCount = (int)ChunkSize / 4;
            offsets = new UInt32[objectCount];

            for (int i = 0; i < objectCount; i++)
                offsets[i] = file.readUInt32();
        }
    }
}
