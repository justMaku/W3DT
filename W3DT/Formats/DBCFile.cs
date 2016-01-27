using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace W3DT.Formats
{
    public struct DBCHeader
    {
        public UInt32 RecordCount;
        public UInt32 FieldCount;
        public UInt32 RecordSize;
        public UInt32 StringBlockSize;
    }

    public class DBCException : Exception
    {
        public DBCException(string message) : base(message) { }
    }

    public class DBCFile : FormatBase
    {
        private static readonly UInt32 MAGIC = 0x43424457;
        public DBCHeader Header { get; private set; }
        public DBCTable Table { get; private set; }
        private int stringRefOffset;
        //private byte[] stringData;

        public DBCFile(string path) : base(path)
        {
            if (readUInt32() != MAGIC)
                throw new Exception("Invalid DBC file format!");

            Header = readHeader();

            // Locate string data before processing.
            int preSeek = seek;
            skip((int) (Header.RecordCount * Header.RecordSize));
            stringRefOffset = seek;
            seekPosition(preSeek);

            Table = getTable(Path.GetFileNameWithoutExtension(path));
        }

        public string getString(int offset)
        {
            int preSeek = seek;
            seekPosition(stringRefOffset + offset);

            string result = readString();
            seekPosition(preSeek);

            return result;
        }

        public string getString(UInt32 offset)
        {
            return getString((int)offset);
        }

        private DBCHeader readHeader()
        {
            DBCHeader header = new DBCHeader();
            header.RecordCount = readUInt32();
            header.FieldCount = readUInt32();
            header.RecordSize = readUInt32();
            header.StringBlockSize = readUInt32();
            return header;
        }

        private DBCTable getTable(string fileName)
        {
            switch (fileName)
            {
                case "EmotesText": return new DBC_EmotesText(this);
            }
            throw new DBCException("DBC structure is unknown!");
        }
    }
}
