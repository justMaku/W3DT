using System.Collections.Generic;
using System.IO;
using System.Drawing;
using W3DT.MapViewer;
using W3DT.Events;
using Hjg.Pngcs;
using SereniaBLPLib;

namespace W3DT.Runners
{
    public class RunnerExport2DMap : RunnerBase
    {
        private MapCanvas canvas;
        private string fileName;

        public RunnerExport2DMap(string fileName, MapCanvas canvas)
        {
            this.canvas = canvas;
            this.fileName = fileName;
        }

        public override void Work()
        {
            MapCanvasTile[,] tiles = canvas.Images;
            int rowCount = tiles.GetLength(1);
            int colCount = tiles.GetLength(0);

            ImageInfo info = new ImageInfo((colCount * canvas.MaxTiles) * 256, (rowCount * canvas.MaxTiles) * 256, 8, false);
            PngWriter writer = new PngWriter(File.OpenWrite(fileName), info);

            int lineOfs = 0;
            int tileLines = 256 * canvas.MaxTiles;
            int canvasCols = (colCount * canvas.MaxTiles) * 256;

            for (int tileY = 0; tileY < rowCount; tileY++)
            {
                Color[,] rows = new Color[tileLines, canvasCols];
                for (int tileX = 0; tileX < colCount; tileX++)
                {
                    List<MapCanvasTile.SubTile> subTiles = tiles[tileX, tileY].tiles;
                    subTiles.Sort((a, b) => a.CompareTo(b));

                    for (int subTileY = 0; subTileY < canvas.MaxTiles; subTileY++) 
                    {
                        for (int subTileX = 0; subTileX < canvas.MaxTiles; subTileX++) 
                        {
                            int subTileIndex = (subTileX * canvas.MaxTiles) + subTileY;

                            if (subTileIndex < subTiles.Count)
                            {
                                using (BlpFile blp = new BlpFile(File.OpenRead(subTiles[subTileIndex].File)))
                                using (Bitmap bmp = blp.GetBitmap(0))
                                {
                                    for (int pY = 0; pY < 256; pY++)
                                        for (int pX = 0; pX < 256; pX++)
                                            rows[pY + (subTileY * 256), pX + (subTileX * 256) + (tileX * (256 * canvas.MaxTiles))] = bmp.GetPixel(pX, pY);
                                }
                            }
                        }
                    }
                }

                for (int iLine = 0; iLine < tileLines; iLine++)
                {
                    byte[] compRow = new byte[canvasCols * 3];
                    for (int iCol = 0; iCol < canvasCols; iCol++)
                    {
                        Color color = rows[iLine, iCol];
                        int ofs = 3 * iCol;

                        compRow[ofs] = color.R;
                        compRow[ofs + 1] = color.G;
                        compRow[ofs + 2] = color.B;
                    }

                    writer.WriteRowByte(compRow, lineOfs);
                    lineOfs++;
                }
            }

            writer.End();
            EventManager.Trigger_MapExportDone2D();
        }
    }
}
