using RestaurantManagement.Models.Enums;
using RestaurantNetwork.Model;
using System.Security.Cryptography.X509Certificates;

namespace RestaurantManagement.Models
{
    public class Restaurant
    {
        public required string Name { get; set; }
        public required Address Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required TimeOnly OpeningHours { get; set; }
        public required TimeOnly ClosingHours { get; set; }
        public required List<MenuItem> Menu { get; set; }
        public required List<Employee> Employees { get; set; }
        public required List<Person> Clients { get; set; }
        public  List<Reservation> Reservations { get; set; } = new List<Reservation>();


        //GetEmployeesByType
        public List<Employee> GetEmployeesByType(EmployeeType type)
        {
         return Employees.Where(e => e.EmployeeType == type).ToList();
           }

        //Zwroc imiona i nazwiska danego typu pracownika ----- krotki:
        public List<(string FirstName, string LastName)> GetEmployeesFirstAndLastNameByType(EmployeeType type)
        {
            return Employees
                .Where(e => e.EmployeeType == type)
                .Select(e => (e.FirstName, e.LastName))
                .ToList();
        }


        //Doda nowy przepis do istniejacej listy
        /* public void AddMenu(MenuItem menuItem)
         {
              Menu.Add(menuItem);
         }
        */

        public void AddMenus(IEnumerable<MenuItem> menuItems)
        {
            Menu.AddRange(menuItems);
        }


        // 2) usuwa po nazwie
        public void RemoveMenu(string menuName) 
        {
            var menuItem = Menu.FirstOrDefault(m => m.Name.ToLower() == menuName.ToLower());
            if (menuItem != null)
            {
                Menu.Remove(menuItem);
            }
        }
    }
}
