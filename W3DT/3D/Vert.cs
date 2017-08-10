namespace W3DT._3D
{
    public class Vert
    {
        public Position Point;
        public UV UV;
        public int Offset;

        public Vert(Position point, UV uv = null, int offset = -1)
        {
            Point = point;
            UV = uv;
            Offset = offset;
        }
    }
}
