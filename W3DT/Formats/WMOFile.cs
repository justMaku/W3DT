using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats.WMO;

namespace W3DT.Formats
{
    public class WMOException : Exception
    {
        public WMOException(string message) : base(message) { }
    }

    public class WMOFile : FormatBase
    {
        public WMOFile(string path) : base(path)
        {
            chunks = new List<Chunk_Base>();
            readChunks();
        }

        private void readChunks()
        {
            while (!isEndOfStream())
            {
                UInt32 chunkID = readUInt32();
                Chunk_Base chunk = null;
                int startSeek = seek + 4;

                switch (chunkID)
                {
                    case Chunk_MVER.Magic: chunk = new Chunk_MVER(this); break;
                    case Chunk_MOHD.Magic: chunk = new Chunk_MOHD(this); break;
                    default: chunk = new Chunk_Base(this); break;
                }

                if (chunk.ChunkID == 0x0)
                    Log.Write("WMO: Unknown chunk encountered = {0}", chunkID);
                else
                    chunks.Add(chunk);

                seekPosition((int) (startSeek + chunk.ChunkSize));
            }

            // Check version
            Chunk_Base version = getChunksByID(Chunk_MVER.Magic).FirstOrDefault();

            if (version == null || !(version is Chunk_MVER))
                throw new WMOException("File is not a valid WMO file (missing version header).");

            // WMOv4 required.
            if (((Chunk_MVER)version).fileVersion != 17)
                throw new WMOException("Unsupported WMO version!");
        }

        public IEnumerable<Chunk_Base> getChunksByID(UInt32 id)
        {
            return chunks.Where(c => c.ChunkID == id);
        }

        public List<Chunk_Base> getChunks()
        {
            return chunks;
        }

        private List<Chunk_Base> chunks;
    }
}
