using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public struct MDDFEntry
    {
        UInt32 entry;
        UInt32 uniqueID;
        Position position;
        Rotation rotation;
        UInt16 scale;
        UInt16 flags;
    }

    public class Chunk_MDDF : Chunk_Base
    {
        public const UInt32 Magic = 0x4D444446;
        public MDDFEntry[] entries { get; private set; }

        public Chunk_MDDF(ADTFile file) : base(file, "MDDF", Magic)
        {
            int entryCount = (int)ChunkSize / 36;
            entries = new MDDFEntry[entryCount];

            for (int i = 0; i < entryCount; i++)
            {
                MDDFEntry entry = new MDDFEntry();
                Stuffer.Stuff(entry, file, GetLogPrefix());
                entries[i] = entry;
            }
        }
    }
}
