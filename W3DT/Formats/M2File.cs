using System;
using W3DT._3D;

namespace W3DT.Formats
{
    public class M2Exception : Exception
    {
        public M2Exception(string message) : base(message) { }
    }

    public class M2File : FormatBase
    {
        private static uint MD21_MAGIC = 0x3132444D;
        private static uint MD20_MAGIC = 0x3032444D;

        public uint Version { get; private set; }
        public string Name { get; private set; }
        public uint Flags { get; private set; }
        public Position[] Verts { get; private set; }
        public Position[] Normals { get; private set; }
        public UV[] UV1 { get; private set; }
        public UV[] UV2 { get; private set; }

        private M2SkinFile skin;

        public M2File(string file, M2SkinFile skin) : base(file)
        {
            this.skin = skin;
        }

        public override void parse()
        {
            Log.Write("Parsing model file {0} with skin {1}...", BaseName, skin.BaseName);
            skin.parse(); // Parse the given skin file.

            int ofs = 0;
            uint magic = readUInt32();

            if (!magic.Equals(MD20_MAGIC)) // Assume Legion+ chunked format, relate offset to chunk.
            {
                seekPosition(0); // Go back to the start.
                while (!isEndOfStream() && !isOutOfBounds(seek + 8))
                {
                    uint chunkID = readUInt32();
                    uint chunkSize = readUInt32();

                    if (chunkID == MD21_MAGIC)
                    {
                        ofs = seek;
                        break;
                    }
                    else
                    {
                        skip((int)chunkSize);
                    }
                }

                if (ofs == 0)
                    throw new M2Exception("Unable to find MD20 chunk inside MD21 file! Format evolved?");
            }

            // Start reading as MD20
            seekPosition(ofs); // Seek the beginning of the MD20 data.
            magic = readUInt32();

            if (magic != MD20_MAGIC)
                throw new M2Exception(string.Format("Data is not MD20 at {0}", ofs));

            /* 
             * 274+ Legion
             * ? - ? Warlords of Draenor
             * 273 Mists of Pandaria
             * 265-272 Cataclysm
             * 264 Wrath of the Lich King
             * 260-263 The Burning Crusade
             * ?-256 World of Warcraft
             */
            Version = readUInt32();
            int iName = (int)readUInt32(); // Contains trailing 0-byte.
            int ofsName = (int)readUInt32();
            Flags = readUInt32();
            int nGlobalSeq = (int)readUInt32();
            int ofsGlobalSeq = (int)readUInt32();
            int nAnims = (int)readUInt32();
            int ofsAnims = (int)readUInt32();
            int nAnimLookup = (int)readUInt32();
            int ofsAnimLookup = (int)readUInt32();
            int nBones = (int)readUInt32();
            int ofsBones = (int)readUInt32();
            int nKeyBoneLookup = (int)readUInt32();
            int ofsKeyBoneLookup = (int)readUInt32();
            int nVerts = (int)readUInt32();
            int ofsVerts = (int)readUInt32();

            seekPosition(ofs + ofsName);
            Name = readString(iName - 1);

            // Verts
            Verts = new Position[nVerts];
            Normals = new Position[nVerts];
            UV1 = new UV[nVerts];
            UV2 = new UV[nVerts];

            seekPosition(ofs + ofsVerts);
            for (int i = 0; i < nVerts; i++)
            {
                float x = readFloat();
                float z = readFloat() * - 1;
                float y = readFloat();

                Verts[i] = new Position(x, y, z);
                uint boneWeight = readUInt32(); // 4 * byte
                uint boneIndices = readUInt32(); // 4 * byte
                Normals[i] = Position.Read(this);
                UV1[i] = UV.Read(this);
                UV2[i] = UV.Read(this);
            }
        }

        public Mesh ToMesh()
        {
            Mesh mesh = new Mesh(Name);

            // Verts, normals, UV.
            for (int i = 0; i < Verts.Length; i++)
            {
                mesh.addVert(Verts[i]);
                mesh.addNormal(Normals[i]);
                mesh.addUV(UV1[i]);
            }

            // Faces (triangles)
            int ofs = 0;
            for (int i = 0; i < skin.TrianglePoints.Length / 3; i++)
            {
                mesh.addFace(skin.TrianglePoints[ofs], skin.TrianglePoints[ofs + 1], skin.TrianglePoints[ofs + 2]);
                ofs += 3;
            }

            return mesh;
        }
    }
}
