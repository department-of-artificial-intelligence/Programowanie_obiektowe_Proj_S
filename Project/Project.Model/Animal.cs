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
        public string Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string MicrochipNuber { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<Appointment> Appointments { get; set; }

        public override string ToString()
        {
            return $"{Name} {Species} {Breed} {Age} {MicrochipNuber}";
        }
    }


}
