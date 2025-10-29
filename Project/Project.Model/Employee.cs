using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Employee : Person
    {
        public int id { get; set; }
        public string position { get; set; }
        public decimal salary { get; set; }

        public Employee() : base(string.Empty, string.Empty) 
        {
            id = default; // n+1?
            position = string.Empty;
            salary = default;
        }
        public Employee(string firstName, string lastName, int id, string position, decimal salary) : base(firstName, lastName) 
        {
            this.id = id; // check?
            this.position = position;
            this.salary = salary;
        }
    }
}
