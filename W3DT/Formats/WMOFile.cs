using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT.Formats.WMO;

namespace W3DT.Formats
{
    public class WMOException : Exception
    {
        public WMOException(string message) : base(message) { }
    }

    public class WMOFile : FormatBase
    {
        public string baseName { get; private set; }
        private List<Chunk_Base> chunks;
        private List<WMOFile> groupFiles;
        private bool isRoot;

        public WMOFile(string path, bool isRoot = true) : base(path)
        {
            this.isRoot = isRoot;
            chunks = new List<Chunk_Base>();

            if (isRoot)
                groupFiles = new List<WMOFile>();

            baseName = Path.GetFileNameWithoutExtension(path);
        }

        public void addGroupFile(WMOFile file)
        {
            // Group files can only be inside root files.
            if (isRoot)
                groupFiles.Add(file);
        }

        public void parse()
        {
            Chunk_MOGP groupRoot = null;

            while (!isEndOfStream() && !isOutOfBounds(seek + 4))
            {
                UInt32 chunkID = readUInt32();
                string skip = null;
                Chunk_Base chunk = null;
                int startSeek = seek + 4;

                switch (chunkID)
                {
                    case Chunk_MVER.Magic: chunk = new Chunk_MVER(this); break;
                    case Chunk_MOHD.Magic: chunk = new Chunk_MOHD(this); break;
                    case Chunk_MCVP.Magic: chunk = new Chunk_MCVP(this); break;
                    case Chunk_MFOG.Magic: chunk = new Chunk_MFOG(this); break;
                    case Chunk_MODD.Magic: chunk = new Chunk_MODD(this); break;
                    case Chunk_MODN.Magic: chunk = new Chunk_MODN(this); break;
                    case Chunk_MODS.Magic: chunk = new Chunk_MODS(this); break;
                    case Chunk_MOGI.Magic: chunk = new Chunk_MOGI(this); break;
                    case Chunk_MOGN.Magic: chunk = new Chunk_MOGN(this); break;
                    case Chunk_MOLT.Magic: chunk = new Chunk_MOLT(this); break;
                    case Chunk_MOMT.Magic: chunk = new Chunk_MOMT(this); break;
                    case Chunk_MONR.Magic: chunk = new Chunk_MONR(this); break;
                    case Chunk_MOPY.Magic: chunk = new Chunk_MOPY(this); break;
                    case Chunk_MOTV.Magic: chunk = new Chunk_MOTV(this); break;
                    case Chunk_MOTX.Magic: chunk = new Chunk_MOTX(this); break;
                    case Chunk_MOVI.Magic: chunk = new Chunk_MOVI(this); break;
                    case Chunk_MOVT.Magic: chunk = new Chunk_MOVT(this); break;
                    case 0x4D4F4241: skip = "MOBA (render batches)"; break;
                    case 0x4D4F4C52: skip = "MOLR (light references)"; break;
                    case 0x4D4F4452: skip = "MODR (doodad references)"; break;
                    case 0x4D4F424E: skip = "MOBN (BSP nodes)"; break;
                    case 0x4D4F4252: skip = "MOBR (CAaBSP MOBN)"; break;
                    case 0x4D4F4357: skip = "MOCV (vertex colours)"; break;
                    case 0x4D4C4952: skip = "MLIQ (liquids)"; break;
                    case 0x4D4F5249: skip = "MORI (???)"; break;
                    case 0x4D4F5242: skip = "MORB (???)"; break;
                    case 0x4D4F5441: skip = "MOTA (map object tangent array)"; break;
                    case 0x4D4F4253: skip = "MOBS (???)"; break;
                    case 0x4D4F504C: skip = "MOPL (plane holes)"; break;

                    case Chunk_MOGP.Magic:
                        groupRoot = new Chunk_MOGP(this);
                        chunk = groupRoot;
                        break;

                    default: chunk = new Chunk_Base(this); break;
                }

                if (skip != null)
                {
                    Log.Write("WMO: Skipping chunk " + skip);
                    seekPosition((int) (startSeek + readUInt32()));
                    continue;
                }

                if (chunk.ChunkID != 0x0)
                {
                    if (chunk is IChunkSoup)
                    {
                        if (groupRoot != null)
                            groupRoot.addChunk(chunk);
                        else
                            Log.Write("WMO: MOGP sub-chunk found before the MOGP chunk? Darkness! Madness!");
                    }
                    else
                    {
                        chunks.Add(chunk);
                    }
                }
                else
                {
                    string hex = chunkID.ToString("X");
                    Log.Write("WMO: Unknown chunk encountered = {0} (0x{1}) at {2} in {3}", chunkID, hex, seek, baseName);
                }

                if (!(chunk is Chunk_MOGP))
                    seekPosition((int) (startSeek + chunk.ChunkSize));
            }

            if (isRoot)
            {
                // Check version
                Chunk_Base version = getChunksByID(Chunk_MVER.Magic).FirstOrDefault();

                if (version == null || !(version is Chunk_MVER))
                    throw new WMOException("File is not a valid WMO file (missing version header).");

                // WMOv4 required.
                if (((Chunk_MVER)version).fileVersion != 17)
                    throw new WMOException("Unsupported WMO version!");

                // Root file needs to be a root file.
                if (!chunks.Any(c => c.ChunkID == Chunk_MOHD.Magic))
                    throw new WMOException("File is not a valid WMO file (missing root header).");

                // Parse all group files and import their chunks.
                foreach (WMOFile groupFile in groupFiles)
                {
                    groupFile.parse();
                    chunks.AddRange(groupFile.getChunks().Where(c => c.ChunkID != Chunk_MVER.Magic));
                    groupFile.flush();
                }

                Log.Write("WMO: Root file loaded with {0} group file children.", groupFiles.Count);
            }
        }

        public void flush()
        {
            chunks.Clear();

            if (groupFiles != null)
                groupFiles.Clear();
        }

        public IEnumerable<Chunk_Base> getChunksByID(UInt32 id)
        {
            return chunks.Where(c => c.ChunkID == id);
        }

        public List<Chunk_Base> getChunks()
        {
            return chunks;
        }
    }
}
