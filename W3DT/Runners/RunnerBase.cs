using System.Threading;

namespace W3DT.Runners
{
    public abstract class RunnerBase
    {
        public Thread thread { get; private set; }
        public int state { get; protected set; }
        public RunnerBase previousRunner { get; set; }
        public string ThreadName { get; protected set; }

        public RunnerBase()
        {
            ThreadName = GetType().Name;
        }

        public void Begin()
        {
            thread = new Thread(new ThreadStart(Work));
            thread.Name = ThreadName;
            thread.Start();
        }

        public abstract void Work();

        public void Kill()
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
        }
    }
}
