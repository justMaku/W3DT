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

        // Settings //
        public static readonly string SETTINGS_FILE = "settings.json";

        public static readonly string DIRECTORY_PLACEHOLDER = "<Select World of Warcraft installation directory>";
    }
}
