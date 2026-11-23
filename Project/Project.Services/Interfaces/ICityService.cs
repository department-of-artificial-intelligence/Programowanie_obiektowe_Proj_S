using Project.DTO.DTOs;
using Project.Model;

namespace Project.Services.Interfaces;

public interface ICityService
{
    void AddRestaurant(City city, AddRestaurantDto restaurant);
    RemovedRestaurantDto RemoveRestaurant(City city, int id);
}