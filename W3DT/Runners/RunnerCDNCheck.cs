using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace W3DT.Runners
{
    class RunnerCDNCheck : RunnerBase
    {
        private SplashScreen screen;

        public RunnerCDNCheck(SplashScreen screen)
        {
            this.screen = screen;
        }

        public override void Work()
        {
            // new string[] { "\r\n", "\n" }
            using (WebClient client = new WebClient())
            {
                string rawData = client.DownloadString("http://us.patch.battle.net/wow_beta/cdns");
            }
            // http://us.patch.battle.net/wow_beta/cdns

            // Step 1: Download CDNS data.
            // Step 2: Load all CDNs into memory.
            // Step 3: Check if we have a local one already
            // Step 3: If we do, check it's valid.
            // Step 4: If none exist, or is invalid, continue.
            // Step 5: Locate fastest CDN, mark as current.
            // Step 6: Throw event to tell Splash screen we're done.
        }
    }
}
