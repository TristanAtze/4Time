using Microsoft.Data.SqlClient;

namespace _4Time.DataCore;

internal class Connector
{
    /// <summary>
    /// Connection-String für die Verbindung zur Datenbank.
    /// </summary>
    internal const string ConnectionString = "Server=(localdb)\\LocalTestDB;Database=TestDB;TrustServerCertificate=True;";
        

    /// <summary>
    /// Boolean, der angibt, ob die Verbindung zur Datenbank hergestellt werden konnte.
    /// </summary>
    internal static bool IsConnected = false;

    /// <summary>
    /// SqlConnection-Objekt für die Verbindung zur Datenbank.
    /// </summary>
    private static readonly SqlConnection? SqlConnection = null;

    /// <summary>
    /// Konstruktor zur Initialisierung von allgemeinen Werten.
    /// </summary>
    static Connector()
    {
       (string, string) firstLastName = GetCurrentUser();
       FirstName = firstLastName.Item1;
       LastName = firstLastName.Item2;
    }

    /// <summary>
    /// Vorname des Benutzers. Wird anhand des Windows-Benutzernamens ermittelt.
    /// </summary>
    public static string FirstName { get; set; }

    /// <summary>
    /// Nachname des Benutzers. Wird anhand des Windows-Benutzernamens ermittelt.
    /// </summary>
    public static string LastName { get; set; }

    /// <summary>
    /// Überprüft, ob eine Verbindung zur Datenbank hergestellt werden kann.
    /// </summary>
    /// <returns>
    /// True → Verbindung möglich
    /// False → Verbindung nicht möglich
    /// </returns>
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

    /// <summary>
    /// Öffnet die Verbindung zur Datenbank, wenn sie noch nicht geöffnet ist.
    /// </summary>
    //internal static void OpenConnection()
    //{
    //    if (IsDatabaseConnectionAvailable() && !isConnected)
    //    {
    //        connection = new SqlConnection(CONNECTION_STRING);
    //        connection.OpenAsync();
    //        Thread.Sleep(222);
    //        isConnected = true;
    //    }
    //    else
    //    {
    //        MessageBox.Show(
    //        "Fehler bei der Datenbankverbindung",
    //        "Fehler",
    //        MessageBoxButtons.OK,
    //        MessageBoxIcon.Error
    //        );
    //    }
    //}

    /// <summary>
    /// Ermittelt den aktuellen Windows-Benutzernamen und erstellt einen eindeutigen Schüssel für die Verschlüsselung von Daten.
    /// </summary>
    /// <returns></returns>
    private static (string,string) GetCurrentUser()
    {
        var userName = Environment.UserName;
        string[] userNameSplit = userName.Split(" ");
        return (userNameSplit[0],userNameSplit[1]);
    }

    internal static void CloseConnection()
    {
        try
        {
            if (SqlConnection != null && SqlConnection.State == System.Data.ConnectionState.Open)
            {
                SqlConnection.Close();
                IsConnected = false;
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
