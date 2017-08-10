using System;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    class Chunk_MCVP : Chunk_Base
    {
        // MCVP WMO Chunk
        // Convex colume plane data.

        public const UInt32 Magic = 0x4D435650;
        public C4Plane[] planes { get; private set; }

        public Chunk_MCVP(WMOFile file) : base(file, "MCVP", Magic)
        {
            int planeCount = (int) ChunkSize / 16;
            planes = new C4Plane[planeCount];

            for (int i = 0; i < planeCount; i++)
                planes[i] = C4Plane.Read(file);

            LogWrite("Loaded " + planeCount + " convex volume planes.");
        }
    }
}
