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

    public class WMOFile : ChunkedFormatBase, IChunkProvider
    {
        //public string baseName { get; private set; }
        private Chunk_MOGP groupRoot;
        private List<WMOFile> groupFiles;
        private bool isRoot;

        public WMOFile(string path, bool isRoot = true) : base(path)
        {
            this.isRoot = isRoot;
            //Chunks = new List<Chunk_Base>();

            if (isRoot)
                groupFiles = new List<WMOFile>();

            //baseName = Path.GetFileNameWithoutExtension(path);
        }

        public override Chunk_Base lookupChunk(UInt32 magic)
        {
            switch (magic)
            {
                case Chunk_MVER.Magic: return new Chunk_MVER(this);
                case Chunk_MOHD.Magic: return new Chunk_MOHD(this);
                case Chunk_MCVP.Magic: return new Chunk_MCVP(this);
                case Chunk_MFOG.Magic: return new Chunk_MFOG(this);
                case Chunk_MODD.Magic: return new Chunk_MODD(this);
                case Chunk_MODN.Magic: return new Chunk_MODN(this);
                case Chunk_MODS.Magic: return new Chunk_MODS(this);
                case Chunk_MOGI.Magic: return new Chunk_MOGI(this);
                case Chunk_MOGN.Magic: return new Chunk_MOGN(this);
                case Chunk_MOLT.Magic: return new Chunk_MOLT(this);
                case Chunk_MOMT.Magic: return new Chunk_MOMT(this);
                case Chunk_MONR.Magic: return new Chunk_MONR(this);
                case Chunk_MOPY.Magic: return new Chunk_MOPY(this);
                case Chunk_MOTV.Magic: return new Chunk_MOTV(this);
                case Chunk_MOTX.Magic: return new Chunk_MOTX(this);
                case Chunk_MOVI.Magic: return new Chunk_MOVI(this);
                case Chunk_MOVT.Magic: return new Chunk_MOVT(this);

                case Chunk_MOGP.Magic: return new Chunk_MOGP(this);
                default: return new Chunk_Base(this);
            }
        }

        public override void storeChunk(Chunk_Base chunk)
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
                Chunks.Add(chunk);

                if (chunk is Chunk_MOGP)
                    groupRoot = (Chunk_MOGP)chunk;
            }
        }

        public override string getFormatName()
        {
            return "WMO";
        }

        public void addGroupFile(WMOFile file)
        {
            // Group files can only be inside root files.
            if (isRoot)
                groupFiles.Add(file);
        }

        public new void parse()
        {
            base.parse();

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
                if (!Chunks.Any(c => c.ChunkID == Chunk_MOHD.Magic))
                    throw new WMOException("File is not a valid WMO file (missing root header).");

                // Parse all group files and import their chunks.
                foreach (WMOFile groupFile in groupFiles)
                {
                    groupFile.parse();
                    Chunks.AddRange(groupFile.getChunks().Where(c => c.ChunkID != Chunk_MVER.Magic));
                    groupFile.flush();
                }

                Log.Write("WMO: Root file loaded with {0} group file children.", groupFiles.Count);
            }
        }

        public new void flush()
        {
            base.flush();

            if (groupFiles != null)
                groupFiles.Clear();
        }

        public Chunk_Base getChunk(UInt32 chunkID, bool error = true)
        {
            Chunk_Base chunk = getChunksByID(chunkID).FirstOrDefault();
            if (chunk == null && error)
                throw new WMOException(string.Format("File does not contain chunk 0x{0}.", chunkID.ToString("X")));

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
