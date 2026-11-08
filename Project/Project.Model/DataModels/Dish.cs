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
        public List<DishIngridient> Ingridients { get; set; }
        public bool IsAvailable { get; set; }

        public Dish(int id, string name, string description, decimal price, string portionSize, List<DishIngridient> ingridients, bool isAvailable)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PortionSize = portionSize;
            Ingridients = ingridients;
            IsAvailable = isAvailable;
        }

        public void ChangePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "must be > 0");
            Price = price;
        }

        public void AddIngredient(DishIngridient ingridient)
        {
            ArgumentNullException.ThrowIfNull(ingridient, nameof(ingridient));
            Ingridients.Add(ingridient);
        }
        public decimal CalculateCost()
        {
            return Ingridients.Sum(i => i.Ingridient.UnitPrice * i.Quantity);
        }

        public override string ToString()
        {
            
            var ingredientsList = string.Join("\n  - ", Ingridients.Select(i => $"{i.Ingridient.Name} ({i.Quantity} {i.Ingridient.Unit})"));
            decimal cost = CalculateCost();

            return
$@"----------------------------------------
Danie: {Name} (ID: {Id})
Opis: {Description}
Cena sprzedaży: {Price}
Koszt własny: {cost}
Wielkość porcji: {PortionSize}
Dostępność: {(IsAvailable ? "Dostępne" : "Niedostępne")}
Składniki:
  - {ingredientsList}";
        }

    }
}
