using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT.Events;
using W3DT.Formats;
using W3DT.Formats.WDT;
using W3DT.Formats.ADT;
using W3DT._3D;

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
                if (!Program.IsCASCReady)
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
                    WaveFrontWriter ob = new WaveFrontWriter("test_terrain.obj");
                    ob.UseTextures = false;
                    ob.UseNormals = false;

                    for (int y = 0; y < 64; y++)
                    {
                        for (int x = 0; x < 64; x++)
                        {
                            if (mainChunk.map[x, y])
                            {
                                string pathBase = string.Format(@"World\Maps\{0}\{0}_{1}_{2}", mapName, x, y);
                                string adtPath = pathBase + ".adt";
                                string texPath = pathBase + "_tex0.adt";
                                string objPath = pathBase + "_obj0.adt";

                                Program.CASCEngine.SaveFileTo(adtPath, Constants.TEMP_DIRECTORY);
                                Program.CASCEngine.SaveFileTo(texPath, Constants.TEMP_DIRECTORY);
                                Program.CASCEngine.SaveFileTo(objPath, Constants.TEMP_DIRECTORY);

                                string adtTempPath = Path.Combine(Constants.TEMP_DIRECTORY, adtPath);
                                string texTempPath = Path.Combine(Constants.TEMP_DIRECTORY, texPath);
                                string objTempPath = Path.Combine(Constants.TEMP_DIRECTORY, objPath);

                                try
                                {
                                    ADTFile adt = new ADTFile(adtTempPath, ADTFileType.ROOT);
                                    adt.parse();

                                    ADTFile tex = new ADTFile(texTempPath, ADTFileType.TEX);
                                    tex.parse();

                                    ADTFile obj = new ADTFile(objTempPath, ADTFileType.OBJ);
                                    obj.parse();

                                    foreach (Chunk_MCNK soupChunk in adt.getChunksByID(Chunk_MCNK.Magic))
                                    {
                                        Chunk_MCVT hmChunk = (Chunk_MCVT)soupChunk.getChunk(Chunk_MCVT.Magic);

                                        Mesh mesh = new Mesh();
                                        int v = 0;

                                        float pX = soupChunk.position.X;
                                        float pY = soupChunk.position.Y;
                                        float pZ = soupChunk.position.Z;

                                        int ofs = 10;
                                        for (int sX = 0; sX < 8; sX++)
                                        {
                                            for (int sY = 0; sY < 8; sY++)
                                            {
                                                int cIndex = ofs - 1;
                                                int tlIndex = cIndex - 9;
                                                int blIndex = cIndex + 8;

                                                float tl = hmChunk.vertices[tlIndex];
                                                float tr = hmChunk.vertices[tlIndex + 1];
                                                float bl = hmChunk.vertices[blIndex];
                                                float br = hmChunk.vertices[blIndex + 1];
                                                float c = hmChunk.vertices[cIndex];

                                                //int xD = x * (8 * 256);
                                                //int yD = y * (8 * 256);

                                                mesh.addVert(new Position(pX + sX, tl + pZ, pY + sY)); // + 0
                                                mesh.addVert(new Position(pX + sX, tr + pZ, pY + sY + 1)); // + 1
                                                mesh.addVert(new Position(pX + sX + 1, bl + pZ, pY + sY)); // + 2
                                                mesh.addVert(new Position(pX + sX + 1, br + pZ, pY + sY + 1)); // + 3
                                                mesh.addVert(new Position(pX + sX + 0.5f, c + pZ, pY + sY + 0.5f)); // + 4;

                                                mesh.addFace(v, v + 2, v + 4);
                                                mesh.addFace(v + 1, v + 3, v + 4);
                                                mesh.addFace(v, v + 1, v + 4);
                                                mesh.addFace(v + 2, v + 3, v + 4);

                                                v += 5;
                                                ofs += 1;
                                            }
                                            ofs += 9;
                                        }

                                        ob.addMesh(mesh);
                                    }
                                }
                                catch (ADTException ex)
                                {
                                    LogWrite("Unable to process tile {0},{1} due to exception: {2}", x, y, ex.Message);
                                }
                            }
                        }
                    }

                    ob.Write();
                    ob.Close();
                }

                // Job's done.
                EventManager.Trigger_MapExportDone(true);
            }
            catch (Exception e)
            {
                EventManager.Trigger_MapExportDone(false, e.Message);
            }
        }

        private void LogWrite(string message, params object[] data)
        {
            Log.Write("RunnerMapExport: " + message, data);
        }
    }
}
