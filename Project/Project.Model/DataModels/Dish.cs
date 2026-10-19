using Project.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class Dish
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PortionSize { get; set; }
        public List<Ingridient> Ingridients { get; set; }
        public bool IsAvailable { get; set; }

        public Dish(int id, string name, string description, decimal price, string portionSize, List<Ingridient> ingridients, bool isAvailable)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PortionSize = portionSize;
            Ingridients = ingridients;
            IsAvailable = isAvailable;
        }
    }
}
