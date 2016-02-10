using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT._3D;

namespace W3DT.Formats.ADT
{
    public class Chunk_MCNK : Chunk_Base, IChunkProvider
    {
        // MCNK ADT Chunk
        // Header (has sub-chunks, fields only if this chunk is in the root)

        public const UInt32 Magic = 0x4D434E4B;
        private List<Chunk_Base> Chunks;

        public UInt32 flags { get; set; }
        public UInt32 indexX { get; set; }
        public UInt32 indexY { get; set; }
        public UInt32 nLayers { get; set; }
        public UInt32 nDoodadRefs { get; set; }
        public UInt32 ofsHeight { get; set; } // MCVT
        public UInt32 ofsNormal { get; set; } // MCNR
        public UInt32 ofsLayer { get; set; } // MCLY
        public UInt32 ofsRefs { get; set; } // MCRF
        public UInt32 ofsAlpha { get; set; } // MCAL
        public UInt32 sizeAlpha { get; set; }
        public UInt32 ofsShadow { get; set; } // MCSH
        public UInt32 sizeShadow { get; set; }
        public UInt32 areaID { get; set; }
        public UInt32 nMapObjRefs { get; set; }
        public UInt16 holes { get; set; }
        public UInt16 unk1 { get; set; }
        public UInt32[] lqTexMap { get; set; } // uint2[8][8]. Hannah is lazy.
        public UInt32 predTex { get; set; }
        public UInt32 noEffectDoodad { get; set; }
        public UInt32 ofsSndEmitters { get; set; } // MCSE
        public UInt32 nSndEmitters { get; set; }
        public UInt32 ofsLiquid { get; set; } // MCLQ
        public UInt32 sizeLiquid { get; set; }
        public Position position { get; set; }
        public UInt32 ofsMCCV { get; set; } // MCCV
        public UInt32 ofsMCLV { get; set; } // MCLV
        public UInt32 unk2 { get; set; } // Unused

        public Chunk_MCNK(ADTFile file) : base(file, "MCNK", Magic)
        {
            Chunks = new List<Chunk_Base>();

            if (file.Type == ADTFileType.ROOT) // Only root has the header.
            {
                lqTexMap = new UInt32[4]; // Assign a size so the stuffer knows what to do.
                Stuffer.Stuff(this, file, GetLogPrefix(), true);
            }
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
