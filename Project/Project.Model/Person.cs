using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Ages { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age}";
        }

    }
}
