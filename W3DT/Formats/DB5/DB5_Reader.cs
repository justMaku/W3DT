using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using W3DT.Formats.DB2;

namespace W3DT.Formats.DB5
{
    class DB5_Reader : IEnumerable<KeyValuePair<int, DB5_Row>>
    {
        private const int HeaderSize = 48;
        private const uint Magic = 0x35424457;

        public int RecordsCount { get; private set; }
        public int FieldsCount { get; private set; }
        public int RecordSize { get; private set; }
        public int StringTableSize { get; private set; }
        public int MinIndex { get; private set; }
        public int MaxIndex { get; private set; }

        private readonly byte[] stringTable;
        private readonly DB2_Meta[] meta;

        public byte[] StringTable => stringTable;
        public DB2_Meta[] Meta => meta;

        private Dictionary<int, DB5_Row> index = new Dictionary<int, DB5_Row>();

        public DB5_Reader(string file) : this(new FileStream(file, FileMode.Open)) { }

        public DB5_Reader(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                if (reader.BaseStream.Length < HeaderSize)
                    throw new InvalidDataException("Corrupted DB5 file (header mis-match)");

                if (reader.ReadUInt32() != Magic)
                    throw new InvalidDataException("Corrupted DB5 file (magic)");

                RecordsCount = reader.ReadInt32();
                FieldsCount = reader.ReadInt32();
                RecordSize = reader.ReadInt32();
                StringTableSize = reader.ReadInt32();

                uint tableHash = reader.ReadUInt32();
                uint layoutHash = reader.ReadUInt32();

                MinIndex = reader.ReadInt32();
                MaxIndex = reader.ReadInt32();

                int locale = reader.ReadInt32();
                int copyTableSize = reader.ReadInt32();
                int flags = reader.ReadUInt16();
                int index = reader.ReadUInt16();

                bool isSparse = (flags & 0x1) != 0;
                bool hasIndex = (flags & 0x4) != 0;

                meta = new DB2_Meta[FieldsCount];

                for (int i = 0; i < meta.Length; i++)
                {
                    meta[i] = new DB2_Meta();
                    meta[i].Bits = reader.ReadInt16();
                    meta[i].Offset = reader.ReadInt16();
                }

                DB5_Row[] rows = new DB5_Row[RecordsCount];
                for (int i = 0; i < RecordsCount; i++)
                    rows[i] = new DB5_Row(this, reader.ReadBytes(RecordSize));

                stringTable = reader.ReadBytes(StringTableSize);

                if (isSparse)
                    throw new Exception("Sparse table encountered!");

                if (hasIndex)
                {
                    for (int i = 0; i < RecordsCount; i++)
                        this.index[reader.ReadInt32()] = rows[i];
                }
                else
                {
                    for (int i = 0; i < RecordsCount; i++)
                    {
                        int id = rows[i].Data.Skip(meta[index].Offset).Take((32 - this.meta[index].Bits) >> 3).Select((b, k) => b << k * 8).Sum();
                        this.index[id] = rows[i];
                    }
                }

                if (copyTableSize > 0)
                {
                    // Duplicate existing entry as new one.
                    int count = copyTableSize / 8;
                    for (int i = 0; i < count; i++)
                        this.index[reader.ReadInt32()] = this.index[reader.ReadInt32()];
                }
            }
        }

        public bool HasRow(int id)
        {
            return index.ContainsKey(id);
        }

        public DB5_Row GetRow(int id)
        {
            return index.ContainsKey(id) ? index[id] : null;
        }

        public IEnumerator<KeyValuePair<int, DB5_Row>> GetEnumerator()
        {
            return index.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return index.GetEnumerator();
        }
    }
}
