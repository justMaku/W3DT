using System;

namespace W3DT.Formats.ADT
{
    public class Chunk_MMDX : Chunk_Base
    {
        // MMDX ADT Chunk
        // List of models used in this ADT tile.

        public const UInt32 Magic = 0x4D4D4458;
        public StringBlock models { get; private set; }

        public Chunk_MMDX(ADTFile file) : base(file, "MMDX", Magic)
        {
            models = new StringBlock(file, (int)ChunkSize);
        }
    }
}
