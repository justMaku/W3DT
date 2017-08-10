using System;

namespace W3DT.Formats.DB3
{
    class DB3_Row
    {
        private byte[] data;
        private DB3_Reader reader;

        public byte[] Data => data;

        public DB3_Row(DB3_Reader reader, byte[] data)
        {
            this.reader = reader;
            this.data = data;
        }

        public unsafe T GetField<T>(int offset)
        {
            object value;

            fixed (byte *ptr = data)
            {
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.String:
                        string str;
                        int start = BitConverter.ToInt32(data, offset);
                        if (reader.StringTable.TryGetValue(start, out str))
                            value = str;
                        else
                            value = string.Empty;

                        return (T)value;
                    case TypeCode.SByte:
                        value = ptr[offset];
                        return (T)value;

                    case TypeCode.Byte:
                        value = ptr[offset];
                        return (T)value;

                    case TypeCode.Int16:
                        value = *(short*)(ptr + offset);
                        return (T)value;

                    case TypeCode.UInt16:
                        value = *(ushort*)(ptr + offset);
                        return (T)value;

                    case TypeCode.Int32:
                        value = *(int*)(ptr + offset);
                        return (T)value;

                    case TypeCode.UInt32:
                        value = *(uint*)(ptr + offset);
                        return (T)value;

                    case TypeCode.Single:
                        value = *(float*)(ptr + offset);
                        return (T)value;

                    default:
                        return default(T);
                }
            }
        }
    }
}
