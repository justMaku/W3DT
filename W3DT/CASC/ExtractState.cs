using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.CASC
{
    public class ExtractState
    {
        public CASCFile File { get; private set; }
        public bool State { get; set; }
        public int TrackerID { get; set; }

        public ExtractState(CASCFile file)
        {
            File = file;
            State = false;
        }
    }
}
