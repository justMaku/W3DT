using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MVER : Chunk_Base
    {
        // MVER ADT Chunk
        // File version.

        public const UInt32 Magic = 0x4D564552;
        public UInt32 Version { get; private set; }

        public Chunk_MVER(ADTFile file) : base(file, "MVER", Magic)
        {
            Version = file.readUInt32();

            // Ensure we have a v18 ADT chunk.
            if (Version != 18)
                throw new ADTException("Unsupported ADT version: " + Version);
        }
    }
}
