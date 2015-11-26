using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT
{
    static class Constants
    {
        // Updating //
        public static readonly string WEB_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        public static readonly string UPDATE_REPO_URL = @"https://api.github.com/repos/Kruithne/W3DT/releases/latest";
        public static readonly string UPDATE_PACKAGE_FILE = "update.zip";

        // CDN // 
        public static readonly string CDN_VERSION_URL = @"http://us.patch.battle.net/wow_beta/versions";

        // Base Stuff //
        public static readonly string LOAD_FLAVOR_FILE = "load_flavor";
        public static readonly string LOADING_DEFAULT = "Loading...";

        public static readonly string SETTINGS_FILE = "settings.json";
        public static readonly string LOG_FILE = "session.log";

        public static readonly string DIRECTORY_PLACEHOLDER = "<Select World of Warcraft installation directory>";
        public static readonly string WOW_BUILD_FILE = ".build.info";

        // Settings labels //
        public static readonly string CURRENT_VERSION_STRING = "Current Version: {0}";
        public static readonly string CDN_HOST_STRING = "Remote CDN Host: {0}";
        public static readonly string WOW_DIRECTORY_STRING = "Local Directory: {0}";
    }
}
