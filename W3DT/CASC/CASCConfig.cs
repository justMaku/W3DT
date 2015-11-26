using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.CASC
{
    class CASCConfig
    {
        public static KeyValueConfig CDNConfig { get; private set; }
        public static KeyValueConfig BuildConfig { get; private set; }

        private static VersionInfo BuildInfo;
        private static VersionInfo VersionInfo;

        public static void Load()
        {
            if (Program.Settings.UseRemote)
            {
                // Load version info from default CDN.
                using (Stream stream = CDNHandler.OpenFileDirect(Constants.CDN_VERSION_URL))
                    VersionInfo = new VersionInfo(stream);

                // Load build config from selected CDN.
                using (Stream stream = CDNHandler.OpenConfigFileDirect(VersionInfo["BuildConfig"]))
                    BuildConfig = new KeyValueConfig(stream);

                // Load CDN config from selected CDN.
                using (Stream stream = CDNHandler.OpenConfigFileDirect(VersionInfo["CDNConfig"]))
                    CDNConfig = new KeyValueConfig(stream);
            }
            else
            {
                string buildFile = Path.Combine(Program.Settings.WoWDirectory, Constants.WOW_BUILD_FILE);

                // Load build info file.
                using (Stream stream = new FileStream(buildFile, FileMode.Open))
                    BuildInfo = new VersionInfo(stream);

                // Load build config file.
                string buildKey = BuildInfo["Build Key"];
                string buildConfigFile = Path.Combine(Program.Settings.WoWDirectory, @"Data\config", buildKey.Substring(0, 2), buildKey.Substring(2, 2), buildKey);
                using (Stream stream = new FileStream(buildConfigFile, FileMode.Open))
                    BuildConfig = new KeyValueConfig(stream);

                // Load CDN config file.
                string cdnKey = BuildInfo["CDN Key"];
                string cdnConfigFile = Path.Combine(Program.Settings.WoWDirectory, @"Data\config", cdnKey.Substring(0, 2), buildKey.Substring(2, 2), cdnKey);
                using (Stream stream = new FileStream(cdnConfigFile, FileMode.Open))
                    CDNConfig = new KeyValueConfig(stream);
            }
        }

        public static byte[] EncodingKey
        {
            get { return BuildConfig["encoding"][1].ToByteArray(); }
        }

        public static byte[] RootMD5
        {
            get { return BuildConfig["root"][0].ToByteArray(); }
        }

        public static string CDNUrl
        {
            get
            {
                if (Program.Settings.UseRemote)
                {
                    return String.Format("http://{0}/{1}", Program.Settings.RemoteHost, Program.Settings.RemoteHostPath);
                }
                else
                {
                    string[] hosts = BuildInfo["CDN Hosts"].Split(null);
                    return String.Format("http://{0}{1}", hosts[0], BuildInfo["CDN Path"]);
                }
            }
        }
    }
}
