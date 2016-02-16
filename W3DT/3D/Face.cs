using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Face
    {
        private short index = 0;

        public Vert[] Verts { get; private set; }
        public int PointCount { get { return Verts.Length; } }
        public uint TextureID { get; set; }
        public Colour4 Colour { get; set; }

        public Face()
        {
            Verts = new Vert[3];
            Colour = Colour4.White;
        }

        public void addPoint(Vert vert)
        {
            if (index == 3)
            {
                Log.Write("WARNING: Trying to add more than 3 verts to a triangle?");
                return;
            }

            Verts[index] = vert;
            index++;
        }

        public void Draw(OpenGL gl)
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID);

            int[] amb_diff = { Colour.A, Colour.B, Colour.G, Colour.A };
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE, amb_diff);

            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < 3; i++)
            {
                Position point = Verts[i].Point;
                UV uv = Verts[i].UV;

                if (uv != null)
                    gl.TexCoord(uv.U, uv.V);

                gl.Vertex(point.X, point.Y, point.Z);
            }

            gl.End();
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
}
