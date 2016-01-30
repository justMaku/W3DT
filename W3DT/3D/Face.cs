using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Face
    {
        // ToDo: Hard-code these to be 3 in length.
        private List<Position> points;
        private List<UV> uvs;
        private uint textureID;

        public int PointCount { get { return points.Count; } }

        public Face(uint texID)
        {
            points = new List<Position>();
            uvs = new List<UV>();
            textureID = texID;
        }

        public void addPoint(Position point, UV uv)
        {
            points.Add(point);
            uvs.Add(uv);
        }

        public void Draw(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureID);
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < points.Count; i++)
            {
                Position point = points[i];
                UV uv = uvs[i];

                gl.TexCoord(uv.u, uv.v);
                gl.Vertex(point.X, point.Y, point.Z);
            }

            gl.End();
        }
    }
}
