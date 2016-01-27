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
                }

                if (chunk != null)
                    chunks.Add(chunk);
                else
                    Log.Write("WMO: Unknown chunk encountered = {0}", chunkID);
            }

            // Check version
            Chunk_Base version = getChunksByID(Chunk_MVER.Magic).FirstOrDefault();

            if (version == null || !(version is Chunk_MVER))
                throw new WMOException("File is not a valid WMO file (missing version header).");

            // WMOv4 required.
            if (((Chunk_MVER)version).FileVersion != 4)
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
