using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Client : User
    {
        public required string LevelOfAdvanced { get; set; }
        public required int Goals { get; set; }
        public required float Weight { get; set; }
        public required float Height { get; set; }
        public required float Age { get; set; }
        public List<Plan> Plan { get; set; } = new List<Plan>();
        public List<string> History { get; set; } = new List<string>();

    }
}
