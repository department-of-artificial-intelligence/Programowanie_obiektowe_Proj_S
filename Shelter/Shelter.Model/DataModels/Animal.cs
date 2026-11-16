using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Model.DataModels
{
    public class Animal
    {
        public int Id {  get; set; } 
        public int ShelterId { get; set; }
        public int SectorId { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public Animal(int id, int shelterId, int sectorId, string species, string breed, DateOnly arrivalDate)
        {
            Id = id;
            ShelterId = shelterId;
            SectorId = sectorId;
            Species = species;
            Breed = breed;
            ArrivalDate = arrivalDate;
        }
    }
}
