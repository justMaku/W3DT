namespace W3DT.Formats
{
    public class M2SkinFile : FormatBase
    {
        private static uint MAGIC = 0x4E494B53;

        public short[] Indicies { get; private set; }
        public short[] TrianglePoints { get; private set; }

        public M2SkinFile(string file) : base(file) { }

        public override void parse()
        {
            if (readUInt32() != MAGIC)
                throw new M2Exception(BaseName + " is not a valid SKIN file!");

            int nIndices = (int)readUInt32();
            int ofsIndices = (int)readUInt32();
            int nTriangles = (int)readUInt32();
            int ofsTriangles = (int)readUInt32();

            seekPosition(ofsIndices);
            Indicies = new short[nIndices];
            for (int i = 0; i < nIndices; i++)
                Indicies[i] = (short)readUInt16();

            seekPosition(ofsTriangles);
            TrianglePoints = new short[nTriangles];
            for (int i = 0; i < nTriangles; i++)
                TrianglePoints[i] = (short)readUInt16();
        }
    }
}
