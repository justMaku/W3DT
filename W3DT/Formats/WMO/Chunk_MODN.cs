using System;

namespace W3DT.Formats.WMO
{
    public class Chunk_MODN : Chunk_Base
    {
        // MODN WMO Chunk
        // Contains doodad names for the WMO file.

        public const UInt32 Magic = 0x4D4F444E;
        public StringBlock data { get; private set; }

        public Chunk_MODN(WMOFile file) : base(file, "MODN", Magic)
        {
            data = new StringBlock(file, (int) ChunkSize);
            LogWrite("Loaded " + data.count() + " doodad names.");
        }
    }
}
