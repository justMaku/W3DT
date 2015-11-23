using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace W3DT
{
    class Log
    {
        private static TextWriter writer;

        public static void Initialize(string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            writer = new StreamWriter(file, true);

            string year = DateTime.Now.ToString("yyyy");
            string monthDay = DateTime.Now.ToString("MMdd");

            Write(String.Format("Captain's log, USS Enterprise (NCC-{0}-D), {1}", year, monthDay), false);
        }

        public static void Dispose()
        {
            writer.Close();
        }

        private static bool IsValid()
        {
            return writer != null;
        }

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff");
        }

        public static void Write(string message)
        {
            Write(message, true);
        }

        public static void Write(string message, bool timestamp)
        {
            if (IsValid())
            {
                if (timestamp)
                    writer.WriteLine(String.Format("[{0}] {1}", GetTimestamp(), message));
                else
                    writer.WriteLine(message);
            }

            writer.Flush();
            Debug.WriteLine(message);
        }
    }
}
