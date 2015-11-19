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

        public Settings()
        {
            // Set default values.
            AutomaticUpdates = true;
        }

        public void Persist(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
