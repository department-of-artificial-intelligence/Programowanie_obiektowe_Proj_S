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

        public Veterinarian() : base() { }

        public Veterinarian(int id, string firstName, string lastName, string? licenseNumber = null, string? specialty = null, string? email = null, string? phone = null)
            : base(id, firstName, lastName, email, phone)
        {
            LicenseNumber = licenseNumber;
            Specialty = specialty;
        }



    }
}
