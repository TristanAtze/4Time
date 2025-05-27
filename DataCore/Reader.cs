using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace _4Time.DataCore;

internal class Reader : Connector
{
    /// <summary>
    /// Dynamischer Datenbank Reader.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <param name="columns"></param>
    /// <param name="conditions"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    internal static List<T> Read<T>(string table, string[]? columns = null, string[]? conditions = null, string? password = null) where T : new()
    {
        var entries = new List<T>();
        var sql = new StringBuilder();
        // Spaltenauswahl
        string columnList = (columns != null && columns.Length > 0)
            ? string.Join(", ", columns)
            : "*";

        sql.Append($"SELECT {columnList} FROM [dbo].[{table}]");

        // Bedingungen
        if (conditions != null && conditions.Length > 0)
        {
            sql.Append(" WHERE ");
            sql.Append(string.Join(" AND ", conditions));
        }

        var connection = new SqlConnection(ConnectionString);
        connection.Open();
        using (var command = new SqlCommand(sql.ToString(), connection))
        {
            var reader = command.ExecuteReader();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                if (typeof(T) == typeof(Entry))
                {
                    var startDecrypted = Crypto.Decryption(reader.GetString(3), password).Result;
                    var endDecrypted = Crypto.Decryption(reader.GetString(4), password).Result;
                    var commentDecrypted = Crypto.Decryption(reader.GetString(6), password).Result;

                    entries.Add((T)(object)new Entry()
                    {
                        EntryID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        CategoryID = reader.GetInt32(2),
                        Start = DateTime.Parse(startDecrypted),
                        End = DateTime.Parse(endDecrypted),
                        Comment = commentDecrypted
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
        connection.Close();

        return entries;
    }

}
