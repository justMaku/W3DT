using System;

namespace W3DT.Formats
{
    interface IChunkedFormat
    {
        Chunk_Base lookupChunk(UInt32 magic);
        string getFormatName();
        void storeChunk(Chunk_Base chunk);
        void flush();
    }
}
