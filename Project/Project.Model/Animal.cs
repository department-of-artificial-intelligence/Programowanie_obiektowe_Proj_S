using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? WeightKg { get; set; }
        public string? MicrochipNumber { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<Appointment> Appointments { get; set; } = new();

        //public int GetAge() => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);

        //public override string ToString()
        {
        //    return $"{Name} {Species} {Breed}, Age: {GetAge()}";
        }
    }


}
