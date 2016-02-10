using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCRW : Chunk_Base
    {
        // MCRW ADT Chunk
        // MODF Entries

        public const UInt32 Magic = 0x4D435257;
        public UInt32[] entries { get; private set; }

        public Chunk_MCRW(ADTFile file) : base(file, "MCRW", Magic)
        {
            int nEntry = (int)ChunkSize / 4;
            entries = new UInt32[nEntry];

            for (int i = 0; i < nEntry; i++)
                entries[i] = file.readUInt32();
        }
    }
}
