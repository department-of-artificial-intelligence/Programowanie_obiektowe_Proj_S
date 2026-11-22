
using Project.Model;

namespace Project.Services.Services
{
    public class RestaurantChainService
    {
        public void AddCity(RestaurantChain restaurantChain, City city)
        {
            restaurantChain.Cities.Add(city);
        }

        public List<Restaurant>? FindRestaurants(RestaurantChain restaurantChain, string cityName)
        {
            City? foundedCity = restaurantChain.Cities.Find(city => city.Name == cityName);

            if (foundedCity != null)
            {
                return foundedCity.Restaurants;
            }
            else
            {
                return null;
            }
        }

        public List<City> GetAllCities(RestaurantChain restaurantChain)
        {
            ArgumentNullException.ThrowIfNull(restaurantChain);

            return restaurantChain.Cities;
        }
    }
}
