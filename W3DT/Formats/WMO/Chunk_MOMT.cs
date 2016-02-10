using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOMT : Chunk_Base
    {
        // MOMT WMO Chunk
        // Material data!

        public const UInt32 Magic = 0x4D4F4D54;
        public Material[] materials;

        public Chunk_MOMT(WMOFile file) : base(file, "MOMT", Magic)
        {
            int materialCount = (int)ChunkSize / 64;
            materials = new Material[materialCount];

            for (int i = 0; i < materialCount; i++)
                materials[i] = Material.Read(file, i);

            LogWrite("Loaded " + materialCount + " materials.");
        }
    }
}
