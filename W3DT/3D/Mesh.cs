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
        private List<UV> uvs;
        private List<Face> faces;

        public int VertCount { get { return verts.Count; } }
        public int FaceCount { get { return faces.Count; } }
        public int UVCount { get { return uvs.Count; } }

        public string Name { get; private set; }

        public Mesh(string name = "Unnamed Mesh")
        {
            verts = new List<Position>();
            faces = new List<Face>();
            uvs = new List<UV>();

            Name = name;
        }

        public void addVert(Position vert)
        {
            verts.Add(vert);
        }

        public void addUV(UV uv)
        {
            uvs.Add(uv);
        }

        public void addFace(uint texID, Colour4 colour, params int[] points)
        {
            Face face = new Face(texID, colour);

            foreach (int point in points)
                face.addPoint(verts[point], uvs[point]);

            if (face.PointCount > 0)
                faces.Add(face);
        }

        public override void Draw(OpenGL gl)
        {
            gl.Color(1.0F, 1.0F, 1.0F, 1.0F);
            foreach (Face face in faces)
                face.Draw(gl);
        }

        public override string ToString()
        {
            return string.Format("Mesh [{0}] => {0} Verts, {1} UVs, {2} Faces.", Name, VertCount, UVCount, FaceCount);
        }
    }
}
