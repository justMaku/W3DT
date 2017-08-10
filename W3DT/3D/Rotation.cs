using W3DT.Formats;

namespace W3DT._3D
{
    public struct Rotation
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Rotation(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public static Rotation Read(FormatBase input)
        {
            return new Rotation(input.readFloat(), input.readFloat(), input.readFloat(), input.readFloat());
        }

        public override string ToString()
        {
            return string.Format("X: [{0}] Y: [{1}] Z: [{2}] W: [{3}]", X, Y, Z, W);
        }
    }
}
