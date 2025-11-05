using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Trainer:User
    {
        public required string Specialization { get; set; }
        public required int Experience { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();

    }
}
