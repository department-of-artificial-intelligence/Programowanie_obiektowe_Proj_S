using Project.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Animal : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public int Age { get; set; }
        public double? WeightKg { get; set; }

        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public Animal() { }

        public Animal( string name, string? species = null, string? breed = null, int age = 0, double? weightKg = null, int? ownerId = null)
        {
            Name = name;
            Species = species;
            Breed = breed;
            Age = age;
            WeightKg = weightKg;
            OwnerId = ownerId;
        }
        public override string ToString()
        {
            return $"{Name} ({Species})";
        }

    }

}
