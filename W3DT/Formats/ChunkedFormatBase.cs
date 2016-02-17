using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    abstract public class ChunkedFormatBase : FormatBase, IChunkedFormat
    {
        public abstract string getFormatName();
        public abstract Chunk_Base lookupChunk(UInt32 magic);
        public abstract void storeChunk(Chunk_Base chunk);

        public List<Chunk_Base> Chunks { get; private set; }

        public ChunkedFormatBase(string path) : base(path)
        {
            Chunks = new List<Chunk_Base>();
        }

        public override void parse()
        {
            while (!isEndOfStream() && !isOutOfBounds(seek + 4))
            {
                int startSeek = seek + 8;
                Chunk_Base chunk = lookupChunk(readUInt32());

                if (chunk.ChunkID != 0x0)
                {
                    storeChunk(chunk);
                }
                else
                {
                    string hex = chunk.ChunkID.ToString("X");
                    Log.Write(getFormatName() + ": Unknown chunk encountered = {0} (0x{1}) at {2} in {3}", chunk.ChunkID, hex, seek, BaseName);
                }

                // Ensure we're at the right position for the next chunk.
                if (!(chunk is IChunkProvider))
                    seekPosition((int)(startSeek + chunk.ChunkSize));

                if (chunk is ADT.Chunk_MCNR)
                    skip(13); // 0 112 245 18 0 0 0 0 84 245 18 0 ???
            }
        }

        public void flush()
        {
            Chunks.Clear();
        }
    }
}
