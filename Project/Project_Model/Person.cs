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
        public required string Email { get; set; }

        public Person() { 
            Person_id=0; 
            First_Name=string.Empty; 
            Last_Name=string.Empty; 
            Phone_Number=0;
            Email=string.Empty;
        }
        public Person(int person_id, string first_Name, string last_Name, long phone_Number, string email){
            Person_id = person_id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Phone_Number = phone_Number;
            Email = email;
        }
    }
}