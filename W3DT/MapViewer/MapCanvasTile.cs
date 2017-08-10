using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SereniaBLPLib;

namespace W3DT.MapViewer
{
    public class MapCanvasTile
    {
        public class SubTile : IComparable<SubTile>
        {
            public string File;
            public int DrawX;
            public int DrawY;

            public SubTile(string image, int drawX, int drawY)
            {
                File = image;
                DrawX = drawX;
                DrawY = drawY;
            }

            public int CompareTo(SubTile that)
            {
                if (this.DrawX == that.DrawX)
                {
                    if (this.DrawY == that.DrawY)
                        return 0;

                    return this.DrawY < that.DrawY ? -1 : 1;
                }

                return this.DrawX < that.DrawX ? -1 : 1;
            }
        }

        private bool _active = false;
        private int canvasSize;
        private int tileSize;
        private Bitmap image;
        public List<SubTile> tiles { get; private set; }

        public MapCanvasTile(int maxTiles, int tileSize)
        {
            tiles = new List<SubTile>();
            canvasSize = maxTiles * tileSize;
            this.tileSize = tileSize;
        }

        public bool Active
        {
            get { return _active; }

            set
            {
                if (value != _active)
                {
                    DisposeImage();

                    if (value)
                        RedrawImage();

                    _active = value;
                }
            }
        }

        private void RedrawImage()
        {
            image = new Bitmap(canvasSize, canvasSize);
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (SubTile tile in tiles)
                {
                    using (BlpFile blp = new BlpFile(File.OpenRead(tile.File)))
                    {
                        g.DrawImage(
                            blp.GetBitmap(0),
                            new Rectangle(tile.DrawX, tile.DrawY, tileSize, tileSize),
                            new Rectangle(0, 0, tileSize, tileSize),
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }
        }

        public void AddTile(string image, int x, int y)
        {
            tiles.Add(new SubTile(image, x, y));

            if (Active)
            {
                DisposeImage();
                RedrawImage();
            }
        }

        public void Draw(Graphics g, int offsetX, int offsetY)
        {
            if (image != null)
                g.DrawImage(image, offsetX, offsetY);
        }

        public void Dispose()
        {
            DisposeImage();
            tiles.Clear();
        }

        private void DisposeImage()
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }
    }
}
