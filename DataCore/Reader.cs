using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace _4Time.DataCore;

internal class Reader : Connector
{
    internal static List<T> Read<T>(string table, string[]? columns = null, params string[] conditions) where T : new()
    {
        var entries = new List<T>();
        var sql = new StringBuilder();

        // Spaltenauswahl
        string columnList = (columns != null && columns.Length > 0)
            ? string.Join(", ", columns)
            : "*";

        sql.Append($"SELECT {columnList} FROM [_LK_TestDB].[dbo].[{table}]");

        // Bedingungen
        if (conditions != null && conditions.Length > 0)
        {
            sql.Append(" WHERE ");
            sql.Append(string.Join(" AND ", conditions));
        }

        using (var command = new SqlCommand(sql.ToString(), Connector.connection))
        {
            var reader = command.ExecuteReader();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                if (typeof(T) == typeof(Entry))
                {
                    //TEST-CODE
                    string startEnd = reader.GetString(3);
                    var start = DateTime.Parse(Crypto.Decrypt(startEnd.Split("-")[0]));
                    var ende = DateTime.Parse(Crypto.Decrypt(startEnd.Split("-")[1]));

                    entries.Add((T)(object)new Entry()
                    {
                        EntryID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        CategoryID = reader.GetInt32(2),
                        Start = DateTime.Parse(Crypto.Decrypt(startEnd.Split("-")[0])),
                        End = DateTime.Parse(Crypto.Decrypt(startEnd.Split("-")[1])),
                        Comment = reader.GetString(5)
                    });
                }
                else
                {
                    var entry = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            var value = reader[prop.Name];
                            if (value != DBNull.Value)
                            {
                                prop.SetValue(entry, Convert.ChangeType(value, prop.PropertyType));
                            }
                        }
                        catch (Exception) { }
                    }

                    entries.Add(entry);
                }
            }
            reader.Close();
        }

        return entries;
    }

}
