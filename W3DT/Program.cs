using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using W3DT.JSONContainers;
using W3DT.CASC;

namespace W3DT
{
    static class Program
    {
        public static string Version = "0.0.0.0";
        public static bool STOP_LOAD = false;
        public static bool CASC_LOADING = false;

        public static Settings Settings;
        public static CASCEngine CASCEngine;
        public static CASCFolder Root;

        #if DEBUG
            public static bool IS_DEBUG = true;
            public static bool DO_UPDATE = false;
        #else
            public static bool IS_DEBUG = false;
            public static bool DO_UPDATE = true;
        #endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!IS_DEBUG)
            {
                Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnThreadException);
            }

            Log.Initialize(Constants.LOG_FILE);
            Log.Write(AppDomain.CurrentDomain.FriendlyName + " " + String.Join(" ", args));

            Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Log.Write("Current build: " + Version);

            if (IS_DEBUG)
                Log.Write("DEBUG VERSION - Updating and error catching disabled.");

            // Allow updating to be disabled via parameters.
            if (Array.Exists(args, input => input.ToLower().Equals("--noupdate")))
            {
                Log.Write("NOT UPDATING: Disabled via arguments.");
                DO_UPDATE = false;
            }

            if (File.Exists(Constants.SETTINGS_FILE))
                Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Constants.SETTINGS_FILE));

            // Revert to default settings.
            if (Settings == null)
            {
                Log.Write("No settings file found, reverting to defaults.");
                Settings = new Settings();
            }

            Settings.Persist();

            // Dump settings to log.
            Log.Write(JsonConvert.SerializeObject(Settings, Formatting.Indented));

            if (!Settings.AutomaticUpdates)
            {
                Log.Write("NOT UPDATING: Automatic updating disabled in settings.");
                DO_UPDATE = false;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());

            if (!STOP_LOAD)
                Application.Run(new MainForm());

            Log.Dispose();
        }

        public static bool IsCASCReady()
        {
            if (CASCEngine == null)
                return false;

            return !CASC_LOADING;
        }

        private static void OnThreadException(object sender, EventArgs e)
        {
            Exception ex;

            if (e is UnhandledExceptionEventArgs)
                ex = (Exception)((UnhandledExceptionEventArgs)e).ExceptionObject;
            else if (e is ThreadExceptionEventArgs)
                ex = ((ThreadExceptionEventArgs)e).Exception;
            else
                ex = new Exception("Realize the truth, there is no exception.");

            Log.Write("KABLOOM - This is where it all went wrong.");
            Log.Write("Exception: " + ex.Message);
            Log.Write(ex.StackTrace);
            Log.Dispose();

            Process.Start("W3DTErrorGoblin.exe", Constants.LOG_FILE);
            Application.Exit();
        }
    }
}
