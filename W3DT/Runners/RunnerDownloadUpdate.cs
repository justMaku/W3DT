using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using W3DT.Events;

namespace W3DT.Runners
{
    public class RunnerDownloadUpdate : RunnerBase
    {
        private string packageLocation;

        public RunnerDownloadUpdate(string packageLocation)
        {
            this.packageLocation = packageLocation;
        }

        public override void Work()
        {
            using (var client = new WebClient())
            {
                bool success = false;
                client.Headers.Add("user-agent", Constants.WEB_USER_AGENT);

                try
                {
                    client.DownloadFile(packageLocation, Constants.UPDATE_PACKAGE_FILE);
                    success = true;
                }
                catch (WebException ex)
                {
                    Debug.WriteLine("Encountered exception downloading remote package: " + ex.Message);
                    Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                }

                EventManager.Trigger_UpdateDownloadDone(new UpdateDownloadDoneArgs(success));
            }
        }
    }
}
