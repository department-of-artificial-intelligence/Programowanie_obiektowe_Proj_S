using Project.Model;

namespace Project.Services.Interfaces;

public interface ICityService
{
    void AddRestaurant(City city, Restaurant restaurant);
    Restaurant RemoveRestaurant(City city, int id);
}