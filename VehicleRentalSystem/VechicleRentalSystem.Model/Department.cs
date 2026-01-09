using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department() { }
        public Department(string name, string city, string streetAddress, string emailAddress, string phoneNumber)
        {
            Name = name;
            City = city;
            StreetAddress = streetAddress;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"[{Name} - ID: {Id}]\n" +
                $"Adres: {City}, {StreetAddress} / Kontakt: {EmailAddress}, +48{PhoneNumber}";
        }
    }
}
