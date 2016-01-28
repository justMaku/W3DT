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
            while (!isEndOfStream())
            {
                UInt32 chunkID = readUInt32();
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
                    case Chunk_MOGP.Magic: chunk = new Chunk_MOGP(this); break;
                    default: chunk = new Chunk_Base(this); break;
                }

                if (chunk.ChunkID == 0x0)
                    Log.Write("WMO: Unknown chunk encountered = {0}", chunkID);
                else
                    chunks.Add(chunk);

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
