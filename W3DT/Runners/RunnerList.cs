using System.Threading;

namespace W3DT.Runners
{
    class RunnerList : RunnerBase
    {
        private RunnerBase[] runners;
        private int currentRunner = 0;

        public RunnerList(RunnerBase[] runners)
        {
            this.runners = runners;
        }

        public override void Work()
        {
            while (runners.Length > currentRunner)
            {
                RunnerBase runner = runners[currentRunner];

                if (currentRunner > 0)
                    runner.previousRunner = runners[currentRunner - 1];

                runner.Begin();

                while (runner.thread.IsAlive)
                    Thread.Sleep(100);

                currentRunner++;
            }
        }
    }
}
