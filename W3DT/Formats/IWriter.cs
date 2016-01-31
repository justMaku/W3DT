using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    public interface IWriter
    {
        void Close();
        void Write();
    }
}
