using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats
{
    public class DBC_EmotesText : DBCTable
    {
        public DBC_EmotesText(DBCFile file) : base(file)
        {
            columns = new string[19];

            columns[0] = "ID";
            columns[1] = "Name";
            columns[2] = "EmoteID";

            for (int i = 0; i < 16; i++)
                columns[3 + i] = "EmoteText" + (i + 1);
        }

        public override DBCRecord read()
        {
            DBCRecord record = new DBCRecord((int) file.Header.FieldCount);

            record.addValue(file.readUInt32()); // ID
            record.addValue(file.getString(file.readUInt32())); // Name
            record.addValue(file.readUInt32()); // EmoteID

            for (int i = 0; i < 16; i++)
                record.addValue(file.readUInt32()); // EmoteText
            
            return record;
        }
    }
}
