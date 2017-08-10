namespace W3DT.CASC
{
    public class ExtractState
    {
        public object File { get; private set; }
        public bool State { get; set; }
        public int TrackerID { get; set; }

        public ExtractState(object file)
        {
            File = file;
            State = false;
        }
    }
}
