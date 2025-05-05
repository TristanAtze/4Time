using _4Time.DataCore;
using _4Time.FrontEnd;
using Microsoft.Win32;
using System.Diagnostics;
using Time4SellersApp;

namespace _4Time
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VersionControl();

            DoAutoStart();
            Writer.DatabaseSetup();
            Writer.UserSetup();

            Connector.OpenConnection();
            if (Connector.isConnected)
            {
                Thread.Sleep(222);
            }

            string activeUser = Environment.UserName.ToLower();
            Updater();
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
                Application.Run(new UserView());
            }
        }

        static void Updater()
        {
            Process.Start("K:\\Team Academy\\Azubi_Jahrgang_2024\\Ben Sowieja\\4TimeUpdater\\Updater\\bin\\Debug\\net9.0\\Updater.exe");
        }

        static void DoAutoStart()
        {
            try
            {
                string exePath = Process.GetCurrentProcess()?.MainModule?.FileName ?? "";

                if(exePath != "")
                {
                    RegistryKey? key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Run", writable: true);
                    key?.SetValue("AutoStartExample", $"\"{exePath}\"", RegistryValueKind.String);
                }
                Console.WriteLine("Erfolgreich zum Autostart hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Fehler beim Eintragen in den Autostart:");
                Console.Error.WriteLine(ex.Message);
            }
        }

        static void VersionControl()
        {
            string version = File.ReadAllText("res/Version.txt");

            if (!File.Exists("Version.txt"))
                File.Create("Version.txt").Close();

            File.WriteAllText("Version.txt", version);
        }
    }
}
