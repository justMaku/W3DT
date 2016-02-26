using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using W3DT.CASC;

namespace W3DT.JSONContainers
{
    class Settings
    {
        public bool AutomaticUpdates { get; set; }
        public bool ShowSourceSelector { get; set; }
        public bool UseRemote { get; set; }
        public string WoWDirectory { get; set; }
        public string RemoteHost { get; set; }
        public string RemoteHostPath { get; set; }
        public WoWVersion RemoteClientVersion { get; set; }
        public int SoundPlayerVolume { get; set; }
        public string FileLocale { get; set; }
        public bool AllowMultipleSoundPlayers { get; set; }
        public bool FirstTime { get; set; }

        public Settings()
        {
            // Set default values.
            AutomaticUpdates = true;
            UseRemote = false;
            ShowSourceSelector = true;
            WoWDirectory = null;
            RemoteHost = null;
            RemoteHostPath = null;
            RemoteClientVersion = new WoWVersion("wow", "Live (Retail)");
            SoundPlayerVolume = 100;
            FileLocale = Locale.Default.ID;
            AllowMultipleSoundPlayers = false;
            FirstTime = true;
        }

        public void Persist(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }

        public void Persist()
        {
            Persist(Constants.SETTINGS_FILE);
        }
    }
}
