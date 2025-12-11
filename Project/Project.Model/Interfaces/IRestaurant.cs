using RestaurantManagement.Models;

namespace RestaurantNetwork.Model
{
    public interface IRestaurant
    {
        string Name { get; set; }
        string Address { get; set; }
        List<MenuItem> Menu { get; set; }
        List<Employee> Employees { get; set; }

        void AddMenuItem(MenuItem item);
        void AddEmployee(Employee emp);
    }
}
