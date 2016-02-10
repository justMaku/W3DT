using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WDT
{
    public class Chunk_MAIN : Chunk_Base
    {
        public const UInt32 Magic = 0x4D41494E;
        public bool[,] map { get; private set; }

        public Chunk_MAIN(WDTFile file) : base(file, "MAIN", Magic)
        {
            map = new bool[64,64];
            int tileCount = 0;

            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    UInt32 flags = file.readUInt32();
                    file.skip(4); // Runtime area

                    if (flags == 1)
                    {
                        map[x, y] = true;
                        tileCount++;
                    }
                    else
                    {
                        map[x, y] = false;
                    }
                }
            }

            LogWrite(string.Format("Loaded ADT map structure with {0} tiles (out of possible 4096).", tileCount));
        }
    }
}
