using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Face
    {
        private List<Position> points;

        public Face()
        {
            points = new List<Position>();
        }

        public void addPoint(Position point)
        {
            points.Add(point);
        }

        public int getPointCount()
        {
            return points.Count;
        }

        public void Draw(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_POLYGON);

            foreach (Position point in points)
                gl.Vertex(point.X, point.Y, point.Z);

            gl.End();
        }
    }
}
