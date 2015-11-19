using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using W3DT.Events;
using W3DT.JSONContainers;

namespace W3DT.Runners
{
    class RunnerUpdateCheck : RunnerBase
    {
        private static string UPDATE_URL = @"https://api.github.com/repos/Kruithne/W3DT/releases/latest";

        public override void Work()
        {
            string raw;
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                try
                {
                    raw = client.DownloadString(UPDATE_URL);
                }
                catch (WebException ex)
                {
                    raw = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                }
            }

            LatestReleaseData data = JsonConvert.DeserializeObject<LatestReleaseData>(raw);
            EventManager.T_UpdateCheckComplete(data);
        }
    }
}
