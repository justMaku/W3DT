using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    class Cube : _3DObject
    {
        public Cube(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public override void Draw(OpenGL gl)
        {
            gl.Color(R, G, B);

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(0.5, -0.5, -0.5);
            gl.Vertex(0.5, 0.5, -0.5);
            gl.Vertex(-0.5, 0.5, -0.5);
            gl.Vertex(-0.5, -0.5, -0.5);
            gl.End();

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(0.5, -0.5, 0.5);
            gl.Vertex(0.5, 0.5, 0.5);
            gl.Vertex(-0.5, 0.5, 0.5);
            gl.Vertex(-0.5, -0.5, 0.5);
            gl.End();

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(0.5, -0.5, -0.5);
            gl.Vertex(0.5, 0.5, -0.5);
            gl.Vertex(0.5, 0.5, 0.5);
            gl.Vertex(0.5, -0.5, 0.5);
            gl.End();

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(-0.5, -0.5, 0.5);
            gl.Vertex(-0.5, 0.5, 0.5);
            gl.Vertex(-0.5, 0.5, -0.5);
            gl.Vertex(-0.5, -0.5, -0.5);
            gl.End();

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(0.5, 0.5, 0.5);
            gl.Vertex(0.5, 0.5, -0.5);
            gl.Vertex(-0.5, 0.5, -0.5);
            gl.Vertex(-0.5, 0.5, 0.5);
            gl.End();

            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(0.5, -0.5, -0.5);
            gl.Vertex(0.5, -0.5, 0.5);
            gl.Vertex(-0.5, -0.5, 0.5);
            gl.Vertex(-0.5, -0.5, -0.5);
            gl.End();
        }

        private float R;
        private float G;
        private float B;
    }
}
