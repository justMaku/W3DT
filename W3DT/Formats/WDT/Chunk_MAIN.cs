using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WDT
{
    public class Chunk_MAIN : Chunk_Base
    {
        public const UInt32 Magic = 0x4D41494E;
        public bool[] map { get; private set; }

        public Chunk_MAIN(WDTFile file) : base(file, "MAIN")
        {
            ChunkID = Magic;

            map = new bool[4096];
            int tileCount = 0;

            for (int i = 0; i < 4096; i++)
            {
                UInt32 flags = file.readUInt32();
                file.skip(4); // Runtime area

                if (flags == 1)
                {
                    map[i] = true;
                    tileCount++;
                }
                else
                {
                    map[i] = false;
                }
            }

            LogWrite(string.Format("Loaded ADT map structure with {0} tiles (out of possible 4096).", tileCount));
        }
    }
}
