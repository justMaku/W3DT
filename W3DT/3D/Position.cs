using W3DT.Formats;

namespace W3DT._3D
{
    public struct Position
    {
        public float X;
        public float Y;
        public float Z;

        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Position Read(FormatBase input)
        {
            return new Position(input.readFloat(), input.readFloat(), input.readFloat());
        }

        public override string ToString()
        {
            return string.Format("X: [{0}] Y: [{1}] Z: [{2}]", X, Y, Z);
        }
    }
}
