using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.DataCore.Models
{
    internal class Entry
    {
        public int EntryID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Duration => (End - Start).ToString(@"hh\:mm");
    }
}
