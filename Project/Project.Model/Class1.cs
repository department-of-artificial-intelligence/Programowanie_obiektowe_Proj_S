using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Person
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Title { get; set; }


        public Person() { 
           FirstName = string.Empty;
           LastName = string.Empty;
           Title = string.Empty;

        }
    }
}
