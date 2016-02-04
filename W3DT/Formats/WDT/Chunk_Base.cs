using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WDT
{
    public class Chunk_Base
    {
        protected WDTFile file;
        protected string chunkName;
        private static string LOG_PREFIX = "WDT {0} [{1}] ";

        public UInt32 ChunkID { get; protected set; }
        public UInt32 ChunkSize { get; private set; }

        public Chunk_Base(WDTFile file, string prefix = "????")
        {
            this.file = file;
            this.chunkName = prefix;

            ChunkID = 0x0;
            ChunkSize = file.readUInt32();
        }

        protected string GetLogPrefix()
        {
            return string.Format(LOG_PREFIX, file.BaseName, chunkName);
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
