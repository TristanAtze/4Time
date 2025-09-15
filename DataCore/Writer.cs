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

    /// <summary>
    /// Erstellt die Datenbank und die benötigten Tabellen, wenn sie nicht existieren.
    /// </summary>
    internal static async Task DatabaseSetupAsync()
    {
        string query = File.ReadAllText("res/Setup.txt");

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();
    }

    internal static async Task UserSetupAsync()
    {
        string query = @"
                IF(NOT EXISTS (SELECT 1 FROM [dbo].[User] WHERE [FirstName] = @firstName AND [LastName] = @lastName))
                BEGIN
                    INSERT INTO [dbo].[User] ([FirstName], [LastName], [IsAdmin])
                    VALUES (@firstName, @lastName, @IsAdmin)
                END
            ";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@firstName", Connector.FirstName.ToLower());
        command.Parameters.AddWithValue("@lastName", Connector.LastName.ToLower());
        command.Parameters.AddWithValue("@IsAdmin", false);

        connection.Open();
        command.ExecuteNonQuery();
    }

    internal static void Insert(string table, object obj)
    {
        Dictionary<string, object?> columns = [];

        //Alle Spalten ermitteln
        string schemaQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table";
        using (var schemaConnection = new SqlConnection(ConnectionString))
        using (var schemaCommand = new SqlCommand(schemaQuery, schemaConnection))
        {
            schemaCommand.Parameters.AddWithValue("@table", table);
            schemaConnection.Open();
            using var schemaReader = schemaCommand.ExecuteReader();
            while (schemaReader.Read())
            {
                if (!UNSETTABLE_COLUMNS[obj.GetType()].Contains(schemaReader.GetString(0)))
                    columns.Add(schemaReader.GetString(0), null);
            }
        }

        //Spalten mit Werten füllen
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (columns.ContainsKey(prop.Name))
            {
                columns[prop.Name] = prop.GetValue(obj);
            }
        }

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand();
        command.Connection = connection;
        string query = $"INSERT INTO [dbo].[{table}] ";

        if (columns.Count > 0)
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

            var columnNames = columns.Keys.Select(k => $"[{k}]");
            var paramNames = columns.Keys.Select((_, i) => $"@p{i}");
            query += $"({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", paramNames)})";

            int index = 0;
            foreach (var value in columns.Values)
            {
                command.Parameters.AddWithValue($"@p{index}", value ?? DBNull.Value);
                index++;
            }
        }
        else
        {
            query += "DEFAULT VALUES";
        }

        command.CommandText = query;
        connection.Open();
        command.ExecuteNonQuery();
    }

    internal static void Update(string table, object obj, Dictionary<string, object?> conditions)
    {
        Dictionary<string, object?> columns = [];

        //Alle Spalten ermitteln
        string schemaQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table";
        using (var schemaConnection = new SqlConnection(ConnectionString))
        using (var schemaCommand = new SqlCommand(schemaQuery, schemaConnection))
        {
            schemaCommand.Parameters.AddWithValue("@table", table);
            schemaConnection.Open();
            using var schemaReader = schemaCommand.ExecuteReader();
            while (schemaReader.Read())
            {
                if (!UNSETTABLE_COLUMNS[obj.GetType()].Contains(schemaReader.GetString(0)))
                {
                    columns.Add(schemaReader.GetString(0), null);
                }
            }
        }

        //Spalten mit Werten füllen
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (columns.ContainsKey(prop.Name))
            {
                if (prop.Name == "Start" || prop.Name == "End" || prop.Name == "Comment")
                {
                    columns[prop.Name] = Crypto.Encryption(prop.GetValue(obj)?.ToString() ?? "");
                }
                else
                    columns[prop.Name] = prop.GetValue(obj);
            }
        }

        if (columns.Count == 0)
            return;

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand();
        command.Connection = connection;

        var setClauses = new List<string>();
        int index = 0;
        foreach (var kvp in columns)
        {
            setClauses.Add($"[{kvp.Key}] = @p{index}");
            command.Parameters.AddWithValue($"@p{index}", kvp.Value ?? DBNull.Value);
            index++;
        }

        var conditionClauses = new List<string>();
        int condIndex = 0;
        foreach (var kvp in conditions)
        {
            conditionClauses.Add($"[{kvp.Key}] = @c{condIndex}");
            command.Parameters.AddWithValue($"@c{condIndex}", kvp.Value ?? DBNull.Value);
            condIndex++;
        }

        string query = $"UPDATE [dbo].[{table}] SET {string.Join(", ", setClauses)}";
        if (conditionClauses.Count > 0)
        {
            query += " WHERE " + string.Join(" AND ", conditionClauses);
        }

        command.CommandText = query;
        connection.Open();
        command.ExecuteNonQuery();
    }

    internal static void Delete(string table, Dictionary<string, object?>? conditions = null)
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand();
        command.Connection = connection;

        string query = $"DELETE FROM [dbo].[{table}]";

        var conditionClauses = new List<string>();
        if (conditions != null)
        {
            int index = 0;
            foreach (var kvp in conditions)
            {
                conditionClauses.Add($"[{kvp.Key}] = @c{index}");
                command.Parameters.AddWithValue($"@c{index}", kvp.Value ?? DBNull.Value);
                index++;
            }
        }

        if (conditionClauses.Count > 0)
        {
            query += " WHERE " + string.Join(" AND ", conditionClauses);
        }

        command.CommandText = query;
        connection.Open();
        command.ExecuteNonQuery();
    }
}
