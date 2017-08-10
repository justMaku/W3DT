namespace W3DT.Formats
{
    abstract public class DBCTable
    {
        public DBCTable(DBCFile file)
        {
            this.file = file;

            records = new DBCRecord[file.Header.RecordCount];
            for (int i = 0; i < file.Header.RecordCount; i++)
                records[i] = read();
        }

        public int getColumnCount()
        {
            return columns.Length;
        }

        public string getColumnName(int index)
        {
            return index >= 0 && index < columns.Length ? columns[index] : "unspecified";
        }

        public DBCRecord[] getRecords()
        {
            return records;
        }

        public int getRecordCount()
        {
            return records.Length;
        }

        public DBCRecord getRecord(int index)
        {
            return records[index];
        }

        abstract public DBCRecord read();

        protected DBCFile file;
        protected string[] columns;
        private DBCRecord[] records;
    }
}
