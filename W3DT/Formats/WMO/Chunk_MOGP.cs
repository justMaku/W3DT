using System;
using System.Collections.Generic;
using System.Linq;
using W3DT._3D;

namespace W3DT.Formats.WMO
{
    public class Chunk_MOGP : Chunk_Base, IChunkProvider
    {
        // MOGP WMO Chunk
        // Root chunk. ChunkSize = entire file.

        public const UInt32 Magic = 0x4D4F4750;
        public UInt32 groupNameIndex { get; set; }
        public UInt32 dGroupName { get; set; }
        public UInt32 flags { get; set; }

        public Position boundingBoxLow { get; set; }
        public Position boundingBoxHigh { get; set; }

        public UInt16 moprIndex { get; set; }
        public UInt16 moprCount { get; set; }
        public UInt16 nBatchA { get; set; }
        public UInt16 nBatchInterior { get; set; }
        public UInt32 nBatchExterior { get; set; }

        public byte fog1 { get; set; }
        public byte fog2 { get; set; }
        public byte fog3 { get; set; }
        public byte fog4 { get; set; }

        public UInt32 liquidType { get; set; }
        public UInt32 groupID { get; set; }
        public UInt32 unk1 { get; set; } // 0
        public UInt32 unk2 { get; set; } // 0

        private List<Chunk_Base> subChunks;

        public Chunk_MOGP(WMOFile file) : base(file, "MOGP", Magic)
        {
            subChunks = new List<Chunk_Base>();
            Stuffer.Stuff(this, file, GetLogPrefix());
        }

        public void addChunk(Chunk_Base chunk)
        {
            subChunks.Add(chunk);
            LogWrite(string.Format("Added sub-chunk {0} as sub-chunk; {1} in pool", chunk.GetType().Name, subChunks.Count));
        }

        public Chunk_Base getChunk(UInt32 chunkID, bool error = true)
        {
            Chunk_Base chunk = getChunksByID(chunkID).FirstOrDefault();

            if (chunk == null && error)
                throw new WMOException(string.Format("Chunk does not contain sub-chunk 0x{0}.", chunkID.ToString("X")));

            return chunk;
        }

        public IEnumerable<Chunk_Base> getChunksByID(UInt32 chunkID)
        {
            return subChunks.Where(c => c.ChunkID == chunkID);
        }

        public IEnumerable<Chunk_Base> getChunks()
        {
            return subChunks;
        }
    }
}
