using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Model.DataModules
{
    public class Animal
    {
        public int Id {  get; set; } 
        public int ShelterId { get; set; }
        public int SectorId { get; set; }
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateOnly ArrivalDate { get; set; }
    }
}
