using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;
using W3DT.Events;

namespace W3DT.Runners
{
    class RunnerInitializeCASC : RunnerBase
    {
        public override void Work()
        {
            CASCEngine engine;

            try
            {
                if (Program.Settings.UseRemote)
                    engine = CASCEngine.OpenOnlineStorage();
                else
                    engine = CASCEngine.OpenLocalStorage();

                engine.RootHandler.LoadListFile(Constants.LIST_FILE);
                EventManager.Trigger_LoadStepDone();
                Done(true);
            }
            catch (Exception ex)
            {
                Log.Write("Error prevented CASC engine loading: " + ex.Message);
                Log.Write(ex.StackTrace);

                Done(false);
            }
        }

        private void Done(bool success)
        {
            EventManager.Trigger_CASCLoadDone(new CASCLoadDoneArgs(success));
        }
    }
}
