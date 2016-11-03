using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3DT.Formats.DB2
{
    class DB2_Row
    {
        private readonly byte[] data;
        private readonly DB2_Reader reader;

        public byte[] Data => data;

        public DB2_Row(DB2_Reader reader, byte[] data)
        {
            this.reader = reader;
            this.data = data;
        }

        public T GetField<T>(int field)
        {
            object value;

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    int start = BitConverter.ToInt32(data, field * 4), len = 0;
                    while (reader.StringTable[start + len] != 0)
                        len++;
                    value = Encoding.UTF8.GetString(reader.StringTable, start, len);
                    return (T)value;
                case TypeCode.Int32:
                    value = BitConverter.ToInt32(data, field * 4);
                    return (T)value;
                case TypeCode.Single:
                    value = BitConverter.ToSingle(data, field * 4);
                    return (T)value;
                default:
                    return default(T);
            }
        }
    }
}
