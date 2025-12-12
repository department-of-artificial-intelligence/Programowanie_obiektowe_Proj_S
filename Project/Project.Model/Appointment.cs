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
        public int AnimalId { get; set; }
        public int VeterinarianId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }

        public Appointment() { }

        public Appointment(int id, int animalId, int veterinarianId, DateTime date, string? notes = null, string? status = null)
        {
            Id = id;
            AnimalId = animalId;
            VeterinarianId = veterinarianId;
            Date = date;
            Notes = notes;
            Status = status;
        }
    }


}
