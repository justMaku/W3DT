using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Face
    {
        private Position[] points;
        private UV[] uvs;
        private uint textureID;
        private short index = 0;

        public int PointCount { get { return points.Length; } }

        public Face(uint texID)
        {
            points = new Position[3];
            uvs = new UV[3];

            textureID = texID;
        }

        public void addPoint(Position point, UV uv)
        {
            if (index == 3)
            {
                Log.Write("WARNING: Trying to add more than 3 verts to a triangle?");
                return;
            }

            points[index] = point;
            uvs[index] = uv;
            index++;
        }

        public void Draw(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureID);
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < 3; i++)
            {
                Position point = points[i];
                UV uv = uvs[i];

                gl.TexCoord(uv.u, uv.v);
                gl.Vertex(point.X, point.Y, point.Z);
            }

            gl.End();
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
}
