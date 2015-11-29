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
            CASCEngine engine;

            if (Program.Settings.UseRemote)
                engine = CASCEngine.OpenOnlineStorage();
            else
                engine = CASCEngine.OpenLocalStorage();
            //CASCConfig.Load();
            //CDNHandler.Initialize();

            //CASCFolder root = new CASCFolder(CASCEngine.Hasher.ComputeHash("root"));
            //Program.CASCEngine = new CASCEngine(root);
        }
    }
}
