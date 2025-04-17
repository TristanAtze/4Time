using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.DataCore.Models
{
    internal class Category
    {
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public bool IsWorkTime { get; set; }
    }
}
