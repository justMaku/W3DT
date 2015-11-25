using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace W3DT.Runners
{
    public abstract class RunnerBase
    {
        public Thread thread { get; private set; }
        public int state { get; protected set; }
        public RunnerBase previousRunner { get; set; }

        public void Begin()
        {
            thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }

        public abstract void Work();
    }
}
