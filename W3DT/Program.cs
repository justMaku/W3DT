using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace W3DT
{
    static class Program
    {
        public static bool STOP_LOAD = false;

        #if DEBUG
            public static bool DO_UPDATE = false;
        #else
            public static bool DO_UPDATE = true;
        #endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Allow updating to be disabled.
            if (Array.Exists(args, input => input.ToLower().Equals("--noupdate")))
                DO_UPDATE = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());

            if (!STOP_LOAD)
                Application.Run(new MainForm());
        }
    }
}
