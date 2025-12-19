using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Model
{
    public class Veterinarian : Person
    {
        public string? LicenseNumber { get; set; }
        public string? Specialty { get; set; }

        public int ClinicId { get; set; }
        public Clinic? Clinic { get; set; }

        public Veterinarian() : base() { }

        public Veterinarian(string firstName, string lastName, string? licenseNumber = null, string? specialty = null, string? email = null, string? phone = null)
            : base(firstName, lastName, email, phone)
        {
            LicenseNumber = licenseNumber;
            Specialty = specialty;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} – {Specialty}";
        }

    }
}
