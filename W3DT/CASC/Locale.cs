namespace W3DT.CASC
{
    class Locale
    {
        public static readonly Locale Default = new Locale("enUS", "English (American)", LocaleFlags.enUS);

        public static readonly Locale[] Values = {
            Default,
            new Locale("enGB", "English (UK)", LocaleFlags.enGB),
            new Locale("frFR", "French", LocaleFlags.frFR),
            new Locale("deDE", "Deutsch", LocaleFlags.deDE),
            new Locale("esES", "Spanish (Spain)", LocaleFlags.esES),
            new Locale("esMX", "Spanish (Mexico)", LocaleFlags.esMX),
            new Locale("ruRU", "Russian", LocaleFlags.ruRU),
            new Locale("itIT", "Italian", LocaleFlags.itIT),
            new Locale("ptBR", "Portuguese", LocaleFlags.ptBR),
            new Locale("zhCN", "Chinese (Simplified)", LocaleFlags.zhCN),
            new Locale("zhTW", "Chinese (Traditional)", LocaleFlags.zhTW),
            new Locale("koKR", "Korean", LocaleFlags.koKR)
        };

        public string ID { get; private set; }
        public string Name { get; private set; }
        public LocaleFlags Flags { get; private set; }

        public Locale(string id, string name, LocaleFlags flags)
        {
            ID = id;
            Name = name;
            Flags = flags;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", ID, Name);
        }

        public static Locale GetLocale(string id)
        {
            foreach (Locale locale in Values)
                if (locale.ID.Equals(id))
                    return locale;

            return Default;
        }

        public static Locale GetUserLocale()
        {
            return GetLocale(Program.Settings.FileLocale);
        }
    }
}
