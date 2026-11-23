using Project.Model;
using Project.Model.DataModels;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Services
{
    public class DishService : IDishService
    {
        public void ChangePrice(Dish dish, decimal price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "must be > 0");
            dish.Price = price;
        }

        public void AddIngredient(Dish dish, DishIngridient ingridient)
        {
            ArgumentNullException.ThrowIfNull(ingridient, nameof(ingridient));
            dish.Ingridients.Add(ingridient);
        }
        public decimal CalculateCost(Dish dish)
        {
            return dish.Ingridients.Sum(i => i.Ingridient.UnitPrice * i.Quantity);
        }
    }
}
