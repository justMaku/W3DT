using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOVI : Chunk_Base, IChunkSoup
    {
        // MOVI WMO Chunk
        // Map verts to faces.

        public const UInt32 Magic = 0x4D4F5649;
        public FacePosition[] positions { get; private set; }

        public Chunk_MOVI(WMOFile file) : base(file, "MOVI", Magic)
        {
            int faceCount = (int)ChunkSize / 6;
            positions = new FacePosition[faceCount];

            for (int i = 0; i < faceCount; i++)
                positions[i] = FacePosition.Read(file);

            LogWrite("Loaded " + faceCount + " vert -> face mappings.");
        }
    }
}
