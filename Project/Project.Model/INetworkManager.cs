namespace RestaurantNetwork.Model
{
    public interface INetworkManager
    {
        void AddRestaurant(IRestaurant restaurant);
        IEnumerable<IRestaurant> GetRestaurants();
        IRestaurant? FindByName(string name);
    }
}