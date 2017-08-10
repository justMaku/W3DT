using System;

namespace W3DT.Formats.ADT
{
    public class Chunk_MWMO : Chunk_Base
    {
        // MWMO ADT Chunk
        // List of WMO objects used in this tile.

        public const UInt32 Magic = 0x4D574D4F;
        public StringBlock objects { get; private set; }

        public Chunk_MWMO(ADTFile file) : base(file, "MWMO", Magic)
        {
            objects = new StringBlock(file, (int)ChunkSize);
        }
    }
}
