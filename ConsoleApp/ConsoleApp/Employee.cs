using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNetworkManagement
{
    public class Employee : Person
    {
        public string Position { get; set;  }
        public decimal Salary { get; set;   }

        
        public bool IsEmployed { get; set; }

        public Employee(int id, string firstName, string lastName, string position, decimal salary, bool isEmployed) : base(id, firstName,lastName)
        {
            Position = position;
            Salary = salary;
            IsEmployed = isEmployed;

        }


    }
}
