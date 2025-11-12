using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class User
    {
        public required string Name { get; set; }
        public required string Last_Name {  get; set; }
        public required int Role { get; set; }
        public required int Contact_Details { get; set; }
        public User(string name, string lastName, int role, int contactDetails)
        {
            Name = name;
            Last_Name = lastName;
            Role = role;
            Contact_Details = contactDetails;
        }
        public User()
        {
            Name = string.Empty;
            Last_Name = string.Empty;
            Role = 0;
            Contact_Details = 0;
        }

    }
}
