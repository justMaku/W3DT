using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT.Events;
using W3DT.Formats;
using W3DT.Formats.WDT;

namespace W3DT.Runners
{
    public class MapExportException : Exception
    {
        public MapExportException(string message) : base(message) { }
    }

    public class RunnerMapExport : RunnerBase
    {
        private string mapName;

        public RunnerMapExport(string mapName)
        {
            this.mapName = mapName;
        }

        public override void Work()
        {
            /*
             * ToDo:
             * 
             * - Locate WDT file and parse the data.
             * - Load the World WMO if one exists.
             * - Load ADT tiles and parse them.
             * - Plug all data into WaveFrontWriter
             * - Save as file.
             * 
             */

            LogWrite("Beginning export of {0}...", mapName);

            try
            {
                if (!Program.IsCASCReady())
                    throw new MapExportException("CASC file engine is not loaded.");

                string wdtPath = string.Format(@"World\Maps\{0}\{0}.wdt", mapName);
                Program.CASCEngine.SaveFileTo(wdtPath, Constants.TEMP_DIRECTORY);

                WDTFile headerFile = new WDTFile(Path.Combine(Constants.TEMP_DIRECTORY, wdtPath));
                headerFile.parse();

                // Job's done.
                EventManager.Trigger_MapExportDone(new MapExportDoneArgs(true));
            }
            catch (Exception e)
            {
                EventManager.Trigger_MapExportDone(new MapExportDoneArgs(false, e.Message));
            }
        }

        private void LogWrite(string message, params object[] data)
        {
            Log.Write("RunnerMapExport: " + message, data);
        }
    }
}
