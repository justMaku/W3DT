using System;
using System.Collections.Generic;
using System.IO;
using W3DT.Hashing.MD5;

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

        public static bool ValidateData { get; set; }
        public static bool ThrowOnFileNotFound { get; set; }
        public static LoadFlags LoadFlags { get; set; }

        private int versionIndex;

        public CASCConfig()
        {
            ValidateData = true;
            ThrowOnFileNotFound = true;
            LoadFlags = LoadFlags.All;
        }

        public static CASCConfig LoadOnlineStorageConfig()
        {
            var config = new CASCConfig();
            string usingRegion = null;

            try
            {
                usingRegion = Constants.CDN_REGIONS[0];

                using (var stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_CONFIG_URL, usingRegion)))
                    config.CDNData = VerBarConfig.ReadVerBarConfig(stream);

                using (Stream stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_VERSION_URL, usingRegion)))
                    config.VersionsData = VerBarConfig.ReadVerBarConfig(stream);
            }
            catch
            {
                usingRegion = Constants.CDN_REGIONS[1];

                using (var stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_CONFIG_URL, usingRegion)))
                    config.CDNData = VerBarConfig.ReadVerBarConfig(stream);

                using (Stream stream = CDNIndexHandler.OpenFileDirect(string.Format(Constants.CDN_VERSION_URL, usingRegion)))
                    config.VersionsData = VerBarConfig.ReadVerBarConfig(stream);
            }

            if (usingRegion == null)
                throw new Exception("Unable to access default CDN servers for config/versioning.");

            for (int i = 0; i < config.VersionsData.Count; i++)
            {
                if (config.VersionsData[i]["Region"] == usingRegion)
                {
                    config.versionIndex = i;
                    break;
                }
            }

            string cdnKey = config.VersionsData[config.versionIndex]["CDNConfig"];
            using (Stream stream = CDNIndexHandler.OpenConfigFileDirect(config, cdnKey))
                config.CDNConfig = KeyValueConfig.ReadKeyValueConfig(stream);

            config.ActiveBuild = 0;
            config.Builds = new List<KeyValueConfig>();

            using (Stream stream = CDNIndexHandler.OpenConfigFileDirect(config, config.VersionsData[config.versionIndex]["BuildConfig"]))
            {
                var buildConfig = KeyValueConfig.ReadKeyValueConfig(stream);
                config.Builds.Add(buildConfig);
            }

            return config;
        }

        public static CASCConfig LoadLocalStorageConfig()
        {
            var config = new CASCConfig();
            string buildInfoPath = Path.Combine(Program.Settings.WoWDirectory, Constants.WOW_BUILD_FILE);

            using (Stream buildInfoStream = new FileStream(buildInfoPath, FileMode.Open))
                config.BuildInfo = VerBarConfig.ReadVerBarConfig(buildInfoStream);

            Dictionary<string, string> buildInfo = config.GetActiveBuild();
            if (buildInfo == null)
                throw new Exception("BuildInfo missing!");

            string configFolder = Path.Combine(Program.Settings.WoWDirectory, @"Data\config");
            config.ActiveBuild = 0;
            config.Builds = new List<KeyValueConfig>();

            string buildKey = buildInfo["BuildKey"];
            string buildCfgPath = Path.Combine(configFolder, buildKey.Substring(0, 2), buildKey.Substring(2, 2), buildKey);
            using (Stream stream = new FileStream(buildCfgPath, FileMode.Open))
                config.Builds.Add(KeyValueConfig.ReadKeyValueConfig(stream));

            string cdnKey = buildInfo["CDNKey"];
            string cdnCfgPath = Path.Combine(configFolder, cdnKey.Substring(0, 2), cdnKey.Substring(2, 2), cdnKey);
            using (Stream stream = new FileStream(cdnCfgPath, FileMode.Open))
                config.CDNConfig = KeyValueConfig.ReadKeyValueConfig(stream);

            return config;
        }

        private Dictionary<string, string> GetActiveBuild()
        {
            if (BuildInfo == null)
                return null;

            for (int i = 0; i < BuildInfo.Count; i++)
            {
                if (BuildInfo[i]["Active"] == "1")
                    return BuildInfo[i];
            }

            return null;
        }

        public int ActiveBuild { get; set; }
        public string Product { get; private set; }
        public string BuildName => Builds[ActiveBuild]["build-name"][0];

        public MD5Hash RootMD5 => Builds[ActiveBuild]["root"][0].ToByteArray().ToMD5();
        public MD5Hash DownloadMD5 => Builds[ActiveBuild]["download"][0].ToByteArray().ToMD5();
        public MD5Hash InstallMD5 => Builds[ActiveBuild]["install"][0].ToByteArray().ToMD5();
        public MD5Hash EncodingMD5 => Builds[ActiveBuild]["encoding"][0].ToByteArray().ToMD5();
        public MD5Hash EncodingKey => Builds[ActiveBuild]["encoding"][0].ToByteArray().ToMD5();
        public MD5Hash PartialPriorityMD5 => Builds[ActiveBuild]["partial-priority"][0].ToByteArray().ToMD5();
        public MD5Hash PatchKey => Builds[ActiveBuild]["patch"][0].ToByteArray().ToMD5();

        public string BuildUID => Builds[ActiveBuild]["build-uid"][0];
        public string InstallSize => Builds[ActiveBuild]["install-size"][0];
        public string DownloadSize => Builds[ActiveBuild]["download-size"][0];
        public string PartialPrioritySize => Builds[ActiveBuild]["partial-priority-size"][0];
        public string EncodingSize => Builds[ActiveBuild]["encoding-size"][0];
        public string PatchSize => Builds[ActiveBuild]["patch-size"][0];

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

        public List<string> Archives => CDNConfig["archives"];
        public string ArchiveGroup => CDNConfig["archive-group"][0];
        public List<string> PatchArchives => CDNConfig["patch-archives"];
        public string PatchArchiveGroup => CDNConfig["patch-archive-group"][0];
    }
}
