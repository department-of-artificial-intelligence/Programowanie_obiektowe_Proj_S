
using Mapster;
using Project.DTO.DTOs;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class CityService : ICityService
    {
        public void AddRestaurant(City city, AddRestaurantDto restaurant)
        {
            ArgumentNullException.ThrowIfNull(restaurant);
            var restaurantMapped = restaurant.Adapt<Restaurant>();
            city.Restaurants.Add(restaurantMapped);
        }
        public RemovedRestaurantDto RemoveRestaurant(City city, int id)
        {
            Restaurant? founded = city.Restaurants.Find(x => x.Id == id);
            if (founded == null)
                throw new ArgumentException("No restaruants with this id");
            city.Restaurants.Remove(founded);
            var foundedMapped = founded.Adapt<RemovedRestaurantDto>();
            return foundedMapped;
        }
    }
}
