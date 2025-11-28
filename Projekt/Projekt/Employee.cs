using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projekt
{   
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; } 
        public string Phone { get; set; }

        public Employee(int id, string fullName, string position, string phone)
        {
            Id = id;
            FullName = fullName;
            Position = position;
            Phone = phone;
        }

        public Employee()
        {
            Id = 0;
            FullName = "";
            Position = "";
            Phone = "";
        }

        public override string ToString()
        {
            return $"{Id} {FullName} {Position} {Phone}";
        }
    }

}
