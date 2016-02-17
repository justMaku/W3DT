using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using W3DT._3D;
using W3DT.Runners;

namespace W3DT.Formats
{
    public class WaveFrontWriter : WriterBase
    {
        private const string FORMAT = "0.00000000";
        private TextureManager texManager;
        private StreamWriter obj;
        private StreamWriter mtl;

        private List<Mesh> meshes;

        private string mtlFile;
        private string name;
        private string targetDir;

        public bool UseNormals { get; private set; }
        public bool UseTextures { get; private set; }

        public WaveFrontWriter(string file, TextureManager texManager = null, bool textures = true, bool normals = true)
        {
            this.texManager = texManager;
            UseTextures = textures;
            UseNormals = normals;

            obj = new StreamWriter(file, false);

            if (UseTextures)
            {
                string mtlPath = Path.ChangeExtension(file, ".mtl");
                mtl = new StreamWriter(mtlPath, false);
                mtlFile = Path.GetFileName(mtlPath);
            }

            targetDir = Path.GetDirectoryName(file);
            name = Path.GetFileNameWithoutExtension(file);

            meshes = new List<Mesh>();
        }

        public void addMesh(Mesh mesh)
        {
            meshes.Add(mesh);
        }

        public override void Write()
        {
            obj.WriteLine("# World of Warcraft WMO exported using W3DT");
            obj.WriteLine("# https://github.com/Kruithne/W3DT/");
            nl(obj);

            if (UseTextures)
            {
                // Link material library.
                obj.WriteLine("mtllib " + mtlFile);
                nl(obj);
            }

            obj.WriteLine("o " + name);
            nl(obj);

            string faceFormat = "{0}";

            if (UseTextures)
                faceFormat += "/{0}";

            if (UseNormals)
                faceFormat += "/{0}";

            List<string> texList = new List<string>();
            int faceOffset = 1;
            foreach (Mesh mesh in meshes)
            {
                // Group header
                obj.WriteLine("  g " + mesh.Name);
                obj.WriteLine("  s 1");
                nl(obj);
                
                // Vertices
                foreach (Position vert in mesh.Verts)
                    obj.WriteLine(string.Format("    v {0} {1} {2}", vert.X.ToString(FORMAT), vert.Y.ToString(FORMAT), vert.Z.ToString(FORMAT)));

                obj.WriteLine("    # " + mesh.VertCount + " verts");
                nl(obj);

                // UVs
                if (UseTextures)
                {
                    foreach (UV uv in mesh.UVs)
                        obj.WriteLine(string.Format("    vt {0} {1}", uv.U.ToString(FORMAT), uv.V.ToString(FORMAT)));

                    obj.WriteLine("    # " + mesh.UVCount + " UVs");
                    nl(obj);
                }

                // Normals
                if (UseNormals)
                {
                    foreach (Position norm in mesh.Normals)
                        obj.WriteLine(string.Format("    vn {0} {1} {2}", norm.X.ToString(FORMAT), norm.Y.ToString(FORMAT), norm.Z.ToString(FORMAT)));

                    obj.WriteLine("    # " + mesh.NormalCount + " normals");
                    nl(obj);
                }

                // Faces
                uint previousTexID = 0xFF;
                foreach (Face face in mesh.Faces)
                {
                    if (UseTextures && face.TextureID != previousTexID)
                    {
                        string texPath = texManager.getFile(face.TextureID);
                        if (!texList.Contains(texPath))
                            texList.Add(texPath);

                        nl(obj);
                        obj.WriteLine("    usemtl " + Path.GetFileNameWithoutExtension(texPath));
                        previousTexID = face.TextureID;
                    }

                    int p1 = face.Verts[0].Offset + faceOffset;
                    int p2 = face.Verts[1].Offset + faceOffset;
                    int p3 = face.Verts[2].Offset + faceOffset;

                    string ofs1 = string.Format(faceFormat, p1);
                    string ofs2 = string.Format(faceFormat, p2);
                    string ofs3 = string.Format(faceFormat, p3);

                    obj.WriteLine(string.Format("    f {0} {1} {2}", ofs1, ofs2, ofs3));
                }
                faceOffset += mesh.VertCount;
                obj.WriteLine("    # " + mesh.FaceCount + " faces");
                nl(obj);
            }

            if (UseTextures)
            {
                foreach (string tex in texList)
                {
                    string file = Path.GetFileNameWithoutExtension(tex);

                    mtl.WriteLine("newmtl " + file);
                    mtl.WriteLine("illum 2");
                    mtl.WriteLine("Kd 1.0 1.0 1.0");
                    mtl.WriteLine("Ka 0.250000 0.250000 0.250000");
                    mtl.WriteLine("Ks 0.000000 0.000000 0.000000");
                    mtl.WriteLine("Ke 0.000000 0.000000 0.000000");
                    mtl.WriteLine("Ns 0.000000");
                    mtl.WriteLine("map_Kd -s 1 -1 1 " + file + ".png");
                    nl(mtl);
                }

                new RunnerExportBLPtoPNG(texList.ToArray(), targetDir).Begin();
            }
        }

        public override void Close()
        {
            obj.Close();

            if (UseTextures)
                mtl.Close();

            meshes.Clear();
        }
    }
}
