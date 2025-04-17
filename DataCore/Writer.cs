using Microsoft.Data.SqlClient;
using System;
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

            connection.OpenAsync();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void InitiateShutdown()
        {
            string query = @"
                UPDATE [dbo].[Shutdown]
                SET [Shutdown] = 1
            ";

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.OpenAsync();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void CreateEntry(DateTime start, DateTime end, string categoryName, string? comment)
        {


            string query = @"
                INSERT INTO dbo.[Entries]
                (
                    [UserID],
                    [CategoryID],
                    [Start_End],
                    [Comment],
                    [TimeStamp],
                ) 
            "
        }
    }
}
