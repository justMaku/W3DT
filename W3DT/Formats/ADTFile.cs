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
        public ADTFile(string file) : base(file)
        {
            // ToDo: Construct chunk holder.
        }

        public override void parse()
        {
            // ToDo: Actually parse the file.
        }
    }
}
