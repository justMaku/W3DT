using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using W3DT.CASC;

namespace W3DT.Formats.DB3
{
    class DB3_Reader : IEnumerable<KeyValuePair<int, DB3_Row>>
    {
        private readonly int HeaderSize;
        private const uint DB3Magic = 0x33424457;
        private const uint DB4Magic = 0x34424457;

        public int RecordsCount { get; private set; }
        public int FieldsCount { get; private set; }
        public int RecordSize { get; private set; }
        public int StringTableSize { get; private set; }
        public int MinIndex { get; private set; }
        public int MaxIndex { get; private set; }

        public Dictionary<int, string> StringTable { get; private set; }
        private SortedDictionary<int, DB3_Row> index = new SortedDictionary<int, DB3_Row>();

        public DB3_Reader(string file) : this(new FileStream(file, FileMode.Open)) { }

        public DB3_Reader(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                if (reader.BaseStream.Length < HeaderSize)
                    throw new InvalidDataException("Corrupted DB3/DB4 file (header mis-match)");

                uint magic = reader.ReadUInt32();
                if (magic != DB3Magic && magic != DB4Magic)
                    throw new InvalidDataException("Corrupted DB3/DB4 file (magic)");

                HeaderSize = magic == DB3Magic ? 48 : 52;

                RecordsCount = reader.ReadInt32();
                FieldsCount = reader.ReadInt32();
                RecordSize = reader.ReadInt32();
                StringTableSize = reader.ReadInt32();

                uint tableHash = reader.ReadUInt32();
                uint build = reader.ReadUInt32();
                uint timeModified = reader.ReadUInt32();

                int minID = reader.ReadInt32();
                int maxID = reader.ReadInt32();
                int locale = reader.ReadInt32();
                int copyTableSize = reader.ReadInt32();

                if (magic == DB4Magic)
                    reader.ReadInt32(); // Meta flags.

                int stringTableStart = HeaderSize + RecordsCount * RecordSize;
                int stringTableEnd = stringTableStart + StringTableSize;

                int[] indexes = null;
                bool hasIndex = stringTableEnd + copyTableSize < reader.BaseStream.Length;

                if (hasIndex)
                {
                    reader.BaseStream.Position = stringTableEnd;
                    indexes = new int[RecordsCount];

                    for (int i = 0; i < RecordsCount; i++)
                        indexes[i] = reader.ReadInt32();
                }

                reader.BaseStream.Position = HeaderSize;

                for (int i = 0; i < RecordsCount; i++)
                {
                    byte[] recordBytes = reader.ReadBytes(RecordSize);

                    if (hasIndex)
                    {
                        byte[] newRecordBytes = new byte[RecordSize + 4];

                        Array.Copy(BitConverter.GetBytes(indexes[i]), newRecordBytes, 4);
                        Array.Copy(recordBytes, 0, newRecordBytes, 4, recordBytes.Length);

                        index.Add(indexes[i], new DB3_Row(this, newRecordBytes));
                    }
                    else
                    {
                        index.Add(BitConverter.ToInt32(recordBytes, 0), new DB3_Row(this, recordBytes));
                    }
                }

                reader.BaseStream.Position = stringTableStart;
                StringTable = new Dictionary<int, string>();

                while (reader.BaseStream.Position != stringTableEnd)
                {
                    int index = (int)reader.BaseStream.Position - stringTableStart;
                    StringTable[index] = reader.ReadCString();
                }

                long copyTablePosition = stringTableEnd + (hasIndex ? 4 * RecordsCount : 0);
                if (copyTablePosition != reader.BaseStream.Length && copyTableSize != 0)
                {
                    reader.BaseStream.Position = copyTablePosition;

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        int id = reader.ReadInt32();
                        int copy = reader.ReadInt32();

                        RecordsCount++;

                        DB3_Row copyRow = index[copy];
                        byte[] newData = new byte[copyRow.Data.Length];

                        Array.Copy(copyRow.Data, newData, newData.Length);
                        Array.Copy(BitConverter.GetBytes(id), newData, 4);

                        index.Add(id, new DB3_Row(this, newData));
                    }
                }
            }
        }

        public bool HasRow(int index)
        {
            return this.index.ContainsKey(index);
        }

        public DB3_Row GetRow(int index)
        {
            return this.index.ContainsKey(index) ? this.index[index] : null;
        }

        public IEnumerator<KeyValuePair<int, DB3_Row>> GetEnumerator()
        {
            return index.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return index.GetEnumerator();
        }
    }
}
