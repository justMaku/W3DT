using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Mesh : _3DObject
    {
        private List<Position> verts;
        private List<Face> faces;

        public Mesh()
        {
            verts = new List<Position>();
            faces = new List<Face>();
        }

        public void addVert(Position vert)
        {
            verts.Add(vert);
        }

        public void addFace(params int[] points)
        {
            int vertCount = verts.Count;
            Face face = new Face();

            foreach (int point in points)
            {
                int index = point - 1;
                if (index >= 0 && index < vertCount)
                    face.addPoint(verts[index]);
            }

            if (face.getPointCount() > 0)
                faces.Add(face);
        }

        public override void Draw(OpenGL gl)
        {
            gl.Color(1, 0, 0);
            foreach (Face face in faces)
                face.Draw(gl);
        }
    }
}
