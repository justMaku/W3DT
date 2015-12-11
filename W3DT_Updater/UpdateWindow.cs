using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace W3DT_Updater
{
    public partial class UpdateWindow : Form
    {
        private int attempts = 0;
        private static string PACKAGE_FILE = "update.zip";

        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void DoUpdate()
        {
            try
            {
                if (File.Exists(PACKAGE_FILE))
                {
                    using (ZipArchive archive = ZipFile.OpenRead(PACKAGE_FILE))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            // Ensure we don't try to over-write the running executable.
                            if (!entry.FullName.Equals(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name))
                            {
                                entry.ExtractToFile(Path.Combine(Directory.GetCurrentDirectory(), entry.FullName), true);
                                Debug.WriteLine("Extracting: " + entry.FullName);
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Encountered exception on extraction: " + ex.Message);
            }

            try
            {
                File.Delete(PACKAGE_FILE);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Encountered exception removing package: " + ex.Message);
            }

            Process.Start("W3DT.exe", "--noupdate");
            Close(); // We're done, let's go home.
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("W3DT");

            if (processes.Length == 0)
            {
                // No processes running, we should be safe to update.
                MainTimer.Enabled = false;
                DoUpdate();
            }
            else
            {
                // Processes still running, attempt to close them.
                try
                {
                    foreach (Process process in processes)
                    {
                        if (attempts >= 40) // Beyond 40 attempts, be vicious.
                            process.Kill();
                        else
                            process.CloseMainWindow();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception while halting process: " + ex.Message);
                }

                attempts += 1;
            }
        }
    }
}
