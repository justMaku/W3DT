using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class Chunk_Base
    {
        protected WMOFile file;
        protected string chunkName;
        private static string LOG_PREFIX = "WMO: [{0}] ";

        public UInt32 ChunkSize { get; private set; }
        public UInt32 ChunkID { get; protected set; }

        public Chunk_Base(WMOFile file, string chunkName = "????")
        {
            this.file = file;
            this.chunkName = chunkName;

            ChunkID = 0x0;
            ChunkSize = file.readUInt32();
        }

        protected string GetLogPrefix()
        {
            return string.Format(LOG_PREFIX, chunkName);
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
