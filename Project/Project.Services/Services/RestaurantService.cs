
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class RestaurantService : IRestaurantService
    {
        public void AddEmployee(Restaurant restaurant, Employee employee)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));
            restaurant.Employees.Add(employee);
        }

        public bool RemoveEmployee(Restaurant restaurant, string firstName, string lastName)
        {
            Employee? employee = restaurant.Employees.Find(x => x.FirstName == firstName && x.LastName == lastName);
            if (employee != null)
            {
                restaurant.Employees.Remove(employee);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddDish(Restaurant restaurant, Dish dish)
        {
            ArgumentNullException.ThrowIfNull(dish, nameof(dish));
            restaurant.Menu.Add(dish);
        }

        // TODO!!!
        // CreateOrder(customer, items)
        public List<Dish> GetAvailableDishes(Restaurant restaurant)
        {
            return restaurant.Menu;
        }
    }
}