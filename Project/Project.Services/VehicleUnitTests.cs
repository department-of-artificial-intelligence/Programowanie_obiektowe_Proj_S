using Project.Model;
using Project.Abstractions;
using Xunit;

namespace Project.Tests
{
    public class VehicleTests
    {
        private class FakeDriver : IDriver
        {
            public bool IsAvailable => Status == DriverStatus.Available;

            public int Id { get; set; }
            public string FirstName => "A";
            public string LastName => "B";
            public string LicenseNumber => "X";
            public DriverStatus Status { get; set; } = DriverStatus.Available;
            public IVehicle? AssignedVehicle => null;

            public List<IOrder> Orders { get; } = new();

            public void AssignOrder(IOrder order) { }
            public void RemoveOrder(IOrder order) { }
            public void AssignVehicle(IVehicle vehicle) { }
            public void MarkAsAvailable() { Status = DriverStatus.Available; }
        }

        [Fact]
        public void IsAvailableWhenStatusAvailableTest()
        {
            var v = new DeliveryVan(1, "V", 2000, 2, 0, "Ford", "M", "R", 5)
            {
                VStatus = VehicleStatus.Available
            };

            Assert.True(v.IsAvailable);
        }

        [Fact]
        public void AssignDriverWhenDriverNullTest()
        {
            var v = new DeliveryVan(1, "V", 2000, 2, 0, "Ford", "M", "R", 5);

            Assert.Throws<ArgumentNullException>(() => v.AssignDriver(null));
        }

        [Fact]
        public void AssignDriverWhenDriverNotAvailableTest()
        {
            var v = new DeliveryVan(1, "V", 2000, 2, 0, "Ford", "M", "R", 5);
            var driver = new FakeDriver { Status = DriverStatus.Assigned };

            Assert.Throws<InvalidOperationException>(() => v.AssignDriver(driver));
        }

        [Fact]
        public void AssignDriverTest()
        {
            var v = new DeliveryVan(1, "V", 2000, 2, 0, "Ford", "M", "R", 5);
            var driver = new Driver(1, "Jan", "Kowalski", "ABC12345");

            v.AssignDriver(driver);

            Assert.Equal(VehicleStatus.InTransit, v.VStatus);
            Assert.Equal(driver, v.AssignedDriver);
        }

        [Fact]
        public void MarkAsAvailableTest()
        {
            var driver = new Driver(1, "Jan", "Kowalski", "ABC12345");

            var v = new DeliveryVan(1, "V", 2000, 2, 0, "Ford", "M", "R", 5)
            {
                AssignedDriver = driver,
                VStatus = VehicleStatus.InTransit
            };

            v.MarkAsAvailable();

            Assert.Null(v.AssignedDriver);
            Assert.Equal(VehicleStatus.Available, v.VStatus);
        }
    }
}