using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.Formats
{
    public abstract class WriterBase : IWriter
    {
        protected void nl(StreamWriter writer)
        {
            writer.WriteLine(string.Empty);
        }

        public abstract void Close();
        public abstract void Write();
    }
}
