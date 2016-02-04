﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WDT
{
    public class Chunk_MPHD : Chunk_Base
    {
        public const UInt32 Magic = 0x4D504844;

        public UInt32 flags { get; private set; }
        public UInt32 unk1 { get; private set; }

        public Chunk_MPHD(WDTFile file) : base(file, "MPHD")
        {
            ChunkID = Magic;

            flags = file.readUInt32();
            unk1 = file.readUInt32();
            // 4 * 6 bytes of unused data here.
            // No need to skip, seek check higher up will take care of it.

            LogValue("flags", flags);
            LogValue("unk1", unk1);
        }
    }
}