using RestaurantManagement.Models;

namespace RestaurantNetwork.Model
{

    public interface IRestaurant
    {
        string Name { get; set; }
        Address Address { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        TimeOnly OpeningHours { get; set; }
        TimeOnly ClosingHours { get; set; }

        List<MenuItem> Menu { get; set; }
        List<Employee> Employees { get; set; }

        void AddMenus(IEnumerable<MenuItem> menuItems);
        void RemoveMenu(string menuName);
    }
}
