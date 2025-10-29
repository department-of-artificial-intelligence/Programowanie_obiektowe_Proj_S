using Project.Model;

namespace RestaurantNetwork.Model
{
    public class Restaurant : IRestaurant
    {
        public string Name { get; }
        public string Address { get; }
        public Manager Manager { get; }
        public List<MenuItem> Menu { get; }
        public List<Employee> Employees { get; }
        public List<Order> Orders { get; }

        public Restaurant(string name, string address, Manager manager)
        {
            Name = name;
            Address = address;
            Manager = manager;
            Menu = new List<MenuItem>();
            Employees = new List<Employee>();
            Orders = new List<Order>();
        }

        public void AddMenuItem(MenuItem item) => Menu.Add(item);
        public void AddEmployee(Employee emp) => Employees.Add(emp);
        public void AddOrder(Order order) => Orders.Add(order);

        public override string ToString() => $"{Name} ({Address})";
    }
}
