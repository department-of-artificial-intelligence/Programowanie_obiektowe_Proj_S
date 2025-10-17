using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Treatment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Description} {Cost} ";
        }


    }
}
