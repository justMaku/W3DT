using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace W3DT._3D
{
    public class Mesh : _3DObject
    {
        public List<Position> Verts { get; private set; }
        public List<UV> UVs { get; private set; }
        public List<Face> Faces { get; private set; }
        public List<Position> Normals { get; private set; }

        public int VertCount { get { return Verts.Count; } }
        public int FaceCount { get { return Faces.Count; } }
        public int UVCount { get { return UVs.Count; } }
        public int NormalCount { get { return Normals.Count; } }

        public string Name { get; private set; }
        public bool ShouldRender { get; set; }

        public Mesh(string name = "Unnamed Mesh")
        {
            Verts = new List<Position>();
            Faces = new List<Face>();
            UVs = new List<UV>();
            Normals = new List<Position>();

            Name = name;
            ShouldRender = true;
        }

        public void addVert(Position vert)
        {
            Verts.Add(vert);
        }

        public void addUV(UV uv)
        {
            UVs.Add(uv);
        }

        public void addNormal(Position normal)
        {
            Normals.Add(normal);
        }

        public void addFace(uint texID, Colour4 colour, params int[] points)
        {
            Face face = new Face(texID, colour);

            foreach (int point in points)
                face.addPoint(Verts[point], UVs[point], point);

            if (face.PointCount > 0)
                Faces.Add(face);
        }

        public override void Draw(OpenGL gl)
        {
            gl.Color(1.0F, 1.0F, 1.0F, 1.0F);
            foreach (Face face in Faces)
                face.Draw(gl);
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToAdvancedString()
        {
            return string.Format("Mesh [{0}] => {0} Verts, {1} UVs, {2} Faces, {3} Normals.", Name, VertCount, UVCount, FaceCount, NormalCount);
        }
    }
}
