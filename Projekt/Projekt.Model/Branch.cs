using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}