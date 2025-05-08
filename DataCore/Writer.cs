using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;

namespace _4Time.DataCore;

internal class Writer : Connector
{
    private static readonly Dictionary<Type, string[]> UNSETTABLE_COLUMNS = new()
    {
        {typeof(User), ["UserID"] },
        {typeof(Entry), ["EntryID", "TimeStamp"] },
        {typeof(Category), ["CategoryID"] },
    };

    internal static void DatabaseSetup()
    {
        string query = File.ReadAllText("res/Setup.txt");

        var connection = new SqlConnection(CONNECTION_STRING);
        var command = new SqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    internal static void UserSetup()
    {
        string query = @"
                IF(NOT EXISTS (SELECT 1 FROM [dbo].[User] WHERE [FirstName] = @firstName AND [LastName] = @lastName))
                BEGIN
                    INSERT INTO [dbo].[User] ([FirstName], [LastName], [IsAdmin])
                    VALUES (@firstName, @lastName, @IsAdmin)
                END
            ";

        var connection = new SqlConnection(CONNECTION_STRING);
        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@firstName", Connector.FirstName.ToLower());
        command.Parameters.AddWithValue("@lastName", Connector.LastName.ToLower());
        command.Parameters.AddWithValue("@IsAdmin", false);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    internal static void Insert(string table, object obj)
    {
        Dictionary<string,object?> columns = [];

        //Alle Spalten ermitteln
        string schemaQuery = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
        var schemaConnection = new SqlConnection(CONNECTION_STRING);
        var schemaCommand = new SqlCommand(schemaQuery, schemaConnection);

        schemaConnection.Open();
        var schemaReader = schemaCommand.ExecuteReader();

        while(schemaReader.Read())
        {
            if (!UNSETTABLE_COLUMNS[obj.GetType()].Contains(schemaReader.GetString(0)))
                columns.Add(schemaReader.GetString(0), null);
        }
        schemaConnection.Close();

        //Spalten mit Werten füllen
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (columns.ContainsKey(prop.Name))
            {
                columns[prop.Name] = prop.GetValue(obj);
            }
        }

        //INSERT-Statement erstellen
        string query = $"INSERT INTO [dbo].[{table}] ";

        if(columns.Count > 0)
        {
            if (columns.TryGetValue("Start", out object? valueStart) && obj.GetType() == typeof(Entry))
            {
                columns["Start"] = Crypto.Encryption(valueStart?.ToString() ?? "");
            }

            if (columns.TryGetValue("End", out object? valueEnd) && obj.GetType() == typeof(Entry))
            {
                columns["End"] = Crypto.Encryption(valueEnd?.ToString() ?? "");
            }

            if (columns.TryGetValue("Comment", out object? valueComment) && obj.GetType() == typeof(Entry))
            {
                columns["Comment"] = Crypto.Encryption(valueComment?.ToString() ?? "");
            }

            query += "([" + string.Join("], [", columns.Keys) + "]) VALUES (";
            query += string.Join(", ", columns.Values.Select(v => v == null ? "NULL" : $"'{v}'")) + ")";
        }
        else
        {
            query += "DEFAULT VALUES";
        }

        var connection = new SqlConnection(CONNECTION_STRING);
        var command = new SqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    internal static void Update(string table, object obj, string[] condition)
    {
        Dictionary<string, object?> columns = [];
        //Alle Spalten ermitteln
        string schemaQuery = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
        var schemaConnection = new SqlConnection(CONNECTION_STRING);

        using (var schemaCommand = new SqlCommand(schemaQuery, schemaConnection))
        {
            schemaConnection.Open();
            var schemaReader = schemaCommand.ExecuteReader();
            while (schemaReader.Read())
            {
                if (!UNSETTABLE_COLUMNS[obj.GetType()].Contains(schemaReader.GetString(0)))
                {
                    columns.Add(schemaReader.GetString(0), null);
                }
            }
            schemaConnection.Close();
        };

        //Spalten mit Werten füllen
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (columns.ContainsKey(prop.Name))
            {
                if(prop.Name == "Start" || prop.Name == "End" || prop.Name == "Comment")
                {
                    columns[prop.Name] = Crypto.Encryption(prop.GetValue(obj)?.ToString() ?? "");
                }
                else
                    columns[prop.Name] = prop.GetValue(obj);
            }
        }
        //UPDATE-Statement erstellen
        string query = "";
        if (columns.Count > 0)
        {
            query = $"UPDATE [dbo].[{table}] SET ";
            query += string.Join(", ", columns.Select(kvp => $"[{kvp.Key}] = {(kvp.Value == null ? "NULL" : $"'{kvp.Value}'")}"));
            query += $" WHERE ";
            query += string.Join(" AND ", condition);
        }

        var connection = new SqlConnection(CONNECTION_STRING);
        var command = new SqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    internal static void Delete(string table, params string[] conditions)
    {
        string query = $"DELETE FROM [dbo].[{table}]";

        if (conditions.Length > 0)
        {
            query += " WHERE " + string.Join(" AND ", conditions);
        }

        var connection = new SqlConnection(CONNECTION_STRING);
        var command = new SqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}
