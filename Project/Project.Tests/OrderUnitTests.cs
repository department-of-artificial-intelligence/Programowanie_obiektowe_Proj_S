using Project.Model;
using Project.Abstractions;

namespace Project.Tests
{
    public class OrderTests
    {
        private Driver CreateDriverWithVehicle()
        {
            var driver = new Driver(1, "Jan", "Kowalski", "X1");
            var van = new DeliveryVan(2, "VIN", 2020, 2, 0, "Ford", "T", "REG1", 10);
            driver.AssignVehicle(van);
            return driver;
        }

        [Fact]
        public void AssignOrderWhenNoVehicleTest() // Does not assign when Driver has no Vehicle
        {
            var order = new Order(1, "Load", "A", "B");
            var driver = new Driver(1, "Jan", "Kowalski", "X1");

            order.AssignOrder(driver);

            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Null(order.AssignedDriver);
            Assert.Null(order.AssignedVehicle);
        }

        [Fact]
        public void AssignOrderWhenDriverNotAvailableTest() // Does not assign when Driver not Available
        {
            var order = new Order(1, "Load", "A", "B");
            var driver = CreateDriverWithVehicle();
            driver.Status = DriverStatus.Assigned;

            order.AssignOrder(driver);

            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Null(order.AssignedDriver);
            Assert.Null(order.AssignedVehicle);
        }

        [Fact]
        public void AssignOrderWhenOrderNotPendingTest() // Does not assign when Order not Pending
        {
            var order = new Order(1, "Load", "A", "B");
            var driver = CreateDriverWithVehicle();

            typeof(Order)
                .GetProperty("Status")!
                .SetValue(order, OrderStatus.Completed);

            order.AssignOrder(driver);

            Assert.Equal(OrderStatus.Completed, order.Status);
            Assert.Null(order.AssignedDriver);
            Assert.Null(order.AssignedVehicle);
        }

        private class FakeDriver : IDriver
        {
            public int Id => 99;
            public string FirstName => "X";
            public string LastName => "Y";
            public string LicenseNumber => "L";
            public DriverStatus Status => DriverStatus.Available;
            public bool IsAvailable => true;
            public IVehicle? AssignedVehicle => null;
            public List<IOrder> Orders { get; } = new();

            public void AssignVehicle(IVehicle vehicle) { }
            public void MarkAsAvailable() { }
            public void AssignOrder(IOrder order) { }
            public void RemoveOrder(IOrder order) { }
        }

        [Fact]
        public void AssignOrderWhenDriverIsNotConcreteDriverTest() // Throws when Driver is not concrete Driver
        {
            var order = new Order(1, "Load", "A", "B");
            var fakeDriver = new FakeDriver();

            Assert.Throws<ArgumentException>(() => order.AssignOrder(fakeDriver));
        }
    }
}