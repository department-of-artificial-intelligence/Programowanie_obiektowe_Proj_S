using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Tests
{
    public class PizzeriaTest
    {
        [Fact]
        public void Constructor_InitializesPropertiesAndLists()
        {
            string name = "Napoli";
            string address = "Main St 1";

            Pizzeria p = new Pizzeria(name, address);

            Assert.Equal(name, p.Name);
            Assert.Equal(address, p.Address);

            Assert.NotNull(p.Menu);
            Assert.NotNull(p.Storage);
            Assert.NotNull(p.Workers);
            Assert.NotNull(p.Orders);

            Assert.Empty(p.Workers);
            Assert.Empty(p.Orders);
        }

        [Fact]
        public void AddWorker_ValidWorker_AddsToList()
        {
            Pizzeria p = new Pizzeria("Test", "Address");
            Worker w = new KitchenWorker("Mario", "Test", 1000m, "Oven");

            p.AddWorker(w);

            Assert.Single(p.Workers);
            Assert.Equal("Mario", p.Workers[0].FirstName);
        }

        [Fact]
        public void AddWorker_Null_DoesNotAdd()
        {
            Pizzeria p = new Pizzeria("Test", "Address");

            p.AddWorker(null);

            Assert.Empty(p.Workers);
        }

        [Fact]
        public void PlaceOrder_ByItemNames_CreatesOrderWithCorrectItems()
        {
            Pizzeria p = new Pizzeria("Test", "Address");

            p.Menu.AddItem(new MenuItem(1, "Pizza", 20m, "Cheese"));
            p.Menu.AddItem(new MenuItem(2, "Soda", 5m, "Drink"));

            Client c = new Client(1, "John", "Doe", "123");

            List<string> wantedItems = new List<string> { "Pizza", "Soda" };

            Order order = p.PlaceOrder(c, wantedItems);

            Assert.Single(p.Orders);
            Assert.Equal(2, order.Items.Count);
            Assert.Equal("Pizza", order.Items[0].Name);
        }

        [Fact]
        public void PlaceOrder_ByItemNames_IgnoresItemsNotInMenu()
        {
            Pizzeria p = new Pizzeria("Test", "Address");

            Client c = new Client(1, "John", "Doe", "123");
            List<string> wantedItems = new List<string> { "Ghost Pizza" };

            Order order = p.PlaceOrder(c, wantedItems);

            Assert.Empty(order.Items);
            Assert.Single(p.Orders);
        }

        [Fact]
        public void GenerateOrderId_IncrementsIdsAutomatically()
        {
            Pizzeria p = new Pizzeria("Test", "Address");
            Client c = new Client();

            Order o1 = p.PlaceOrder(c, new List<MenuItem>());
            Order o2 = p.PlaceOrder(c, new List<MenuItem>());
            Order o3 = p.PlaceOrder(c, new List<MenuItem>());

            Assert.Equal(1, o1.OrderId);
            Assert.Equal(2, o2.OrderId);
            Assert.Equal(3, o3.OrderId);
        }

        [Fact]
        public void GetInfo_ReturnsFormattedStringWithStats()
        {
            Pizzeria p = new Pizzeria("Best Pizza", "Street 1");
            p.Menu.AddItem(new MenuItem(1, "A", 10, ""));
            p.AddWorker(new HallWorker("A", "B", 10));

            string info = p.GetInfo();

            Assert.Contains("Best Pizza", info);
            Assert.Contains("Menu items: 1", info);
            Assert.Contains("Staff count: 1", info);
        }
    }
}
