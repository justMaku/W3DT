using System;
using W3DT.Formats;

namespace W3DT._3D
{
    public class DoodadSet 
    {
        public string Name { get; private set; }
        public UInt32 FirstIndex { get; private set; }
        public UInt32 Count { get; private set; }

        public DoodadSet(string name, UInt32 firstIndex, UInt32 count)
        {
            Name = name;
            FirstIndex = firstIndex;
            Count = count;
        }

        public static DoodadSet Read(FormatBase input)
        {
            return new DoodadSet(input.readString(20), input.readUInt32(), input.readUInt32());
        }
    }
}
