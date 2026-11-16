using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelter.Model.DataModels;

namespace Shelter.Services
{
    public class AnimalService : IAnimalService
    {
        public Animal ChangeShelter() { return new(); } 
    }
}
