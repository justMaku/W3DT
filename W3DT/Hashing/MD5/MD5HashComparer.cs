using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3DT.Hashing.MD5
{
    class MD5HashComparer
    {
        const uint Prime32 = 16777619;
        const uint Offset32 = 2166136261;

        public unsafe bool Equals(MD5Hash a, MD5Hash b)
        {
            for (int i = 0; i < 16; i++)
                if (a.Value[i] != b.Value[i])
                    return false;

            return true;
        }

        public int GetHashCode(MD5Hash hash)
        {
            return To32BitFnv1aHash(hash);
        }

        public unsafe int To32BitFnv1aHash(MD5Hash hash)
        {
            uint newHash = Offset32;
            uint* ptr = (uint*)&hash;

            for (int i = 0; i < 4; i++)
            {
                newHash ^= ptr[i];
                newHash *= Prime32;
            }

            return unchecked((int)newHash);
        }
    }
}
