using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using W3DT.Events;
using SereniaBLPLib;

namespace W3DT.Runners
{
    public class RunnerMapBuilder : RunnerBase
    {
        private Regex pattern = new Regex(@"^map(\d+)_(\d+)$");
        private string[] files;

        public RunnerMapBuilder(string[] files)
        {
            this.files = files;
        }

        public override void Work()
        {
            SortedDictionary<int, SortedDictionary<int, string>> map = new SortedDictionary<int, SortedDictionary<int, string>>();

            int xSize = 0;
            int ySize = 0;
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Match match = pattern.Match(fileName);

                if (match.Success)
                {
                    int xx = int.Parse(match.Groups[1].Value);
                    int yy = int.Parse(match.Groups[2].Value);

                    if (!map.ContainsKey(xx))
                    {
                        map.Add(xx, new SortedDictionary<int, string>());
                        xSize++;
                    }

                    SortedDictionary<int, string> node = map[xx];
                    node.Add(yy, file);

                    if (node.Count > ySize)
                        ySize = node.Count;
                }
            }

            Bitmap canvas = new Bitmap(xSize * 256, ySize * 256);
            using (Graphics gfx = Graphics.FromImage(canvas))
            {
                int iX = 0;
                foreach (KeyValuePair<int, SortedDictionary<int, string>> node in map)
                {
                    int iY = 0;
                    foreach (KeyValuePair<int, string> subNode in node.Value)
                    {
                        if (File.Exists(subNode.Value))
                            using (BlpFile blp = new BlpFile(File.OpenRead(subNode.Value)))
                                gfx.DrawImage(blp.GetBitmap(0), new Rectangle(iX * 256, iY * 256, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);

                        iY++;
                    }
                    iX++;
                }
            }

            EventManager.Trigger_MapBuildDone(new MapBuildDoneArgs(canvas));
        }
    }
}
