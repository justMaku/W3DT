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
        private short index = 0;
        private Colour4 colour;

        public int[] Offset { get; private set; }
        public uint TextureID { get; private set; }
        public int PointCount { get { return points.Length; } }

        public Face(uint texID, Colour4 colour)
        {
            points = new Position[3];
            uvs = new UV[3];
            Offset = new int[3];

            TextureID = texID;
            this.colour = colour;
        }

        public void addPoint(Position point, UV uv, int offset)
        {
            if (index == 3)
            {
                Log.Write("WARNING: Trying to add more than 3 verts to a triangle?");
                return;
            }

            points[index] = point;
            uvs[index] = uv;

            // Used to identify the position this face took from the
            // stack it was produced from. IE index in a WMO group.
            Offset[index] = offset;

            index++;
        }

        public void Draw(OpenGL gl)
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID);

            int[] amb_diff = { colour.a, colour.b, colour.g, colour.a };
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE, amb_diff);

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
