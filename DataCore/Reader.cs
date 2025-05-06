using _4Time.DataCore.Models;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace _4Time.DataCore;

internal class Reader : Connector
{
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

        using (var command = new SqlCommand(sql.ToString(), Connector.connection))
        {
            var reader = command.ExecuteReader();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                if (typeof(T) == typeof(Entry))
                {
                    var startDecrypted = Task.Run(() => Crypto.Decryption(reader.GetString(3), password)).Result;
                    var endDecrypted = Task.Run(() => Crypto.Decryption(reader.GetString(4), password)).Result;
                    var commentDecrypted = Task.Run(() => Crypto.Decryption(reader.GetString(6), password)).Result;
                    //Thread.Sleep(20);
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

        return entries;
    }

}
