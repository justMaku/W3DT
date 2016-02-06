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
            LogWrite("Beginning export of {0}...", mapName);

            try
            {
                if (!Program.IsCASCReady())
                    throw new MapExportException("CASC file engine is not loaded.");

                string wdtPath = string.Format(@"World\Maps\{0}\{0}.wdt", mapName);
                Program.CASCEngine.SaveFileTo(wdtPath, Constants.TEMP_DIRECTORY);

                WDTFile headerFile = new WDTFile(Path.Combine(Constants.TEMP_DIRECTORY, wdtPath));
                headerFile.parse();

                if (!headerFile.Chunks.Any(c => c.ChunkID == Chunk_MPHD.Magic))
                    throw new MapExportException("Invalid map header (WDT)");

                // ToDo: Check if world WMO exists and load it.

                // Ensure we have a MAIN chunk before trying to process terrain.
                Chunk_MAIN mainChunk = (Chunk_MAIN)headerFile.Chunks.Where(c => c.ChunkID == Chunk_MAIN.Magic).FirstOrDefault();
                if (mainChunk != null)
                {
                    for (int y = 0; y < 64; y++)
                    {
                        for (int x = 0; x < 64; x++)
                        {
                            if (mainChunk.map[x, y])
                            {
                                string adtPath = string.Format(@"World\Maps\{0}\{0}_{1}_{2}.adt", mapName, x, y);
                                Program.CASCEngine.SaveFileTo(adtPath, Constants.TEMP_DIRECTORY);

                                string adtTempPath = Path.Combine(Constants.TEMP_DIRECTORY, adtPath);
                                // ToDo: Read ADT files.
                            }
                        }
                    }
                }

                // ToDo: Push all data into a mesh and export it.

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
