using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; 
using _4Time.DataCore.Models;    

namespace _4Time.DataCore 
{
    internal class Reader : Connector
    {
        private const int MaxDegreeOfParallelism = 16; //Handel wie viele Threads gleichzeitig ausgeführt werden können

        internal static async Task<List<T>> Read<T>(string table, string[]? columns = null, string[]? conditions = null, string? password = null) where T : new()
        {
            var sql = new StringBuilder();
            string columnList = (columns != null && columns.Length > 0)
                ? string.Join(", ", columns)
                : "*";

            sql.Append($"SELECT {columnList} FROM [dbo].[{table}]");

            if (conditions != null && conditions.Length > 0)
            {
                sql.Append(" WHERE ");
                sql.Append(string.Join(" AND ", conditions));
            }

            var processingTasks = new List<Task<T>>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            T[]? resultsArray = null;

            // Semaphore zur Begrenzung der gleichzeitigen Verarbeitungsschritte
            using (var semaphore = new SemaphoreSlim(MaxDegreeOfParallelism, MaxDegreeOfParallelism))
            {
                using (var connection = new SqlConnection(ConnectionString)) 
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql.ToString(), connection))
                    {
                        using (var dbReader = command.ExecuteReader())
                        {
                            while (await dbReader.ReadAsync())
                            {
                                await semaphore.WaitAsync(); 

                                var rowData = ExtractRowDataForProcessing(dbReader, typeof(T), properties);

                                processingTasks.Add(Task.Run(async () =>
                                {
                                    try
                                    {
                                        return await ProcessRowAsync<T>(rowData, properties, password);
                                    }
                                    finally
                                    {
                                        semaphore.Release(); // "Slot" wieder freigeben
                                    }
                                }));
                            }
                        } // dbReader wird hier disposed
                    } // command wird hier disposed
                } // connection wird hier disposed

                resultsArray = await Task.WhenAll(processingTasks);

            } 
            return resultsArray.ToList();
        }

        private static RowDataHolder ExtractRowDataForProcessing(SqlDataReader dbReader, Type typeOfT, PropertyInfo[] properties)
        {
            if (typeOfT == typeof(Entry))
            {
                return new RowDataHolder
                {
                    IsEntryType = true,
                    EntrySpecificData = new EntrySpecificRowData
                    {
                        EntryID = dbReader.GetInt32(0),
                        UserID = dbReader.GetInt32(1),
                        CategoryID = dbReader.GetInt32(2),
                        RawStart = dbReader.GetString(3),
                        RawEnd = dbReader.GetString(4),
                        RawComment = dbReader.GetString(6)
                    }
                };
            }
            else
            {
                var genericData = new Dictionary<string, object?>();
                for (int i = 0; i < dbReader.FieldCount; i++)
                {
                    var columnName = dbReader.GetName(i);
                    var value = dbReader.GetValue(i);
                    genericData[columnName] = value == DBNull.Value ? null : value;
                }
                return new RowDataHolder
                {
                    IsEntryType = false,
                    GenericData = genericData
                };
            }
        }

        private static async Task<T> ProcessRowAsync<T>(RowDataHolder rowData, PropertyInfo[] properties, string? password) where T : new()
        {
            if (rowData.IsEntryType && rowData.EntrySpecificData != null && typeof(T) == typeof(Entry))
            {
                var entryData = rowData.EntrySpecificData;
                var startDecryptedTask = Crypto.DecryptionAsync(entryData.RawStart, password);
                var endDecryptedTask = Crypto.DecryptionAsync(entryData.RawEnd, password);
                var commentDecryptedTask = Crypto.DecryptionAsync(entryData.RawComment, password);

                await Task.WhenAll(startDecryptedTask, endDecryptedTask, commentDecryptedTask);

                return (T)(object)new Entry()
                {
                    EntryID = entryData.EntryID,
                    UserID = entryData.UserID,
                    CategoryID = entryData.CategoryID,
                    Start = DateTime.Parse(await startDecryptedTask),
                    End = DateTime.Parse(await endDecryptedTask),
                    Comment = await commentDecryptedTask
                };
            }
            else if (!rowData.IsEntryType && rowData.GenericData != null)
            {
                var entry = new T();
                foreach (var prop in properties)
                {
                    try
                    {
                        if (rowData.GenericData.TryGetValue(prop.Name, out object? value) && value != null)
                        {
                            prop.SetValue(entry, Convert.ChangeType(value, prop.PropertyType));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Setzen der Eigenschaft {prop.Name} für Typ {typeof(T).Name}: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return entry;
            }
            else
            {
                throw new InvalidOperationException("Ungültige oder unvollständige RowDataHolder-Daten für Typ " + typeof(T).Name);
            }
        }
    }
} 