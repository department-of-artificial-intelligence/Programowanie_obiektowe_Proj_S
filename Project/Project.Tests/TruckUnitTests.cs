using Project.Model;

namespace Project.Tests
{
    public class TruckTests
    {
        [Fact]
        public void VTypeTest() // Returns truck
        {
            var truck = new Truck(1, "V", 2000, 10, 0, "Volvo", "FH", "R4", 20000);
            Assert.Equal(Project.Abstractions.VehicleType.Truck, truck.VType);
        }

        [Fact]
        public void CalculateWearRateTest() // ReturnsPositiveValue
        {
            var truck = new Truck(1, "V", DateTime.Now.Year - 4, 10, 50000, "Volvo", "FH", "R4", 20000);

            float wear = truck.CalculateWearRate();

            Assert.True(wear > 0);
        }
    }
}
