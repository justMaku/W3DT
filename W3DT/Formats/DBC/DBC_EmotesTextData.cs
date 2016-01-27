using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    class DBC_EmotesTextData : DBCTable
    {
        public DBC_EmotesTextData(DBCFile file) : base(file)
        {
            columns = new string[2];

            columns[0] = "ID";
            columns[1] = "Text";
        }

        public override DBCRecord read()
        {
            DBCRecord record = new DBCRecord((int)file.Header.FieldCount);

            record.addValue(file.readUInt32()); // ID
            record.addValue(file.getString(file.readUInt32())); // Text

            return record;
        }
    }
}
