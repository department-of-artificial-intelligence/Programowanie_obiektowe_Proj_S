using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Veterinarian
    {
 
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic {  get; set; }

        public List<Appointment> Appointments { get; set; } = new();

        //public override string ToString()
     

        
    }
}
