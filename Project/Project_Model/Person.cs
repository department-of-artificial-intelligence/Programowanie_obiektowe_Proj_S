using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Person
    {
        public required int Person_id { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required long Phone_Number { get; set; }
        public string Email { get; set; }

    }
}