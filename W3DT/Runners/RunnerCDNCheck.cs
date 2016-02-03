using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using W3DT.Events;

namespace W3DT.Runners
{
    class RunnerCDNCheck : RunnerBase
    {
        public override void Work()
        {
            Log.Write("CDN scan has started!");

            string rawData;

            using (WebClient client = new WebClient())
            {
                try
                {
                    rawData = client.DownloadString(string.Format(Constants.CDN_CONFIG_URL, Constants.CDN_REGIONS[0]));
                }
                catch
                {
                    rawData = client.DownloadString(string.Format(Constants.CDN_CONFIG_URL, Constants.CDN_REGIONS[1]));
                }
            }

            if (rawData == null || rawData == string.Empty)
                throw new Exception("Malformed CDN data from US server.");

            Dictionary<string, string> knownHosts = new Dictionary<string, string>();
            string[] dataLines = rawData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < dataLines.Length; i++)
            {
                // Skip first line (header info) and empty lines.
                if (i == 0 || dataLines[i] == string.Empty)
                    continue;

                string[] parts = dataLines[i].Split('|');
                string[] hosts = parts[2].Split(null);

                foreach (string host in hosts)
                    if (!knownHosts.ContainsKey(host))
                        knownHosts.Add(host, parts[1]);
            }

            string bestHost = null;
            string hostPath = null;
            double bestPing = -1D;

            Log.Write("CDN ping routine time...");
            foreach (KeyValuePair<string, string> node in knownHosts)
            {
                double ping = HostPingAverage(node.Key);
                if (ping >= 0 && (bestHost == null || ping < bestPing))
                {
                    bestHost = node.Key;
                    hostPath = node.Value;
                    bestPing = ping;
                }
            }

            if (bestHost != null)
                Log.Write("{0} was found to be the fastest host with a delay of {1}ms.", bestHost, bestPing);
            else
                Log.Write("Failed to find a CDN server. Looks like the Legion will prevail after all.");

            EventManager.Trigger_CDNScanDone(new CDNScanDoneArgs(bestHost, hostPath));
            Log.Write("CDN scan has finished without error!");
        }

        private double HostPingAverage(string host)
        {
            Log.Write("Attempting to ping " + host + "...");

            try
            {
                bool success = false;
                long totalTime = 0;
                Ping pingSender = new Ping();

                for (int i = 0; i < 4; i++)
                {
                    PingReply reply = pingSender.Send(host, 120);
                    if (reply.Status == IPStatus.Success)
                    {
                        Log.Write("Ping response from {0} ({1}) of {2}ms", host, reply.Address.ToString(), reply.RoundtripTime);
                        success = true;
                        totalTime += reply.RoundtripTime;
                    }
                    else
                    {
                        Log.Write("No response from {0} ({1})", host, i);
                    }
                }

                if (success)
                {
                    double totalTimeRounded = totalTime / 4;
                    Log.Write("Average ping delay for {0}: {1}", host, totalTimeRounded);

                    return totalTimeRounded;
                }

                Log.Write("All attempts to ping {0} failed, ignoring host.", host);
                return -1D;
            }
            catch (PingException ex)
            {
                Log.Write("Attempted ping to {0} FAILED; {1}", host, ex.Message);
                return -1D;
            }
        }
    }
}
