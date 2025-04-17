using Microsoft.Data.SqlClient;
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
        internal static void DatabaseSetup()
        {
            string query = File.ReadAllText("Setup.txt");

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            string queryNewUser = @"
                IF(NOT EXISTS (SELECT 1 FROM [dbo].[User] WHERE [FirstName] = @firstName AND [LastName] = @lastName))
                BEGIN
                    INSERT INTO [dbo].[User] ([FirstName], [LastName], [IsAdmin])
                    VALUES (@firstName, @lastName, @IsAdmin)
                END
            ";

            command.Parameters.AddWithValue("@firstName", Connector.FirstName.ToLower());
            command.Parameters.AddWithValue("@lastName", Connector.LastName.ToLower());
            command.Parameters.AddWithValue("@IsAdmin", false);

            var connectionNewUser = new SqlConnection(ConnectionString);
            var commandNewUser = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void InitiateShutdown()
        {
            string query = @"
                UPDATE [dbo].[Shutdown]
                SET [Shutdown] = 1
            "; 
        }
        internal static void DeleteEntry(int entryId)
        {
            string query = @"
                DELETE FROM [dbo].[Entries]
                WHERE [EntryID] = @EntryID
            ";
            using var command = new SqlCommand(query, Connector.connection);
            command.Parameters.AddWithValue("@EntryID", entryId);
            command.ExecuteNonQuery();
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
                string query = @"
                INSERT INTO dbo.Entries (UserID, CategoryID, Start_End, Comment) VALUES (@UserID, @CategoryID, @Start_End, @Comment)
                ";

                
                using var command = new SqlCommand(query, Connector.connection);
                command.Parameters.AddWithValue("@UserID", Reader.GetUserDetails().UserID);
                command.Parameters.AddWithValue("@CategoryID", Reader.GetAllCategorysDetails().Where(x => x.Description == categoryName).Select(x => x.CategoryID).First());
                command.Parameters.AddWithValue("@Start_End", $"{start} - {end}");
                command.Parameters.AddWithValue("@Comment", comment ?? string.Empty);
                command.ExecuteNonQuery();
            }
            else if (entryId.HasValue)
            {
                string query = @"
                UPDATE dbo.Entries
                SET Start_End = @Start_End, Comment = @Comment
                WHERE EntryID = @EntryID
                ";
                using var command = new SqlCommand(query, Connector.connection);
                command.Parameters.AddWithValue("@EntryID", entryId.Value);
                command.Parameters.AddWithValue("@Start_End", $"{start} - {end}");
                command.Parameters.AddWithValue("@Comment", comment ?? string.Empty);
                command.ExecuteNonQuery();
            }
        }
    }
}
