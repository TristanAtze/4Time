using _4Time.Async;
using _4Time.DataCore;
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
        public static void Main()
        {
            //Task.Run(YouTubeShortsBlocker.StartMonitoringAsync);
            ProgrammSetup();
            //Connector.OpenConnection();
            if (Connector.isConnected)
            {
                Thread.Sleep(50);
            }

            string activeUser = Environment.UserName.ToLower();

            Updater();
            if (activeUser == "gerd.kaufmann")
            {
                Crypto.FileListenerStart();
                Crypto.GetUserKeys();
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

        static void ProgrammSetup()
        {
            VersionControl();

            if (!AutostartHelper.IsApplicationInCurrentUserStartup())
            {
                AutostartHelper.AddApplicationToCurrentUserStartup();
            }
            Writer.DatabaseSetup();
            Writer.UserSetup();
        }

        static void Updater()
        {
            Process.Start("K:\\Team Academy\\Azubi_Jahrgang_2024\\Ben Sowieja\\4TimeUpdater\\Updater\\bin\\Debug\\net9.0\\Updater.exe");
        }        

        static void VersionControl()
        {
            if (!File.Exists("Version.txt"))
                File.Create("Version.txt").Close();

            string version = File.ReadAllText("Res/Version.txt");

            File.WriteAllText("Version.txt", version);
        }


    }

    public static class AutostartHelper
    {
        // Der Name, unter dem deine Anwendung in der Registry erscheinen soll.
        // Wähle hier etwas Eindeutiges, z.B. den Namen deiner Anwendung.
        private const string AppName = "4Time";

        // Pfad zum Registry-Schlüssel für den aktuellen Benutzer
        private const string RegistryPathCurrentUser = @"Software\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Fügt die Anwendung zum Autostart für den aktuellen Benutzer hinzu.
        /// </summary>
        public static void AddApplicationToCurrentUserStartup()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPathCurrentUser, true))
                {
                    if (key == null)
                    {
                        MessageBox.Show(
                            "Fehler: Registry-Schlüssel nicht gefunden.",
                            "Autostart",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    string executablePath = Application.ExecutablePath;
                    // string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                    key.SetValue(AppName, $"\"{executablePath}\"");
                    MessageBox.Show(
                        "Die Anwendung wurde erfolgreich zum Autostart hinzugefügt.",
                        "Autostart",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Hinzufügen zum Autostart: {ex.Message}",
                    "Autostart",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Entfernt die Anwendung aus dem Autostart für den aktuellen Benutzer.
        /// </summary>
        public static void RemoveApplicationFromCurrentUserStartup()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPathCurrentUser, true))
                {
                    if (key == null)
                    {
                        Console.WriteLine($"Fehler: Registry-Schlüssel nicht gefunden: HKCU\\{RegistryPathCurrentUser}");
                        return;
                    }

                    // Überprüfe, ob der Wert existiert, bevor du versuchst, ihn zu löschen.
                    if (key.GetValue(AppName) != null)
                    {
                        key.DeleteValue(AppName, false); 
                        Console.WriteLine($"Anwendung '{AppName}' wurde aus dem Autostart entfernt.");
                    }
                    else
                    {
                        Console.WriteLine($"Anwendung '{AppName}' war nicht im Autostart registriert.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Entfernen aus dem Autostart: {ex.Message}");
            }
        }

        /// <summary>
        /// Überprüft, ob die Anwendung für den aktuellen Benutzer im Autostart registriert ist.
        /// </summary>
        /// <returns>True, wenn registriert, sonst False.</returns>
        public static bool IsApplicationInCurrentUserStartup()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPathCurrentUser, false)) // false für reinen Lesezugriff
                {
                    if (key == null)
                    {
                        return false; // Schlüssel existiert nicht
                    }

                    string executablePath = Application.ExecutablePath;
                    // Für Konsolenanwendungen:
                    // string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                    object value = key.GetValue(AppName);

                    return value != null && value.ToString().Equals($"\"{executablePath}\"", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Überprüfen des Autostarts: {ex.Message}",
                    "Autostart",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }
    }
}
