using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Project.Model
{
    public class Pizzeria : IShowInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Menu Menu { get; set; }

        [NotMapped]
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
            var order = new Order(0, client);

            foreach (var name in itemNames)
            {
                var originalItem = Menu.FindItem(name);
                if (originalItem != null)
                {
                    var soldItem = new MenuItem(
                        0, // ID 0 tells DB to create a new row
                        originalItem.Name,
                        originalItem.Price ?? 0,
                        originalItem.Description
                    );

                    order.AddItem(soldItem);
                }
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