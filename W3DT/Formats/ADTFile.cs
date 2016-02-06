using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.Formats
{
    public class ADTException : Exception
    {
        public ADTException(string message) : base(message) { }
    }

    public class ADTFile : FormatBase
    {
        public string BaseName { get; private set; }

        public ADTFile(string file) : base(file)
        {
            BaseName = Path.GetFileName(file);
        }

        public void parse()
        {
            // ToDo: Actually parse the file.
        }
    }
}
