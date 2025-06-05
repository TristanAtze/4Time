namespace _4Time.DataCore.Models
{
    /// <summary>
    /// Represents row-specific data for an entry, including identifiers and raw textual information.
    /// </summary>
    /// <remarks>This class encapsulates data related to a specific entry, such as its associated user,
    /// category,  and raw textual details for start time, end time, and comments. It is intended for internal use  and
    /// provides properties for accessing and modifying these values.</remarks>
    internal class EntrySpecificRowData
    {
        public int EntryID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string RawStart { get; set; } = string.Empty;
        public string RawEnd { get; set; } = string.Empty;
        public string RawComment { get; set; } = string.Empty;
    }
}
