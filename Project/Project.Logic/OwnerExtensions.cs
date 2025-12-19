using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Logic
{
    public static class OwnerExtensions
    {
        
        public static IEnumerable<Owner>
            ByAnimal(this IEnumerable<Owner> owners, int animalId)
        {
            return owners.Where(o => o.Animals.Any(a => a.Id == animalId));
        }

        
        public static IEnumerable<Owner>
            SortByLastName(this IEnumerable<Owner> owners)
        {
            return owners.OrderBy(o => o.LastName);
        }

        public static IEnumerable<Owner>
    ByAnimalSpecies(this IEnumerable<Owner> owners, string species)
        {
            return owners.Where(o => o.Animals.Any(a => a.Species == species));
        }
    }
}
