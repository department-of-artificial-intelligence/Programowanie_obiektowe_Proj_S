using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IDishService
{
    void ChangePrice(Dish dish, decimal newPrice);

    void AddIngredient(
        Dish dish,
        DishIngridient ingridient);

    decimal CalculateCost(Dish dish);
}