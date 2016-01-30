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

        public int VertCount { get { return verts.Count; } }
        public int FaceCount { get { return faces.Count; } }

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
                if (point >= 0 && point < vertCount)
                    face.addPoint(verts[point]);

            if (face.PointCount > 0)
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
