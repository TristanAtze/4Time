using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Time.DataCore.Models
{
    internal class Automatics
    {
        public int AutomaticID { get; set; }
        public string Name { get; set; }
        public bool IsChangeable { get; set; }
        public bool IsActive { get; set; }
    }
}
