using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    public class Chunk_Base
    {
        protected ChunkedFormatBase File;
        protected string ChunkName;
        private string LogPrefix;

        public UInt32 ChunkSize { get; private set; }
        public UInt32 ChunkID { get; protected set; }

        public Chunk_Base(ChunkedFormatBase file, string chunkName = "????")
        {
            File = file;
            ChunkName = chunkName;
            LogPrefix = file.getFormatName();

            ChunkID = 0x0;
            ChunkSize = file.readUInt32();
        }

        protected string GetLogPrefix()
        {
            return string.Format("{0} {1} [{2}] ", LogPrefix, File.BaseName, ChunkName);
        }

        protected void LogWrite(string message)
        {
            Log.Write(GetLogPrefix() + message);
        }

        protected void LogValue(string key, object value)
        {
            LogWrite(string.Format("{0} -> {1}", key, value.ToString()));
        }
    }
}
