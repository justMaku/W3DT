﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_MVER : Chunk_Base
    {
        public const UInt32 Magic = 0x4D564552;
        public UInt32 fileVersion { get; set; }

        public Chunk_MVER(WMOFile file) : base(file, "MVER")
        {
            ChunkID = Magic;
            Stuffer.Stuff(this, file, GetLogPrefix());
        }
    }
}
