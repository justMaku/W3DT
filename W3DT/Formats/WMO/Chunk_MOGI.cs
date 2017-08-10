using System;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOGI : Chunk_Base
    {
        // MOGI WMO Chunk
        // Defines group information for the WMO file.

        public const UInt32 Magic = 0x4D4F4749;
        public GroupInformation[] groups { get; private set; }

        public Chunk_MOGI(WMOFile file) : base(file, "MOGI", Magic)
        {
            int groupCount = (int)ChunkSize / 32;
            groups = new GroupInformation[groupCount];

            for (int i = 0; i < groupCount; i++)
                groups[i] = GroupInformation.Read(file);

            LogWrite("Loaded " + groupCount + " group information modules.");
        }
    }
}
