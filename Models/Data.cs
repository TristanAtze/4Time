using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.Models
{
    internal class Data
    {
        public int EntryID { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool IsAdmin { get; set; }

        public string CategoryName { get; set; }
        public bool IsWorkTime { get; set; }

        public string? Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
