using System.Collections.Generic;

namespace W3DT.CASC
{
    class KeyService
    {
        private static Dictionary<ulong, byte[]> keys = new Dictionary<ulong, byte[]>();

        private static Salsa20 salsa = new Salsa20();

        public static Salsa20 SalsaInstance
        {
            get { return salsa; }
        }

        public static byte[] GetKey(ulong keyName)
        {
            byte[] key;
            keys.TryGetValue(keyName, out key);
            return key;
        }
    }
}
