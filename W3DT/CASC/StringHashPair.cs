namespace W3DT.CASC
{
    class StringHashPair
    {
        public ulong Hash { get; private set; }
        public string Value { get; private set; }

        public StringHashPair(ulong hash, string value)
        {
            Hash = hash;
            Value = value;
        }

        public override string ToString()
        {
 	         return Value;
        }
    }
}
