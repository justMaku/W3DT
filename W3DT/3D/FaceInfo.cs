using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Formats;

namespace W3DT._3D
{
    public class FaceInfo
    {
        public int flags { get; private set; }
        public int materialID { get; private set; }

        public FaceInfo(int flags, int materialID)
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
