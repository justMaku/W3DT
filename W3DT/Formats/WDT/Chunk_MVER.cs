using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WDT
{
    class Chunk_MVER : Chunk_Base
    {
        public const UInt32 Magic = 0x4D564552;
        public UInt32 Version { get; private set; }

        public Chunk_MVER(WDTFile file) : base(file, "MVER")
        {
            ChunkID = Magic;
            Version = file.readUInt32();

            LogWrite(string.Format("Version: {0}", Version));
        }
    }
}
