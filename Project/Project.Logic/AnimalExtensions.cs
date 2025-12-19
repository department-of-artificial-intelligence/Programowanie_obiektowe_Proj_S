using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;


namespace Project.Logic
{
    public static class AnimalExtensions
    {
    
        public static IEnumerable<Animal>
            ByOwner(this IEnumerable<Animal> animals, int ownerId)
        {
            return animals.Where(a => a.OwnerId == ownerId);
        }

       
        public static IEnumerable<Animal>
            BySpecies(this IEnumerable<Animal> animals, string species)
        {
            return animals.Where(a => a.Species == species);
        }

        
        public static IEnumerable<Animal>
            SortByName(this IEnumerable<Animal> animals)
        {
            return animals.OrderBy(a => a.Name);
        }
    }
}
