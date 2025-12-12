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
        public static IEnumerable<Animal> OlderThan(this IEnumerable<Animal> animals, int age)
            => animals?.Where(a => a != null && a.Age > age) ?? Enumerable.Empty<Animal>();

        public static IEnumerable<Animal> BySpecies(this IEnumerable<Animal> animals, string species)
            => animals?.Where(a => a != null && string.Equals(a.Species, species, System.StringComparison.OrdinalIgnoreCase)) ?? Enumerable.Empty<Animal>();

        public static IEnumerable<Animal> SortByName(this IEnumerable<Animal> animals)
            => animals?.OrderBy(a => a?.Name) ?? Enumerable.Empty<Animal>();
    }
}
