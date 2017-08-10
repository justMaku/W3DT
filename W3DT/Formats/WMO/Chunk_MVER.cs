using System;

namespace W3DT.Formats.WMO
{
    public class Chunk_MVER : Chunk_Base
    {
        // MVER WMO Chunk
        // Contains the WMO version of the file.

        public const UInt32 Magic = 0x4D564552;
        public UInt32 fileVersion { get; set; }

        public Chunk_MVER(WMOFile file) : base(file, "MVER", Magic)
        {
            Stuffer.Stuff(this, file, GetLogPrefix());
        }
    }
}
