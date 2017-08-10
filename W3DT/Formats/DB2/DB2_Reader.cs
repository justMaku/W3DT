using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace W3DT.Formats.DB2
{
    class DB2_Reader : IEnumerable<KeyValuePair<int, DB2_Row>>
    {
        private const int HeaderSize = 48;
        private const uint Magic = 0x32424457;

        public int RecordsCount { get; private set; }
        public int FieldsCount { get; private set; }
        public int RecordSize { get; private set; }
        public int StringTableSize { get; private set; }
        public int MinIndex { get; private set; }
        public int MaxIndex { get; private set; }

        private readonly DB2_Row[] rows;
        public byte[] StringTable { get; private set; }

        readonly Dictionary<int, DB2_Row> index = new Dictionary<int, DB2_Row>();

        public DB2_Reader(string file) : this(new FileStream(file, FileMode.Open)) { }

        public DB2_Reader(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                if (reader.BaseStream.Length < HeaderSize)
                    throw new InvalidDataException("Corrupted DB2 file (header mis-match)");

                if (reader.ReadUInt32() != Magic)
                    throw new InvalidDataException("Corrupted DB2 file (magic)");

                RecordsCount = reader.ReadInt32();
                FieldsCount = reader.ReadInt32();
                RecordSize = reader.ReadInt32();
                StringTableSize = reader.ReadInt32();

                uint tableHash = reader.ReadUInt32();
                uint build = reader.ReadUInt32();
                uint unk1 = reader.ReadUInt32(); // Who dis?

                if (build > 12880)
                {
                    int minID = reader.ReadInt32();
                    int maxID = reader.ReadInt32();
                    int locale = reader.ReadInt32();
                    int unk5 = reader.ReadInt32();

                    if (maxID != 0)
                    {
                        var diff = maxID - minID + 1; // You okay, Blizz?
                        reader.ReadBytes(diff * 4);
                        reader.ReadBytes(diff * 2);
                    }
                }

                rows = new DB2_Row[RecordsCount];
                for (int i = 0; i < RecordsCount; i++)
                {
                    rows[i] = new DB2.DB2_Row(this, reader.ReadBytes(RecordSize));

                    int idx = BitConverter.ToInt32(rows[i].Data, 0);

                    if (idx < MinIndex)
                        MinIndex = idx;

                    if (idx > MaxIndex)
                        MaxIndex = idx;

                    index[idx] = rows[i];
                }

                StringTable = reader.ReadBytes(StringTableSize);
            }
        }

        public bool HasRow(int index)
        {
            return this.index.ContainsKey(index);
        }

        public DB2_Row GetRow(int index)
        {
            return this.index.ContainsKey(index) ? this.index[index] : null;
        }

        public IEnumerator<KeyValuePair<int, DB2_Row>> GetEnumerator()
        {
            return index.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return index.GetEnumerator();
        }
    }
}
