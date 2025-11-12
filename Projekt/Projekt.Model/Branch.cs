using System;
using System.Linq;
using System.Text;

namespace Projekt.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressFamily BrachAddress {  get; set; }
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
