using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using W3DT.Events;
using SereniaBLPLib;

namespace W3DT.Runners
{
    class RunnerExportBLPtoPNG : RunnerBase
    {
        private string[] paths;
        private string dest;

        public RunnerExportBLPtoPNG(string[] paths, string dest)
        {
            this.paths = paths;
            this.dest = dest;
        }

        public override void Work()
        {
            bool success = false;

            try
            {
                foreach (string path in paths)
                {
                    string newFile = Path.Combine(dest, Path.GetFileNameWithoutExtension(path) + ".png");
                    Log.Write("Exporting {0} -> {1}", path, newFile);

                    using (BlpFile blp = new BlpFile(File.OpenRead(path)))
                    {
                        Bitmap bmp = blp.GetBitmap(0);
                        bmp.Save(newFile, ImageFormat.Png);
                        bmp.Dispose();
                    }
                }
                success = true;
            }
            catch (Exception e)
            {
                // We have failed, the elders will be disappointed.
                Log.Write("BLP to PNG export failed: {0}.", e.Message);
            }

            EventManager.Trigger_ExportBLPtoPNGComplete(success);
        }
    }
}
