using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _4Time.DataCore
{
    internal class Connector 
    {
        internal const string ConnectionString = "Data Source = 192.168.6.131; Initial Catalog = _LK_TestDB; User ID = Azubi; Password = TestSQL2020#!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        internal static bool isConnected = false;
        internal static SqlConnection? connection = null;

        static Connector()
        {
           (string, string, string) FirstLastName = GetCurrentUser();
           FirstName = FirstLastName.Item2;
           LastName = FirstLastName.Item3;
        }

        public static string FirstName { get; set; }
        public static string LastName { get; set; }

        internal static bool IsDatabaseConnectionAvailable()
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.OpenAsync();
                Thread.Sleep(222);
                string testTableQuery = @" ";
                using var command = new SqlCommand(testTableQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static void OpenConnection()
        {
            if (IsDatabaseConnectionAvailable() && !isConnected)
            {
                connection = new SqlConnection(ConnectionString);
                connection.OpenAsync();
                Thread.Sleep(222);
                isConnected = true;
            }
            else
            {
                MessageBox.Show(
                "Fehler bei der Datenbankverbindung",
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
            }
        }

        internal static (string,string,string) GetCurrentUser()
        {
            string userName = Environment.UserName;
            string[] userNameSplitted = userName.Split(".");
            return ($"{userNameSplitted[0]}D209135{userNameSplitted[1]}",userNameSplitted[0],userNameSplitted[1]);
        }

        internal static void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    isConnected = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Fehler beim Schließen der Datenbankverbindung: " + ex.Message,
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
            }
        }
    }
}
