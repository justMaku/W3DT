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
        public override void Work()
        {
            string raw;
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", Constants.WEB_USER_AGENT);

                try
                {
                    raw = client.DownloadString(Constants.UPDATE_REPO_URL);
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
