using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Rental> RentalHistory { get; set; } = new List<Rental>();

        public Customer() { }

        public override string ToString()
        {
            return $"[{Id}] {FirstName} {LastName} ";
        }
    }
}
