using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.DataCore
{
    internal class Reader : Connector
    {
        internal static User GetUserDetails(string firstName, string lastName)
        {
            User user = new()
            {
                FirstName = firstName,
                LastName = lastName
            };

            string query = @"
                SELECT ([UserID], [IsAdmin])
                FROM [dbo].[Users]
                WHERE [FirstName] = @firstName AND [LastName] = @lastName
            ";

            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);

            connection.OpenAsync();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.UserID = reader.GetInt32(0);
                user.IsAdmin = reader.GetBoolean(1);
                return user;
            }
            else
            {
                connection.Close();
                return user;
            }
        }
    }
}
