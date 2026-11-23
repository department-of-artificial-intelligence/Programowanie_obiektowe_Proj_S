using Project.Model;

namespace Project.Services.Interfaces;

public interface IRestaurantService
{
    void AddEmployee(Restaurant restaurant, Employee employee);

    bool RemoveEmployee(Restaurant restaurant, string firstName, string lastName);

    void AddDish(Restaurant restaurant, Dish dish);

    List<Dish> GetAvailableDishes(Restaurant restaurant);
}