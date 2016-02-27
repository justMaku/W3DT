using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using W3DT._3D;

namespace W3DT.Formats
{
    public class W3DFWriter : WriterBase
    {
        private static uint MAGIC = 0x46443357;
        private static uint MESH_MAGIC = 0x4853454D;
        private static uint VERT_MAGIC = 0x54524556;
        private static uint NORM_MAGIC = 0x4D524F4E;
        private static uint FACE_MAGIC = 0x45434146;

        //private StreamWriter writer;
        private BinaryWriter writer;
        private IEnumerable<Mesh> meshes;

        public W3DFWriter(string file, IEnumerable<Mesh> meshes)
        {
            if (File.Exists(file))
                File.Delete(file);

            writer = new BinaryWriter(File.OpenWrite(file));
            this.meshes = meshes;
        }

        public override void Write()
        {
            writer.Write(MAGIC);
            writer.Write((uint) meshes.Count());

            foreach (Mesh mesh in meshes)
            {
                writer.Write(MESH_MAGIC);
                writer.Write((uint)3); // Temp

                // Verts
                writer.Write(VERT_MAGIC);
                writer.Write((uint)(mesh.VertCount * (sizeof(float) * 3)));
                foreach (Position vert in mesh.Verts)
                {
                    writer.Write(vert.X);
                    writer.Write(vert.Y);
                    writer.Write(vert.Z);
                }

                // Normals
                writer.Write(NORM_MAGIC);
                writer.Write((uint)(mesh.NormalCount * (sizeof(float) * 3)));
                foreach (Position norm in mesh.Normals)
                {
                    writer.Write(norm.X);
                    writer.Write(norm.Y);
                    writer.Write(norm.Z);
                }

                // Faces
                writer.Write(FACE_MAGIC);
                writer.Write((uint)(mesh.FaceCount * (sizeof(int) * 3)));
                foreach (Face face in mesh.Faces)
                {
                    writer.Write((uint)face.Verts[0].Offset);
                    writer.Write((uint)face.Verts[1].Offset);
                    writer.Write((uint)face.Verts[2].Offset);
                }
            }
        }

        public override void Close()
        {
            writer.Close();
        }
    }
}