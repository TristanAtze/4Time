using _4Time.DataCore;
using System;
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
            Writer.DatabaseSetup();
            Writer.UserSetup();

            string activeUser = Environment.UserName.ToLower();

            if (activeUser == "gerd.kaufmann")
            {
                MessageBox.Show(
                "Die Anwendung kann nicht auf diesem Computer gestartet werden.",
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
               
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
