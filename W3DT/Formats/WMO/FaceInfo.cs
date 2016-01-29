using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    public class FaceInfo
    {
        public byte flags { get; private set; }
        public byte materialID { get; private set; }

        public FaceInfo(byte flags, byte materialID)
        {
            this.flags = flags;
            this.materialID = materialID;
        }

        public static FaceInfo Read(FormatBase input)
        {
            return new FaceInfo(input.readUInt8(), input.readUInt8());
        }
    }
}
