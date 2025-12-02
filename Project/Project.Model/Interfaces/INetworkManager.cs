using RestaurantNetwork.Model;

namespace RestaurantManagement.Models.Interfaces
{
    public interface INetworkManager
    {
        void AddRestaurant(IRestaurant restaurant);
        IEnumerable<IRestaurant> GetRestaurants();
        IRestaurant? FindByName(string name);
    }
}