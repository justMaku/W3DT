using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace W3DT.MapViewer
{
    public class MapCanvas
    {
        public MapCanvasTile[,] Images { get; private set; }
        public int XSize { get; private set; }
        public int YSize { get; private set; }

        private int MaxTiles;
        private int TileSize;
        private int CanvasSize;

        public MapCanvas(decimal xSize, decimal ySize, decimal maxTiles = 2, int tileSize = 256)
        {
            XSize = (int)Math.Ceiling((decimal) (xSize / maxTiles));
            YSize = (int)Math.Ceiling((decimal) (ySize / maxTiles));

            Images = new MapCanvasTile[XSize, YSize];

            CanvasSize = (int) maxTiles * tileSize;
            for (int x = 0; x < XSize; x++)
                for (int y = 0; y < YSize; y++)
                    Images[x, y] = new MapCanvasTile((int)maxTiles, tileSize);

            MaxTiles = (int) maxTiles;
            TileSize = tileSize;
        }

        public void Draw(Graphics g, int offsetX, int offsetY, int width, int height, Overlay overlay)
        {
            Rectangle bounds = new Rectangle(0, 0, width, height);
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    MapCanvasTile tile = Images[x, y];

                    int tileX = (x * CanvasSize);
                    int tileY = (y * CanvasSize);
                    int drawX = tileX + offsetX;
                    int drawY = tileY + offsetY;

                    Point topLeft = new Point(drawX, drawY);
                    Point bottomLeft = new Point(drawX, drawY + CanvasSize);
                    Point topRight = new Point(drawX + CanvasSize, drawY);
                    Point bottomRight = new Point(drawX + CanvasSize, drawY + CanvasSize);

                    tile.Active = (bounds.Contains(topLeft) || bounds.Contains(bottomLeft) || bounds.Contains(topRight) || bounds.Contains(bottomRight));

                    if (tile.Active)
                    {
                        tile.Draw(g, (x * CanvasSize) + offsetX, (y * CanvasSize) + offsetY);

                        // Overlay
                        for (int tX = 0; tX < MaxTiles; tX++)
                        {
                            for (int tY = 0; tY < MaxTiles; tY++)
                            {
                                int ofsTileX = TileSize * tX;
                                int ofsTileY = TileSize * tY;

                                Point tilePoint = new Point(tileX + ofsTileX, tileY + ofsTileY);
                                if (overlay.ShouldDrawOverlay(tilePoint))
                                    g.DrawImage(overlay.Image, drawX + ofsTileX, drawY + ofsTileY);
                            }
                        }
                    }
                }
            }
        }

        public void DrawToCanvas(string image, int x, int y)
        {
            MapCanvasTile tile = GetCanvas(x, y);

            int drawX = x - ((int)Math.Floor((decimal)(x / CanvasSize)) * CanvasSize);
            int drawY = y - ((int)Math.Floor((decimal)(y / CanvasSize)) * CanvasSize);

            tile.AddTile(image, drawX, drawY);
        }

        private MapCanvasTile GetCanvas(decimal x, decimal y)
        {
            decimal cX = x / (decimal)CanvasSize;
            decimal cY = y / (decimal)CanvasSize;

            return Images[(int)Math.Floor(cX), (int)Math.Floor(cY)];
        }

        public void Dispose()
        {
            foreach (MapCanvasTile tile in Images)
                tile.Dispose();
        }
    }
}
