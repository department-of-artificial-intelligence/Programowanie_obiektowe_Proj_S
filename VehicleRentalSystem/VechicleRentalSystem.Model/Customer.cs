using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public DateTime DriverLicenseExpiration { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public Customer() { }
        public Customer(string firstName, string lastName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public override string ToString()
        {
            return $"[KLIENT - ID: {Id}]\n" +
                $"{FirstName} {LastName} / Kontakt: {EmailAddress}, {PhoneNumber} / Prawo jazdy: {DriverLicenseNumber}, {DriverLicenseExpiration}";
        }
    }
}
