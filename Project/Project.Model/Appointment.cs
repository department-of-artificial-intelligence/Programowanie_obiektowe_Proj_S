using Project.Abstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Appointment : IIdentifiable, IStatus
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
        public string? Status { get; set; }

        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        public int VeterinarianId { get; set; }
        public Veterinarian? Veterinarian { get; set; }

        public List<Treatment> Treatments { get; set; } = new();

        public Appointment() { }

        public Appointment(int animalId, int veterinarianId, DateTime date, string? notes = null, string? status = null)
        {
            AnimalId = animalId;
            VeterinarianId = veterinarianId;
            Date = date;
            Notes = notes;
            Status = status;
        }
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} – {Status}";
        }

    }


}
