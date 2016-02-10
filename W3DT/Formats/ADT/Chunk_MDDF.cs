using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public struct MDDFEntry
    {
        public UInt32 entry;
        public UInt32 uniqueID;
        public Position position;
        public Rotation rotation;
        public UInt16 scale;
        public UInt16 flags;
    }

    public class Chunk_MDDF : Chunk_Base
    {
        // MDDF AFT Chunk
        // Spawn information for doodads.

        public const UInt32 Magic = 0x4D444446;
        public MDDFEntry[] entries { get; private set; }

        public Chunk_MDDF(ADTFile file) : base(file, "MDDF", Magic)
        {
            int entryCount = (int)ChunkSize / 36;
            entries = new MDDFEntry[entryCount];

            for (int i = 0; i < entryCount; i++)
            {
                MDDFEntry entry = new MDDFEntry();
                entry.entry = file.readUInt32();
                entry.uniqueID = file.readUInt32();
                entry.position = Position.Read(file);
                entry.rotation = Rotation.Read(file);
                entry.scale = file.readUInt16();
                entry.flags = file.readUInt16();
            }

            LogWrite("Loaded " + entryCount + " doodad spawns");
        }
    }
}
