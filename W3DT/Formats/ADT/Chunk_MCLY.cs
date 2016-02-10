using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public struct MCLYLayer
    {
        public UInt32 textureID;
        public UInt32 flags;
        public UInt32 ofsMCAL;
        public UInt32 effectID;

        public MCLYLayer(UInt32 textureID, UInt32 flags, UInt32 ofsMCAL, UInt32 effectID)
        {
            this.textureID = textureID;
            this.flags = flags;
            this.ofsMCAL = ofsMCAL;
            this.effectID = effectID;
        }
    }

    public class Chunk_MCLY : Chunk_Base, IChunkSoup
    {
        // MCLY ADT Chunk
        // Texture layers

        public const UInt32 Magic = 0x4D434C59;
        public MCLYLayer[] layers { get; private set; }

        public Chunk_MCLY(ADTFile file) : base(file, "MCLY", Magic)
        {
            int nLayers = (int)ChunkSize / 16;
            layers = new MCLYLayer[nLayers];

            for (int i = 0; i < nLayers; i++)
                layers[i] = new MCLYLayer(file.readUInt32(), file.readUInt32(), file.readUInt32(), file.readUInt32());
        }
    }
}
