using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Actor : Person
    {
        public string role { get; set; }

        public Actor() : base(string.Empty, string.Empty) 
        {
            role = string.Empty;
        }
        public Actor(string firstName, string lastName, string role) : base(firstName, lastName)
        {
            this.role = role;
        }
    }
}
