using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public Decimal TotalCost { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } 
        public string VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }

        public List<Treatment> Treatments { get; set; }

        public override string ToString()
        {
            return $"{Id} {Data} {Description} {TotalCost}";
        }




    }
}
