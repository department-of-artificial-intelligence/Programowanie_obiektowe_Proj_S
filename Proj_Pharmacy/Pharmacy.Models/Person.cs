using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Interfaces;

namespace Pharmacy.Models
{
    public class Person : ID
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person()
        {
            Id = 0;
            FirstName = "Nieznany";
            LastName = "Nieznany";
        }

        public Person(int id, string firstName= "Nieznany", string lastName= "Nieznany")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
