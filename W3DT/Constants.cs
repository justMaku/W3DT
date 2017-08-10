using System;
using W3DT.CASC;

namespace W3DT
{
    static class Constants
    {
        public static readonly string DOUBLE_NEWLINE = Environment.NewLine + Environment.NewLine;
        // Updating //
        public static readonly string WEB_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        public static readonly string UPDATE_REPO_URL = @"https://api.github.com/repos/Kruithne/W3DT/releases/latest";
        public static readonly string UPDATE_PACKAGE_FILE = "update.zip";

        // CDN // 
        public static string[] CDN_REGIONS = new string[] { "us", "eu" };
        public static string CDN_VERSION_URL { get { return @"http://{0}.patch.battle.net/" + Program.Settings.RemoteClientVersion.UrlTag + "/versions"; } }
        public static string CDN_CONFIG_URL { get { return @"http://{0}.patch.battle.net/" + Program.Settings.RemoteClientVersion.UrlTag + "/cdns"; } }

        // CDN Versions //
        public static readonly WoWVersion[] WOW_VERSIONS = new WoWVersion[] {
            new WoWVersion("wow", "Live (Retail)"),
            new WoWVersion("wow_beta", "Beta (In Development)"),
            new WoWVersion("wowt", "PTR (In Development)")
        };

        //public static readonly WoWVersion DEFAULT_VERSION = WOW_VERSIONS[0];

        public static readonly float WOW_ADT_TILE_SIZE = 533.333333333f;
        public static readonly float WOW_ADT_MAP_MAX = WOW_ADT_TILE_SIZE * 32;

        // Base Stuff //
        public static readonly string LOAD_FLAVOR_FILE = "load_flavor";
        public static readonly string LOADING_DEFAULT = "Loading...";

        public static readonly string SETTINGS_FILE = "settings.json";
        public static readonly string LOG_FILE = "session.log";
        public static readonly string LIST_FILE = "listfile";
        public static readonly string LIST_FILE_BIN = "listfile.bin";
        public static readonly string DEV_ASSET_DIR = "../../../assets/";

        public static readonly string TEMP_DIRECTORY = "temp";

        public static readonly string DIRECTORY_PLACEHOLDER = "<Select World of Warcraft installation directory>";
        public static readonly string WOW_BUILD_FILE = ".build.info";

        // Main Form Buttons //
        public static readonly MainButton[] MAIN_FORM_BUTTONS = new MainButton[] {
            new MainButton(W3DT.Properties.Resources.achievement_character_human_female, typeof(ModelViewer), "Model Viewer", "View/export characters and creatures from the game. Characters can be customized, equipped with items and even imported from the armory."),
            new MainButton(W3DT.Properties.Resources.inv_hammer_20__1_, typeof(WMOViewer), "WMO Viewer", "WMO objects are large 3D objects that often contain numerous other smaller objects (doodads). Buildings and caves are two common examples of WMO objects."),
            new MainButton(W3DT.Properties.Resources.inv_drink_03, null, "Object (Doodad) Viewer", "Doodads are small objects generally used for clutter and furnishing both interior and exterior envionrments."),
            new MainButton(W3DT.Properties.Resources.inv_misc_map03, typeof(MapViewerWindow), "Map Viewer", "Browse all maps available in the game using a minimap-based tool and export them in full 3D!"),
            new MainButton(W3DT.Properties.Resources.trade_archaeology_delicatemusicbox, typeof(MusicExplorerWindow), "Music/Sound Player", "Listen and export any of the sound/music files located within the game client. The language of dialog will depend on your game version (or remote locale in the settings)."),
            new MainButton(W3DT.Properties.Resources.inv_inscription_scroll, typeof(ArtExplorerWindow), "Artwork Viewer", "Browse all of the artwork and texture files available within the game client."),
            new MainButton(W3DT.Properties.Resources.inv_misc_book_09, null, "Spellbook", "Browse and export spells from within the game; magical!"),
            new MainButton(W3DT.Properties.Resources.inv_misc_punchcards_blue, typeof(DBCViewer), "DBC/DB2 Viewer", "DBC/DB2 files are client databases that contain various data for the game, mostly strings of localized information; This tool allows you to browse through them."),
            new MainButton(W3DT.Properties.Resources.inv_misc_enggizmos_30, typeof(SettingsForm), "Settings", "Adjust how W3DT works to your liking!"),
            new MainButton(W3DT.Properties.Resources.inv_misc_questionmark, null, "Support/Information", "Details on how to get support for W3DT along with some information about the application and it's development.")
        };

        // Settings labels //
        public static readonly string CURRENT_VERSION_STRING = "Current Version: {0}";
        public static readonly string CDN_HOST_STRING = "Remote CDN Host: {0}";
        public static readonly string WOW_DIRECTORY_STRING = "Local Directory: {0}";

        // Explorer Status //
        public static readonly string GENERIC_WINDOW_SEARCH_STATE = "{0} Files Found ({1})";
        public static readonly string MAP_SEARCH_STATE = "{0} maps found ({1})";
        public static readonly string SEARCH_STATE_SEARCHING = "Searching...";
        public static readonly string SEARCH_STATE_DONE = "Done";

        // Map Viewer //
        public static readonly string MAP_VIEWER_TILE_STATUS = "{0}/{1} Tiles";
        public static readonly string MAP_VIEWER_WARNING_TITLE = "Woah!";
        public static readonly string MAP_VIEWER_WARNING = string.Format("Warning!{0}You've selected to render and export a fairly large area of terrain. This may take a while, and depending on environment quality, may use a lot of disk space.{0}Are you sure you want to do this?", DOUBLE_NEWLINE);
        public static readonly string MAP_VIEWER_WARNING_LARGE = string.Format("WARNING!{0}You've selected to render and export a VERY large area of terrain. This is going to take a LOT of time and will use up a LOT of disk space.{0}Are you sure you want to do this?", DOUBLE_NEWLINE);
        public static readonly string MAP_VIEWER_WARNING_INSANE = string.Format("HOLY CRUMBS, SLOW DOWN THERE!{0}You've bravely selected to render and export an INSANELY LARGE area of terrain. This is going to take an INCREDIBLE amount of time and use stupid amounts of disk space.{0}Are you absolutely sure this is the fate you choose?", DOUBLE_NEWLINE);
        
        // External //
        public static readonly string FINAL_BOSS_URL = "http://finalboss.tv/";
        public static readonly string KRUITHNE_URL = "http://www.kruithne.net/";
    }
}
