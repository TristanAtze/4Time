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
        public string Start_End { get; set; }
        public string? Comment { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
