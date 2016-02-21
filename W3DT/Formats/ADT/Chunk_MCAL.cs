using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCAL : Chunk_Base, IChunkSoup
    {
        public enum CompressType
        {
            UNCOMPRESSED_2048,
            UNCOMPRESSED_4096,
            COMPRESSED
        }

        public const uint Magic = 0x4D43414C;
        private byte[] data;

        public Chunk_MCAL(ADTFile file) : base(file, "MCAL", Magic)
        {
            data = file.readBytes((int)ChunkSize);
        }

        public byte[,] parse(CompressType compress, uint offset, bool fixAlphaMap = false)
        {
            byte[,] alphaMap = new byte[64, 64];
            if (compress == CompressType.COMPRESSED)
            {
                // Compressed.
                uint intOffset = 0;
                while (intOffset < 4096)
                {
                    byte read = data[offset];
                    bool mode = (read & 0x80) == 0x80; // true: Fill, false: copy.
                    int val = read & 0x7F;
                    offset++;

                    for (int v = 0; v < val; v++)
                    {
                        if (intOffset == 4096) break; // Map full. Job's done.
                        byte temp = data[offset];
                        alphaMap[intOffset / 64, intOffset % 64] = temp;
                        intOffset++;

                        if (!mode)
                            offset++;
                    }

                    if (mode)
                        offset++;
                }
            }
            else if (compress == CompressType.UNCOMPRESSED_2048)
            {
                // No compression, 4-bit values.
                uint intOffset = 0;
                for (int i = 0; i < 2048; i++)
                {
                    byte val = data[offset];
                    byte a = (byte)(val & 0x0F);
                    byte b = (byte)((val & 0xF) >> 4);

                    alphaMap[intOffset / 64, intOffset % 64] = (byte) (a * 17);
                    intOffset++;

                    alphaMap[intOffset / 64, intOffset % 64] = (byte)(b * 17);
                    intOffset++;

                    offset++;
                }

                if (fixAlphaMap)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        alphaMap[i, 63] = alphaMap[i, 62];
                        alphaMap[63, i] = alphaMap[62, i];
                    }
                }
            }
            else if (compress == CompressType.UNCOMPRESSED_4096)
            {
                // No compression, full data.
                for (int x = 0; x < 64; x++)
                {
                    for (int y = 0; y < 64; y++)
                    {
                        alphaMap[x, y] = data[offset];
                        offset++;
                    }
                }
            }

            return alphaMap;
        }
    }
}
