namespace Project.Tests
{
    public class OrderTest
    {
        [Fact]
        public void Constructor_SetsPropertiesAndTimestamp()
        {
            int id = 101;
            Client client = new Client(1, "John", "Doe", "555-0000");
            List<MenuItem> items = new List<MenuItem>();

            Order order = new Order(id, client, items);

            Assert.Equal(id, order.OrderId);
            Assert.Same(client, order.Client);
            Assert.Same(items, order.Items);

            Assert.True((DateTime.Now - order.OrderTime).TotalSeconds < 1);
        }

        [Fact]
        public void AddItem_ValidItem_AddsToList()
        {
            Order order = new Order(1, new Client());
            MenuItem item = new MenuItem(1, "Pizza", 20m, "Tasty");

            order.AddItem(item);

            Assert.Single(order.Items);
            Assert.Equal("Pizza", order.Items[0].Name);
        }

        [Fact]
        public void AddItem_Null_DoesNotAdd()
        {
            Order order = new Order(1, new Client());

            order.AddItem(null);

            Assert.Empty(order.Items);
        }

        [Fact]
        public void RemoveItem_ExistingItem_ReturnsTrueAndRemoves()
        {
            Order order = new Order(1, new Client());
            order.AddItem(new MenuItem(1, "Soda", 5m, "Drink"));

            bool result = order.RemoveItem("Soda");

            Assert.True(result);
            Assert.Empty(order.Items);
        }

        [Fact]
        public void RemoveItem_NonExistingItem_ReturnsFalse()
        {
            Order order = new Order(1, new Client());
            order.AddItem(new MenuItem(1, "Soda", 5m, "Drink"));

            bool result = order.RemoveItem("Burger");

            Assert.False(result);
            Assert.Single(order.Items);
        }

        [Fact]
        public void RemoveItem_Duplicates_RemovesOnlyFirstOccurrence()
        {
            Order order = new Order(1, new Client());
            order.AddItem(new MenuItem(1, "Pizza", 20m, ""));
            order.AddItem(new MenuItem(2, "Pizza", 20m, ""));

            order.RemoveItem("Pizza");

            Assert.Single(order.Items);
        }

        [Fact]
        public void GetInfo_CalculatesTotalAndFormatsString()
        {
            Client client = new Client(1, "Anna", "Smith", "123");
            Order order = new Order(99, client);

            order.AddItem(new MenuItem(1, "Pizza", 25.50m, ""));
            order.AddItem(new MenuItem(2, "Cola", 4.50m, ""));

            string info = order.GetInfo();

            Assert.Contains("Order #99", info);
            Assert.Contains("Client: Anna Smith", info);
            Assert.Contains("Pizza (25.50 zł)", info);

            Assert.Contains("Total: 30.00 zł", info);
        }
    }
}
