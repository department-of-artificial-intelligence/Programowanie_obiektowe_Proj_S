using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace jezwik_01.Model
{
    public class Person
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }        
        public required int Age { get; set; }




        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age}";
        }
    }
}
