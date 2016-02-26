using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3DT.Formats
{
    public class M2Exception : Exception
    {
        public M2Exception(string message) : base(message) { }
    }

    public class M2File : FormatBase
    {
        private static uint MD21_MAGIC = 0x3132444D;
        private static uint MD20_MAGIC = 0x3032444D;

        public uint Version { get; private set; }
        public string Name { get; private set; }

        public M2File(string file) : base(file) { }

        public override void parse()
        {
            int ofs = 0;
            uint magic = readUInt32();

            if (!magic.Equals(MD20_MAGIC)) // Assume Legion+ chunked format, relate offset to chunk.
            {
                seekPosition(0); // Go back to the start.
                while (!isEndOfStream() && !isOutOfBounds(seek + 8))
                {
                    uint chunkID = readUInt32();
                    uint chunkSize = readUInt32();

                    if (chunkID == MD21_MAGIC)
                    {
                        ofs = seek;
                        break;
                    }
                    else
                    {
                        skip((int)chunkSize);
                    }
                }

                if (ofs == 0)
                    throw new M2Exception("Unable to find MD20 chunk inside MD21 file! Format evolved?");
            }

            // Start reading as MD20
            seekPosition(ofs); // Seek the beginning of the MD20 data.
            magic = readUInt32();

            if (magic != MD20_MAGIC)
                throw new M2Exception(string.Format("Data is not MD20 at {0}", ofs));

            /* 
             * 274+ Legion
             * ? - ? Warlords of Draenor
             * 273 Mists of Pandaria
             * 265-272 Cataclysm
             * 264 Wrath of the Lich King
             * 260-263 The Burning Crusade
             * ?-256 World of Warcraft
             */
            Version = readUInt32();
            int iName = (int)readUInt32(); // Contains trailing 0-byte.
            int ofsName = (int)readUInt32();

            seekPosition(ofs + ofsName);
            Name = readString(iName - 1);
        }
    }
}
