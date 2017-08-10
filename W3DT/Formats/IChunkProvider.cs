using System;
using System.Collections.Generic;

namespace W3DT.Formats
{
    public interface IChunkProvider
    {
        Chunk_Base getChunk(UInt32 chunkID, bool error);
        IEnumerable<Chunk_Base> getChunksByID(UInt32 chunkID);
        IEnumerable<Chunk_Base> getChunks();
    }
}
