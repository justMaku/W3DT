using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    public interface IChunkProvider
    {
        Chunk_Base getChunk(UInt32 chunkID);
        IEnumerable<Chunk_Base> getChunksByID(UInt32 chunkID);
        IEnumerable<Chunk_Base> getChunks();
    }
}
