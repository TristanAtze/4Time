namespace _4Time.DataCore.Models
{
    /// <summary>
    /// Represents a container for row-specific data, including entry-specific details and generic key-value pairs.
    /// </summary>
    /// <remarks>This class is used to store and manage data associated with a row. It provides properties for
    /// entry-specific data  and a dictionary for generic data, allowing flexible storage of information. The <see
    /// cref="IsEntryType"/> property  indicates whether the row is associated with entry-specific data.</remarks>
    internal class RowDataHolder
    {
        public bool IsEntryType { get; set; }
        public EntrySpecificRowData? EntrySpecificData { get; set; }
        public Dictionary<string, object?>? GenericData { get; set; }
    }
}
