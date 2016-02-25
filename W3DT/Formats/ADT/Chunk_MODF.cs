using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public class Chunk_MODF : Chunk_Base
    {
        public struct MODFEntry
        {
            public UInt32 entry;
            public UInt32 uniqueID;
            public Position position;
            public Position rotation; // Using Rotation over Position due to lack of W value.
            public Position lowerBound;
            public Position upperBound;
            public UInt16 flags;
            public UInt16 doodadSet;
            public UInt16 nameSet;
            public UInt16 unk; // Legion?
        }

        // MODF ADT Chunk
        // Information for WMO spawns on the tile.

        public const UInt32 Magic = 0x4D4F4446;
        public MODFEntry[] entries { get; private set; }

        public Chunk_MODF(ADTFile file) : base(file, "MODF", Magic)
        {
            int entryCount = (int)ChunkSize / 64;
            entries = new MODFEntry[entryCount];

            for (int i = 0; i < entryCount; i++)
            {
                MODFEntry entry = new MODFEntry();
                entry.entry = file.readUInt32();
                entry.uniqueID = file.readUInt32();

                float x = Constants.WOW_ADT_MAP_MAX - file.readFloat();
                float z = file.readFloat() - Constants.WOW_ADT_MAP_MAX;

                entry.position = new Position(z, file.readFloat(), x);
                entry.rotation = Position.Read(file);
                entry.lowerBound = Position.Read(file);
                entry.upperBound = Position.Read(file);
                entry.flags = file.readUInt16();
                entry.doodadSet = file.readUInt16();
                entry.nameSet = file.readUInt16();
                entry.unk = file.readUInt16();
                entries[i] = entry;
            }

            LogWrite("Loaded " + entryCount + " WMO spawns");
        }
    }
}
