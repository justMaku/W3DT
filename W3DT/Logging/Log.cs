using System;
using System.Collections.Concurrent;
using System.IO;
using W3DT.Logging;

namespace W3DT
{
    class Log
    {
        public static ConcurrentQueue<string> Pipe;
        private static Lumbermill runner;

        public static void Initialize(string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            Pipe = new ConcurrentQueue<string>();

            runner = new Lumbermill(new StreamWriter(file, true));
            runner.Begin();

            string year = DateTime.Now.ToString("yyyy");
            string monthDay = DateTime.Now.ToString("MMdd");

            Write(string.Format("Captain's log, USS Enterprise (NCC-{0}-D), {1}", year, monthDay));
        }

        public static void Write(string message, params object[] args)
        {
            Write(string.Format(message, args));
        }

        public static void Write(string message)
        {
            Pipe.Enqueue(message);
        }

        public static void Dispose()
        {
            if (runner != null)
            {
                runner.Kill();
                runner = null;
            }
        }
    }
}
