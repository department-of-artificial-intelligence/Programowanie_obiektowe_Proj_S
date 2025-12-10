using Project.Model;

namespace Project.Tests
{
    public class DeliveryVanTests
    {
        [Fact]
        public void VTypeTest() // Returns DeliveryVan
        {
            var van = new DeliveryVan(1, "V", 2000, 5, 0, "Ford", "T", "R2", 10);
            Assert.Equal(Project.Abstractions.VehicleType.DeliveryVan, van.VType);
        }

        [Fact]
        public void CalculateWearRateTest() // Returns positive value
        {
            var van = new DeliveryVan(1, "V", DateTime.Now.Year - 3, 5, 30000, "Ford", "T", "R2", 10);

            float wear = van.CalculateWearRate();

            Assert.True(wear > 0);
        }
    }
}
