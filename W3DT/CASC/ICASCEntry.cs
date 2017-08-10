namespace W3DT.CASC
{
    public interface ICASCEntry
    {
        string Name { get; }
        ulong Hash { get; }
        int CompareTo(ICASCEntry entry, int col, CASCEngine casc);
    }
}
