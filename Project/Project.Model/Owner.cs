using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Owner
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string PhoneNumber {  get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        List<Animal> Animals { get; set; }


        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {PhoneNumber} {Email} {Address}";
        }
    }
}
