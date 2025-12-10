using RestaurantManagement.Models.Enums;
using System.Collections.Generic;
using RestaurantNetwork.Model; 

//ondelete no action/ set null

namespace RestaurantManagement.Models
{
    public class Restaurant
    {
        public int Id { get; set; } // PK
        public required string Name { get; set; }
        public required Address Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required TimeOnly OpeningHours { get; set; }
        public required TimeOnly ClosingHours { get; set; }

        public required List<MenuItem> Menu { get; set; } = new List<MenuItem>();
        public required List<Employee> Employees { get; set; } = new List<Employee>();
        //public required List<Person> Clients { get; set; } = new List<Person>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<Employee> GetEmployeesByType(EmployeeType type)
            => Employees.Where(e => e.EmployeeType == type).ToList();

        public List<(string FirstName, string LastName)> GetEmployeesFirstAndLastNameByType(EmployeeType type)
            => Employees.Where(e => e.EmployeeType == type)
                        .Select(e => (e.FirstName, e.LastName))
                        .ToList();

        public void AddMenus(IEnumerable<MenuItem> menuItems)
            => Menu.AddRange(menuItems);

        public void RemoveMenu(string menuName)
        {
            var menuItem = Menu.FirstOrDefault(m => m.Name.Equals(menuName, StringComparison.OrdinalIgnoreCase));
            if (menuItem != null)
                Menu.Remove(menuItem);
        }
    }
}
