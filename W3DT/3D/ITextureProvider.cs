namespace W3DT._3D
{
    public interface ITextureProvider
    {
        void addTexture(int extID, string file);
        string getFile(uint extID);
        string getFile(int extID);
        void clear();
    }
}
