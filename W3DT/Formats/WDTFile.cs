using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT.Formats.WDT;

namespace W3DT.Formats
{
    public class WDTException : Exception
    {
        public WDTException(string message) : base(message) { }
    }

    public class WDTFile : FormatBase
    {
        public string BaseName { get; private set; }
        public List<Chunk_Base> Chunks { get; private set; }

        public WDTFile(string file) : base(file)
        {
            Chunks = new List<Chunk_Base>();
            BaseName = Path.GetFileName(file);
        }

        public void parse()
        {
            while (!isEndOfStream() && !isOutOfBounds(seek + 4))
            {
                UInt32 chunkID = readUInt32();
                Chunk_Base chunk = null;
                int startSeek = seek + 4;

                switch (chunkID)
                {
                    case Chunk_MVER.Magic: chunk = new Chunk_MVER(this); break;
                    default: chunk = new Chunk_Base(this); break;
                }

                if (chunk.ChunkID != 0x0)
                {
                    Chunks.Add(chunk);
                }
                else
                {
                    string hex = chunkID.ToString("X");
                    Log.Write("WDT: Unknown chunk encountered = {0} (0x{1}) at {2} in {3}", chunkID, hex, seek, BaseName);
                }

                // Ensure we're at the right position for the next chunk.
                seekPosition((int)(startSeek + chunk.ChunkSize));
            }
        }
    }
}
