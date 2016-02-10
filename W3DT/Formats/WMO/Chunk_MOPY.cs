using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOPY : Chunk_Base, IChunkSoup
    {
        // MOPY WMO Chunk
        // Defines data about faces.

        public const UInt32 Magic = 0x4D4F5059;
        public FaceInfo[] faceInfo { get; private set; }

        public Chunk_MOPY(WMOFile file) : base(file, "MOPY", Magic)
        {
            int infoCount = (int)ChunkSize / 2;
            faceInfo = new FaceInfo[infoCount];

            for (int i = 0; i < infoCount; i++)
                faceInfo[i] = FaceInfo.Read(file);

            LogWrite("Loaded info for " + infoCount + " faces.");
        }
    }
}
