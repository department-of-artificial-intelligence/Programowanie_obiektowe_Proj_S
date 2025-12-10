using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pizzeria : IShowInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Menu Menu { get; set; }
        public StorageRoom Storage { get; set; }
        public IList<Worker> Workers { get; set; }
        public IList<Order> Orders { get; set; }

        public Pizzeria() : this(string.Empty, string.Empty) { }
        public Pizzeria(string name, string address)
        {
            Name = name;
            Menu = new Menu();
            Storage = new StorageRoom();
            Workers = new List<Worker>();
            Orders = new List<Order>();
            Address = address;
        }

        public void AddWorker(Worker worker)
        {
            if (worker != null)
            {
                Workers.Add(worker);
            }
        }

        public void AddOrder(Order order)
        {
            if (order != null)
                Orders.Add(order);
        }

        public Order PlaceOrder(Client client, IList<string> itemNames)
        {
            var order = new Order(GenerateOrderId(), client);

            foreach (var name in itemNames)
            {
                var item = Menu.FindItem(name);
                if (item != null)
                    order.AddItem(item);
            }

            Orders.Add(order);
            return order;
        }

        public Order PlaceOrder(Client client, IList<MenuItem> items)
        {
            var order = new Order(GenerateOrderId(), client);

            foreach (var item in items)
                order.AddItem(item);

            Orders.Add(order);
            return order;
        }

        private int GenerateOrderId()
        {
            return Orders.Count == 0 ? 1 : Orders.Max(o => o.OrderId) + 1;
        }

        public string GetInfo()
        {
            var info = $"=== PIZZERIA: {Name} ===\n";
            info += $"Address: {Address}\n";
            info += $"Menu items: {Menu.AvailableItems.Count}\n";
            info += $"Staff count: {Workers.Count}\n";
            info += $"Total orders: {Orders.Count}\n";
            info += "=========================";

            return info;
        }
    }
}