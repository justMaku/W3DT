namespace W3DT.Formats
{
    public class DBCRecord
    {
        public DBCRecord(int size)
        {
            data = new object[size];
        }

        public void setValue(int index, object obj)
        {
            data[index] = obj;
        }

        public void addValue(object obj)
        {
            setValue(addIndex, obj);
            addIndex++;
        }

        public object getValue(int index)
        {
            return data[index];
        }

        public object[] getValues()
        {
            return data;
        }

        private object[] data;
        private int addIndex = 0;
    }
}
