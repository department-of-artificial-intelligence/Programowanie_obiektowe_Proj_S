using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Plan
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public List<Training> Training { get; set; } = new List<Training>();
            
    }
}
