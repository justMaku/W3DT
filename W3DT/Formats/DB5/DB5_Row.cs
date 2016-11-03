using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W3DT.Formats.DB2;

namespace W3DT.Formats.DB5
{
    class DB5_Row
    {
        private byte[] data;
        private DB5_Reader reader;

        public byte[] Data => data;

        public DB5_Row(DB5_Reader reader, byte[] data)
        {
            this.reader = reader;
            this.data = data;
        }

        public T GetField<T>(int field, int arrayIndex = 0)
        {
            DB2_Meta meta = reader.Meta[field];

            if (meta.Bits != 0x00 && meta.Bits != 0x08 && meta.Bits != 0x10 && meta.Bits != 0x18 && meta.Bits != -32)
                throw new Exception("Unknown meta.Flags");

            int bytesCount = (32 - meta.Bits) >> 3;

            TypeCode code = Type.GetTypeCode(typeof(T));

            object value = null;

            switch (code)
            {
                case TypeCode.Byte:
                    if (meta.Bits != 0x18)
                        throw new Exception("TypeCode.Byte Unknown meta.Bits");

                    value = data[meta.Offset + bytesCount * arrayIndex];
                    break;

                case TypeCode.SByte:
                    if (meta.Bits != 0x18)
                        throw new Exception("TypeCode.SByte Unknown meta.Bits");

                    value = (sbyte)data[meta.Offset + bytesCount * arrayIndex];
                    break;

                case TypeCode.Int16:
                    if (meta.Bits != 0x10)
                        throw new Exception("TypeCode.Int16 Unknown meta.Bits");

                    value = BitConverter.ToInt16(data, meta.Offset + bytesCount * arrayIndex);
                    break;

                case TypeCode.UInt16:
                    if (meta.Bits != 0x10)
                        throw new Exception("TypeCode.UInt16 Unknown meta.Bits");

                    value = BitConverter.ToUInt16(data, meta.Offset + bytesCount * arrayIndex);
                    break;

                case TypeCode.Int32:
                    byte[] b1 = new byte[4];
                    Array.Copy(data, meta.Offset + bytesCount * arrayIndex, b1, 0, bytesCount);
                    value = BitConverter.ToInt32(b1, 0);
                    break;

                case TypeCode.UInt32:
                    byte[] b2 = new byte[4];
                    Array.Copy(data, meta.Offset + bytesCount * arrayIndex, b2, 0, bytesCount);
                    value = BitConverter.ToUInt32(b2, 0);
                    break;

                case TypeCode.Int64:
                    byte[] b3 = new byte[8];
                    Array.Copy(data, meta.Offset + bytesCount * arrayIndex, b3, 0, bytesCount);
                    value = BitConverter.ToInt64(b3, 0);
                    break;

                case TypeCode.UInt64:
                    byte[] b4 = new byte[8];
                    Array.Copy(data, meta.Offset + bytesCount * arrayIndex, b4, 0, bytesCount);
                    value = BitConverter.ToUInt64(b4, 0);
                    break;

                case TypeCode.String:
                    if (meta.Bits != 0x00)
                        throw new Exception("TypeCode.String Unknown meta.Bits");

                    byte[] b5 = new byte[4];
                    Array.Copy(data, meta.Offset + bytesCount * arrayIndex, b5, 0, bytesCount);
                    int start = BitConverter.ToInt32(b5, 0), len = 0;

                    while (reader.StringTable[start + len] != 0)
                        len++;

                    value = Encoding.UTF8.GetString(reader.StringTable, start, len);
                    break;

                case TypeCode.Single:
                    if (meta.Bits != 0x00)
                        throw new Exception("TypeCode.Single Unknown meta.Bits");

                    value = BitConverter.ToSingle(data, meta.Offset + bytesCount * arrayIndex);
                    break;

                default:
                    throw new Exception("Invalid TypeCode: " + code);
            }

            return (T)value;
        }
    }
}
