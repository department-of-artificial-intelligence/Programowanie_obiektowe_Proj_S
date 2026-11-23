using Project.Model;

namespace Project.Services.Interfaces;

public interface IRestaurantChainService
{
    void AddCity(RestaurantChain restaurantChain, City city);

    List<Restaurant>? FindRestaurants(
        RestaurantChain restaurantChain,
        string cityName);

    List<City> GetAllCities(RestaurantChain restaurantChain);
}