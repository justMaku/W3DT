using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.CASC;

namespace W3DT
{
    static class Constants
    {
        // Updating //
        public static readonly string WEB_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        public static readonly string UPDATE_REPO_URL = @"https://api.github.com/repos/Kruithne/W3DT/releases/latest";
        public static readonly string UPDATE_PACKAGE_FILE = "update.zip";

        // CDN // 
        public static string CDN_VERSION_URL { get { return string.Format(@"http://us.patch.battle.net/{0}/versions", Program.Settings.RemoteClientVersion.UrlTag); } }
        public static string CDN_CONFIG_URL { get { return string.Format(@"http://us.patch.battle.net/{0}/cdns", Program.Settings.RemoteClientVersion.UrlTag); } }

        // CDN Versions //
        public static readonly WoWVersion[] WOW_VERSIONS = new WoWVersion[] {
            new WoWVersion("wow", "Live (Retail)"),
            new WoWVersion("wow_beta", "Beta (In Development)"),
            new WoWVersion("wowt", "PTR (In Development)")
        };

        //public static readonly WoWVersion DEFAULT_VERSION = WOW_VERSIONS[0];

        // Base Stuff //
        public static readonly string LOAD_FLAVOR_FILE = "load_flavor";
        public static readonly string LOADING_DEFAULT = "Loading...";

        public static readonly string SETTINGS_FILE = "settings.json";
        public static readonly string LOG_FILE = "session.log";
        public static readonly string LIST_FILE = "listfile";

        public static readonly string TEMP_DIRECTORY = "temp";

        public static readonly string DIRECTORY_PLACEHOLDER = "<Select World of Warcraft installation directory>";
        public static readonly string WOW_BUILD_FILE = ".build.info";

        // Settings labels //
        public static readonly string CURRENT_VERSION_STRING = "Current Version: {0}";
        public static readonly string CDN_HOST_STRING = "Remote CDN Host: {0}";
        public static readonly string WOW_DIRECTORY_STRING = "Local Directory: {0}";

        // Explorer Status //
        public static readonly string GENERIC_WINDOW_SEARCH_STATE = "{0} Files Found ({1})";
        public static readonly string MAP_SEARCH_STATE = "{0} maps found ({1})";
        public static readonly string SEARCH_STATE_SEARCHING = "Searching...";
        public static readonly string SEARCH_STATE_DONE = "Done";
        
        // External //
        public static readonly string FINAL_BOSS_URL = "http://finalboss.tv/";
        public static readonly string KRUITHNE_URL = "http://www.kruithne.net/";
    }
}
