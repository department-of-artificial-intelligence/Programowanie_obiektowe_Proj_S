using Project.Model;
using Project.Abstractions;

namespace Project.Tests
{
    public class DriverTests
    {
        private class FakeOrder : IOrder
        {
            public int Id => 1;
            public string LoadDesc => "X";
            public string LoadingAddress => "A";
            public string UnloadingAdress => "B";
            public OrderStatus Status => OrderStatus.Pending;

            public IDriver? AssignedDriver => null;
            public IVehicle? AssignedVehicle => null;

            public void AssignOrder(IDriver driver) { }
        }

        [Fact]
        public void IsAvailableTest() // Returns True if Status == Available
        {
            var d = new Driver(1, "A", "B", "C") { Status = DriverStatus.Available };
            Assert.True(d.IsAvailable);
        }

        [Fact]
        public void AssignVehicle_SetsVehicleAndStatus() // Sets Vehicle and Status
        {
            var d = new Driver(1, "A", "B", "C");
            var v = new DeliveryVan(1, "V", 2000, 5, 0, "Ford", "T", "R", 5);

            d.AssignVehicle(v);

            Assert.Equal(v, d.AssignedVehicle);
            Assert.Equal(DriverStatus.Assigned, d.Status);
        }

        [Fact]
        public void MarkAsAvailableTest() // Clears Vehicle and sets Status
        {
            var d = new Driver(1, "A", "B", "C")
            {
                AssignedVehicle = new DeliveryVan(1, "V", 2000, 5, 0, "Ford", "T", "R", 5),
                Status = DriverStatus.Assigned
            };

            d.MarkAsAvailable();

            Assert.Null(d.AssignedVehicle);
            Assert.Equal(DriverStatus.Available, d.Status);
        }

        [Fact]
        public void AssignOrderTest() // Clears Vehicle and sets Status
        {
            var d = new Driver(1, "A", "B", "C");
            var order = new FakeOrder();

            d.AssignOrder(order);

            Assert.Contains(order, d.Orders);
            Assert.Equal(DriverStatus.Assigned, d.Status);
        }

        [Fact]
        public void RemoveOrderTest() // Removes Order and restores Status
        {
            var d = new Driver(1, "A", "B", "C");
            var order = new FakeOrder();
            d.AssignOrder(order);

            d.RemoveOrder(order);

            Assert.DoesNotContain(order, d.Orders);
            Assert.Equal(DriverStatus.Available, d.Status);
        }
    }
}