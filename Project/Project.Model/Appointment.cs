using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
        public Decimal TotalCost { get; set; }
        public string Status { get; set; } = "Scheduled";

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } 

        public int VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }

        public List<Treatment> Treatments { get; set; } = new();

        public override string ToString()
        {
            return $"{Date:d} - {Description} ({Status}), Cost: {TotalCost:C}";
        }




    }
}
