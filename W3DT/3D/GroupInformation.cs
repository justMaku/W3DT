using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class GroupInformation
    {
        public UInt32 Flags { get; set; }
        public Position BoundingBoxLow { get; set; }
        public Position BoundingBoxHigh { get; set; }
        public Int32 NameOffset { get; set; }

        public static GroupInformation Read(FormatBase input)
        {
            GroupInformation temp = new GroupInformation();
            Stuffer.Stuff(temp, input, "GroupInformation", true);
            return temp;
        }
    }
}
