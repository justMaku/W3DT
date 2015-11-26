using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT.Runners
{
    class RunnerInitializeCASC : RunnerBase
    {
        public override void Work()
        {
            CASCConfig.Load();
            CDNHandler.Initialize();

            CASCFolder root = new CASCFolder(CDNHandler.Hasher.ComputeHash("root"));
            Program.CASCEngine = new CASCEngine(root);
        }
    }
}
