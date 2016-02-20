using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using W3DT.Events;
using W3DT.CASC;
using SereniaBLPLib;

namespace W3DT.Runners
{
    public struct MapTileXY
    {
        public int X;
        public int Y;

        public MapTileXY(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class MapTileBounds
    {
        public int LowX = -1;
        public int LowY = -1;
        public int HighX = -1;
        public int HighY = -1;
    }

    public class RunnerBuildMinimap : RunnerBase
    {
        private static uint index = 1;
        private static Regex pattern = new Regex(@"^map(\d+)_(\d+)$");
        private CASCFile[] files;
        private MapTileXY[] positions;
        public uint Index { get; private set; }

        public RunnerBuildMinimap(CASCFile[] files)
        {
            this.files = files;

            // Index used to prevent overlap.
            Index = index;
            index++;
        }

        public override void Work()
        {
            if (!Program.IsCASCReady)
                return;

            positions = new MapTileXY[files.Length];
            MapTileBounds bounds = new MapTileBounds();

            for (int i = 0; i < files.Length; i++)
            {
                CASCFile file = files[i];
                string tileName = Path.GetFileNameWithoutExtension(file.FullName);
                Match match = pattern.Match(tileName);

                if (match.Success)
                {
                    int tileX = int.Parse(match.Groups[1].Value);
                    int tileY = int.Parse(match.Groups[2].Value);

                    // Update bounds.
                    if (bounds.LowX == -1 || tileX < bounds.LowX) bounds.LowX = tileX;
                    if (bounds.HighX == -1 || tileX > bounds.HighX) bounds.HighX = tileX;
                    if (bounds.LowY == -1 || tileY < bounds.LowY) bounds.LowY = tileY;
                    if (bounds.HighY == -1 || tileY > bounds.HighY) bounds.HighY = tileY;

                    positions[i] = new MapTileXY(tileX, tileY);
                }
                else
                {
                    // This should not happen.
                    positions[i] = new MapTileXY(0, 0);
                }
            }

            for (int i = 0; i < files.Length; i++)
            {
                CASCFile file = files[i];
                MapTileXY position = positions[i];

                string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);

                // Extract the file if we don't already have it cached.
                if (!File.Exists(tempPath))
                {
                    try
                    {
                        Program.CASCEngine.SaveFileTo(file.FullName, Constants.TEMP_DIRECTORY);
                    }
                    catch (Exception e)
                    {
                        Log.Write("Warning: Unable to extract minimap tile {0} -> {1}", file.FullName, e.Message);
                    }
                }

                if (File.Exists(tempPath))
                    EventManager.Trigger_MinimapTileDone(position, bounds, tempPath, Index);
            }
        }
    }
}
