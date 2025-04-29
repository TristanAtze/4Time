using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.DataCore
{
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
            string query = File.ReadAllText("Setup.txt");

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void UserSetup()
        {
            //TODO Datenbank ändern!!!
            string query = @"
                IF(NOT EXISTS (SELECT 1 FROM [_LK_TestDB].[dbo].[User] WHERE [FirstName] = @firstName AND [LastName] = @lastName))
                BEGIN
                    INSERT INTO [_LK_TestDB].[dbo].[User] ([FirstName], [LastName], [IsAdmin])
                    VALUES (@firstName, @lastName, @IsAdmin)
                END
            ";

            var connection = new SqlConnection(ConnectionString);
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
            //TODO Datenbank ändern!!!
            string schemaQuery = $"SELECT COLUMN_NAME FROM [_LK_TestDB].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
            var schemaConnection = new SqlConnection(ConnectionString);
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
            string query = $"INSERT INTO [_LK_TestDB].[dbo].[{table}] ";

            if(columns.Count > 0)
            {
                if (columns.ContainsKey("Start_End") && obj.GetType() == typeof(Entry))
                {
                    Entry entry = (Entry)obj;
                    columns["Start_End"] = Crypto.Encrypt(entry.Start.ToString()) + " - " + Crypto.Encrypt(entry.End.ToString());
                }

                query += "(" + string.Join(", ", columns.Keys) + ") VALUES (";
                query += string.Join(", ", columns.Values.Select(v => v == null ? "NULL" : $"'{v}'")) + ")";
            }
            else
            {
                query += "DEFAULT VALUES";
            }

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void Update(string table, object obj, string[] condition)
        {
            Dictionary<string, object?> columns = [];
            //Alle Spalten ermitteln
            //TODO Datenbank ändern!!!
            string schemaQuery = $"SELECT COLUMN_NAME FROM [_LK_TestDB].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
            var schemaConnection = new SqlConnection(ConnectionString);

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
                    columns[prop.Name] = prop.GetValue(obj);
                }
            }
            //UPDATE-Statement erstellen
            string query = "";
            if (columns.Count > 0)
            {
                if(columns.ContainsKey("Start_End") && obj.GetType() == typeof(Entry))
                {
                    Entry entry = (Entry) obj;
                    columns["Start_End"] = Crypto.Encrypt(entry.Start.ToString()) + " - " + Crypto.Encrypt(entry.End.ToString());
                }

                query = $"UPDATE [_LK_TestDB].[dbo].[{table}] SET ";
                query += string.Join(", ", columns.Select(kvp => $"{kvp.Key} = {(kvp.Value == null ? "NULL" : $"'{kvp.Value}'")}"));
                query += $" WHERE ";
                query += string.Join(" AND ", condition);
            }

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void Delete(string table, params string[] conditions)
        {
            string query = $"DELETE FROM [_LK_TestDB].[dbo].[{table}]";

            if (conditions.Length > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Legt einen neuen Eintrag an oder updated einen vorhandenen (wenn entryId != null).
        /// Gibt die ID des Eintrags zurück (neu angelegt oder geupdatet).
        /// </summary>
        internal static void CreateOrUpdateEntry(
            int? entryId,
            DateTime start,
            DateTime end,
            string categoryName,
            string? comment)
        {
            if (!entryId.HasValue)
            {
                //TODO Datenbank ändern!!!
                string query = @"
                INSERT INTO [_LK_TestDB].[dbo].[Entries] (UserID, CategoryID, Start_End, Comment) VALUES (@UserID, @CategoryID, @Start_End, @Comment)
                ";


                using var command = new SqlCommand(query, Connector.connection);
                command.Parameters.AddWithValue("@UserID", Reader.Read<User>("User",
                [
                    "[UserID]"
                ],
                [
                    $"[FirstName] = '{FirstName}'",
                    $"[LastName] = '{LastName}'"
                ]).First().UserID);
                command.Parameters.AddWithValue("@CategoryID", Reader.Read<Category>("Categories",
                [
                    "[CategoryID]"
                ],
                [
                    $"[Description] = '{categoryName}'"
                ]).First().CategoryID);
                command.Parameters.AddWithValue("@Start_End", $"{Crypto.Encrypt(start.ToString())} - {Crypto.Encrypt(end.ToString())}");
                command.Parameters.AddWithValue("@Comment", comment ?? string.Empty);
                command.ExecuteNonQuery();
            }
            else if (entryId.HasValue)
            {
                //TODO Datenbank ändern!!!
                string query = @"
                UPDATE [_LK_TestDB].[dbo].[Entries]
                SET Start_End = @Start_End, Comment = @Comment, CategoryID = @CategoryID
                WHERE EntryID = @EntryID
                ";

                using var command = new SqlCommand(query, Connector.connection);
                command.Parameters.AddWithValue("@EntryID", entryId.Value);
                command.Parameters.AddWithValue("@CategoryID", Reader.Read<Category>("Categories",
                [
                    "[CategoryID]"
                ],
                [
                    $"[Description] = '{categoryName}'"
                ]).First());
                command.Parameters.AddWithValue("@Start_End", $"{start} - {end}");
                command.Parameters.AddWithValue("@Comment", comment ?? string.Empty);
                command.ExecuteNonQuery();
            }
        }
    }
}
