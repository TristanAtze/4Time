using _4Time.DataCore;
using _4Time.FrontEnd;
using AutoUpdaterDotNET;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO.Packaging;
using System.Windows.Forms;

namespace Time4SellersApp
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Updater();
            DoAutoStart();
            Writer.DatabaseSetup();
            Writer.UserSetup();
            
            string activeUser = Environment.UserName.ToLower();

            if (activeUser == "gerd.kaufmann")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AdminView());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        static void Updater()
        {
            AutoUpdater.ShowRemindLaterButton = true;
            AutoUpdater.UpdateMode = Mode.Forced; // oder Mode.Normal
            AutoUpdater.ShowSkipButton = true;
            AutoUpdater.Start("https://tristanatze.github.io/4Time/updates.xml");
        }

        static void DoAutoStart()
        {
            try
            {
                string exePath = Process.GetCurrentProcess().MainModule.FileName;

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Run", writable: true))
                {
                    key.SetValue("AutoStartExample", $"\"{exePath}\"", RegistryValueKind.String);
                }

                Console.WriteLine("Erfolgreich zum Autostart hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Fehler beim Eintragen in den Autostart:");
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
