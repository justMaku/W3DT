using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace W3DT.JSONContainers
{
    class Settings
    {
        public bool AutomaticUpdates { get; set; }
        public bool ShowSourceSelector { get; set; }
        public bool UseRemote { get; set; }

        public Settings()
        {
            // Set default values.
            AutomaticUpdates = true;
            UseRemote = false;
            ShowSourceSelector = true;

        }

        public void Persist(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
