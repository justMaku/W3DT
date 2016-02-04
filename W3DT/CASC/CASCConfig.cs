using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace W3DT.CASC
{
    [Flags]
    public enum LoadFlags
    {
        All = -1,
        Download = 1,
        Install = 2,
    }

    public class CASCConfig
    {
        public KeyValueConfig CDNConfig { get; private set; }
        public List<KeyValueConfig> Builds { get; private set; }

        public VerBarConfig BuildInfo { get; private set; }
        public VerBarConfig CDNData { get; private set; }
        public VerBarConfig VersionsData { get; private set; }

        public string Region { get; private set; }
        public static bool ValidateData { get; set; }
        public static bool ThrowOnFileNotFound { get; set; }
        public static LoadFlags LoadFlags { get; set; }

        public CASCConfig()
        {
            ValidateData = true;
            ThrowOnFileNotFound = true;
            LoadFlags = LoadFlags.All;
        }

        public static CASCConfig LoadOnlineStorageConfig()
        {
            var config = new CASCConfig();

            try
            {
                using (var stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_CONFIG_URL, Constants.CDN_REGIONS[0])))
                    config.CDNData = VerBarConfig.ReadVerBarConfig(stream);

                using (Stream stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_VERSION_URL, Constants.CDN_REGIONS[0])))
                    config.VersionsData = VerBarConfig.ReadVerBarConfig(stream);
            }
            catch
            {
                using (var stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_CONFIG_URL, Constants.CDN_REGIONS[1])))
                    config.CDNData = VerBarConfig.ReadVerBarConfig(stream);

                using (Stream stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_VERSION_URL, Constants.CDN_REGIONS[1])))
                    config.VersionsData = VerBarConfig.ReadVerBarConfig(stream);
            }

            string cdnKey = config.VersionsData[0]["CDNConfig"];
            using (Stream stream = CDNIndexHandler.OpenConfigFileDirect(config, cdnKey))
                config.CDNConfig = new KeyValueConfig(stream);

            config.Builds = new List<KeyValueConfig>();

            int selectedBuild = -1;
            for (int i = 0; i < config.CDNConfig["builds"].Count; i++)
            {
                try
                {
                    using (Stream stream = CDNIndexHandler.OpenConfigFileDirect(config, config.CDNConfig["builds"][i]))
                    {
                        var cfg = new KeyValueConfig(stream);
                        config.Builds.Add(cfg);

                        string buildUID = cfg["build-uid"][0];
                        if (selectedBuild == -1 && buildUID == Program.Settings.RemoteClientVersion.UrlTag)
                        {
                            selectedBuild = i;

                            Log.Write("Using build [{0} - {1}] {2}", i, buildUID, cfg["build-name"][0]);
                        }
                    }
                }
                catch
                {

                }
            }

            config.ActiveBuild = selectedBuild > -1 ? selectedBuild : 0;

            return config;
        }

        public static CASCConfig LoadLocalStorageConfig()
        {
            var config = new CASCConfig();
            string buildInfoPath = Path.Combine(Program.Settings.WoWDirectory, Constants.WOW_BUILD_FILE);

            using (Stream buildInfoStream = new FileStream(buildInfoPath, FileMode.Open))
                config.BuildInfo = VerBarConfig.ReadVerBarConfig(buildInfoStream);

            Dictionary<string, string> buildInfo = null;

            for (int i = 0; i < config.BuildInfo.Count; ++i)
            {
                if (config.BuildInfo[i]["Active"] == "1")
                {
                    buildInfo = config.BuildInfo[i];
                    break;
                }
            }

            if (buildInfo == null)
                throw new Exception("Can't find active BuildInfoEntry");

            //string dataFolder = CASCGame.GetDataFolder(config.GameType);
            string configFolder = Path.Combine(Program.Settings.WoWDirectory, @"Data\config");

            config.ActiveBuild = 0;

            config.Builds = new List<KeyValueConfig>();

            string buildKey = buildInfo["BuildKey"];
            string buildCfgPath = Path.Combine(configFolder, buildKey.Substring(0, 2), buildKey.Substring(2, 2), buildKey);
            using (Stream stream = new FileStream(buildCfgPath, FileMode.Open))
                config.Builds.Add(new KeyValueConfig(stream));

            string cdnKey = buildInfo["CDNKey"];
            string cdnCfgPath = Path.Combine(configFolder, cdnKey.Substring(0, 2), cdnKey.Substring(2, 2), cdnKey);
            using (Stream stream = new FileStream(cdnCfgPath, FileMode.Open))
                config.CDNConfig = new KeyValueConfig(stream);

            return config;
        }

        public int ActiveBuild { get; set; }

        public string BuildName { get { return Builds[ActiveBuild]["build-name"][0]; } }

        public string Product { get; private set; }

        public byte[] RootMD5
        {
            get { return Builds[ActiveBuild]["root"][0].ToByteArray(); }
        }

        public byte[] DownloadMD5
        {
            get { return Builds[ActiveBuild]["download"][0].ToByteArray(); }
        }

        public byte[] InstallMD5
        {
            get { return Builds[ActiveBuild]["install"][0].ToByteArray(); }
        }

        public byte[] EncodingMD5
        {
            get { return Builds[ActiveBuild]["encoding"][0].ToByteArray(); }
        }

        public byte[] EncodingKey
        {
            get { return Builds[ActiveBuild]["encoding"][1].ToByteArray(); }
        }

        public string BuildUID
        {
            get { return Builds[ActiveBuild]["build-uid"][0]; }
        }

        public string CDNHost
        {
            get
            {
                if (Program.Settings.UseRemote)
                    return CDNData[0]["Hosts"].Split(null)[0];
                else
                    return BuildInfo[0]["CDNHosts"].Split(null)[0];
            }
        }

        public string CDNPath
        {
            get
            {
                if (Program.Settings.UseRemote)
                    return CDNData[0]["Path"]; // use first
                else
                    return BuildInfo[0]["CDNPath"];
            }
        }

        public string CDNUrl
        {
            get
            {
                if (Program.Settings.UseRemote)
                    return string.Format("http://{0}/{1}", Program.Settings.RemoteHost, Program.Settings.RemoteHostPath);
                else
                    return string.Format("http://{0}{1}", BuildInfo[0]["CDNHosts"].Split(' ')[0], BuildInfo[0]["CDNPath"]);
            }
        }

        public List<string> Archives
        {
            get { return CDNConfig["archives"]; }
        }

        public string ArchiveGroup
        {
            get { return CDNConfig["archive-group"][0]; }
        }

        public List<string> PatchArchives
        {
            get { return CDNConfig["patch-archives"]; }
        }

        public string PatchArchiveGroup
        {
            get { return CDNConfig["patch-archive-group"][0]; }
        }
    }
}
