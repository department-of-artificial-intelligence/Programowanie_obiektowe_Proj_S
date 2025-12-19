using System.Collections.Generic;

namespace Project.Model
{
    public class Customer : Person
    {
        public string Address { get; set; }

        
        public virtual ICollection<Bicycle> RentedBicycles { get; set; } = new List<Bicycle>();

        public Customer() { }
        public Customer(string firstName, string lastName, string address)
            : base(firstName, lastName)
        {
            Address = address;
        }

        public override string ToString()
        {
            return $"Customer: {FirstName} {LastName}, Address: {Address} (Rented: {RentedBicycles.Count})";
        }
    }
}