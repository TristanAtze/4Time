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
        internal static User GetUserDetails()
        {
            User user = new()
            {
                FirstName = Connector.FirstName,
                LastName = Connector.LastName
            };

            string query = @"
                SELECT [UserID], [IsAdmin]
                FROM [dbo].[User]
                WHERE [FirstName] = @firstName AND [LastName] = @lastName
            ";

            var command = new SqlCommand(query, Connector.connection);

            command.Parameters.AddWithValue("@firstName", FirstName.ToLower());
            command.Parameters.AddWithValue("@lastName", LastName.ToLower());

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.UserID = reader.GetInt32(0);
                user.IsAdmin = reader.GetBoolean(1);
            }
            reader.Close();
            return user;
        }


        internal static List<Entry> GetAllEntrysOfUser()
        {
            List<Entry> Entrys = [];

            string query = @"
                SELECT *
                FROM [dbo].[Entries]
                WHERE [UserID] = @UserID
            ";

            User user = GetUserDetails();

            var command = new SqlCommand(query, Connector.connection);

            command.Parameters.AddWithValue("@UserID", user.UserID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Entrys.Add(
                    new Entry
                    {
                        EntryID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        CategoryID = reader.GetInt32(2),
                        Start = DateTime.Parse(reader.GetString(3).Split("-")[0]),
                        End = DateTime.Parse(reader.GetString(3).Split("-")[1]),
                        Comment = reader.GetString(5),
                    }
                );
            }
            reader.Close();

            return Entrys;
        }

        internal static List<Category> GetAllCategorysDetails()
        {
            List<Category> categories = [];

            string query = @"
                SELECT *
                FROM [dbo].[Categories]
            ";

            var command = new SqlCommand(query, Connector.connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(
                    new Category
                    {
                        CategoryID = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        IsWorkTime = reader.GetBoolean(2)
                    }
                );
            }
            reader.Close();
            return categories;
        }
    }
}
