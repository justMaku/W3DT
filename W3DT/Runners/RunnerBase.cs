using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace W3DT.Runners
{
    public abstract class RunnerBase
    {
        public void Begin()
        {
            Thread thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }

        public abstract void Work();
    }
}
