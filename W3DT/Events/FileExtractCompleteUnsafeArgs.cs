namespace W3DT.Events
{
    class FileExtractCompleteUnsafeArgs : FileExtractArgs
    {
        public string File { get; private set; }

        public FileExtractCompleteUnsafeArgs(string file, bool success, int runnerID) : base(success, runnerID)
        {
            File = file;
        }
    }
}
