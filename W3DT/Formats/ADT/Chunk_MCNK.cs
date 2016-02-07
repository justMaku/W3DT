using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCNK : Chunk_Base, IChunkProvider
    {
        public const UInt32 Magic = 0x4D434E4B;
        private List<Chunk_Base> Chunks;

        public Chunk_MCNK(ADTFile file) : base(file)
        {
            Chunks = new List<Chunk_Base>();
        }

        public void addChunk(Chunk_Base chunk)
        {
            Chunks.Add(chunk);
            LogWrite(string.Format("Added sub-chunk {0}; {1} in pool", chunk.GetType().Name, Chunks.Count));
        }

        public Chunk_Base getChunk(UInt32 chunkID)
        {
            Chunk_Base chunk = getChunksByID(chunkID).FirstOrDefault();

            if (chunk == null)
                throw new ADTException(string.Format("Chunk does not contain sub-chunk 0x{0}", chunkID.ToString("X")));

            return chunk;
        }

        public IEnumerable<Chunk_Base> getChunksByID(UInt32 chunkID)
        {
            return Chunks.Where(c => c.ChunkID == chunkID);
        }

        public IEnumerable<Chunk_Base> getChunks()
        {
            return Chunks;
        }
    }
}
