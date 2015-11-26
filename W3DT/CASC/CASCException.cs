using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public class CASCException : Exception
    {
        public CASCException(string message) : base(message) {}
    }
}
