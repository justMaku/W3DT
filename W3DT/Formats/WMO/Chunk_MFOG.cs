using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MFOG : Chunk_Base
    {
        public const UInt32 Magic = 0x4D464F47;
        public FogInfo[] entries { get; private set; }

        public Chunk_MFOG(WMOFile file) : base(file, "MFOG")
        {
            ChunkID = Magic;

            int entryCount = (int)ChunkSize / 48;
            entries = new FogInfo[entryCount];

            for (int i = 0; i < entryCount; i++)
                entries[i] = FogInfo.Read(file);

            LogWrite("Loaded " + entries + " fog info entries.");
        }
    }
}
