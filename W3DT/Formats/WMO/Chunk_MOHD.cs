using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOHD : Chunk_Base
    {
        public const UInt32 Magic = 0x4D4F4844;

        public UInt32 nTextures { get; private set; }
        public UInt32 nGroups { get; private set; }
        public UInt32 nPortals { get; private set; }
        public UInt32 nLights { get; private set; }
        public UInt32 nDoodadNames { get; private set; }
        public UInt32 nDoodadRefs { get; private set; }
        public UInt32 nDoodadSets { get; private set; }
        public int colourR { get; private set; }
        public int colourG { get; private set; }
        public int colourB { get; private set; }
        public int colourX { get; private set; }
        public UInt32 wmoID { get; private set; }
        public Position boundingBoxLow { get; private set; }
        public Position boundingBoxHigh { get; private set; }
        public UInt32 flags { get; private set; }

        public Chunk_MOHD(WMOFile file) : base(file, "MOHD")
        {
            ChunkID = Magic;

            nTextures = file.readUInt32();
            nGroups = file.readUInt32();
            nPortals = file.readUInt32();
            nLights = file.readUInt32();
            nDoodadNames = file.readUInt32();
            nDoodadRefs = file.readUInt32();
            nDoodadSets = file.readUInt32();
            colourR = file.readUInt8();
            colourG = file.readUInt8();
            colourB = file.readUInt8();
            colourX = file.readUInt8();
            wmoID = file.readUInt32();
            boundingBoxLow = Position.Read(file);
            boundingBoxHigh = Position.Read(file);
            flags = file.readUInt32();

            LogValue("nTextures", nTextures);
            LogValue("nGroups", nGroups);
            LogValue("nPortals", nPortals);
            LogValue("nLights", nLights);
            LogValue("nDoodadNames", nDoodadNames);
            LogValue("nDoodadRefs", nDoodadRefs);
            LogValue("nDoodadSets", nDoodadSets);
            LogValue("colourR", colourR);
            LogValue("colourG", colourG);
            LogValue("colourB", colourB);
            LogValue("colourX", colourX);
            LogValue("wmoID", wmoID);
            LogValue("boundingBoxLow", boundingBoxLow);
            LogValue("boundingBoxHigh", boundingBoxHigh);
            LogValue("flags", flags);
        }
    }
}
