using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace W3DT.Helpers
{
    public class BitmapCanvas
    {
        public Bitmap[,] Images { get; private set; }
        public int XSize { get; private set; }
        public int YSize { get; private set; }

        private int MaxTiles;
        private int TileSize;
        private int CanvasSize;

        public BitmapCanvas(decimal xSize, decimal ySize, decimal maxTiles = 10, int tileSize = 256)
        {
            XSize = (int)Math.Ceiling((decimal) (xSize / maxTiles));
            YSize = (int)Math.Ceiling((decimal) (ySize / maxTiles));

            Images = new Bitmap[XSize, YSize];

            CanvasSize = (int) maxTiles * tileSize;
            for (int x = 0; x < XSize; x++)
                for (int y = 0; y < YSize; y++)
                    Images[x, y] = new Bitmap(CanvasSize, CanvasSize);

            MaxTiles = (int) maxTiles;
            TileSize = tileSize;
        }

        public void Draw(Graphics g, int offsetX, int offsetY)
        {
            for (int x = 0; x < XSize; x++)
                for (int y = 0; y < YSize; y++)
                    g.DrawImage(Images[x, y], (x * CanvasSize) + offsetX, (y * CanvasSize) + offsetY);
        }

        public void DrawToCanvas(Image image, int x, int y)
        {
            using (Graphics gfx = Graphics.FromImage(GetCanvas(x, y)))
            {
                int drawX = x - ((int)Math.Floor((decimal) (x / CanvasSize)) * CanvasSize);
                int drawY = y - ((int)Math.Floor((decimal) (y / CanvasSize)) * CanvasSize);

                gfx.DrawImage(image, new Rectangle(drawX, drawY, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);
            }
        }

        private Bitmap GetCanvas(decimal x, decimal y)
        {
            decimal cX = x / (decimal)CanvasSize;
            decimal cY = y / (decimal)CanvasSize;

            return Images[(int)Math.Floor(cX), (int)Math.Floor(cY)];
        }

        public void Dispose()
        {
            foreach (Bitmap image in Images)
                image.Dispose();
        }
    }
}
