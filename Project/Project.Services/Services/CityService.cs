
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class CityService : ICityService
    {
        public void AddRestaurant(City city, Restaurant restaurant)
        {
            ArgumentNullException.ThrowIfNull(restaurant);

            city.Restaurants.Add(restaurant);
        }
        public Restaurant RemoveRestaurant(City city, int id)
        {
            Restaurant? founded = city.Restaurants.Find(x => x.Id == id);
            if (founded == null)
                throw new ArgumentException("No restaruants with this id");
            city.Restaurants.Remove(founded);
            return founded;
        }
    }
}
