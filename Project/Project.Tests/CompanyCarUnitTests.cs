using Project.Model;

namespace Project.Tests
{
    public class CompanyCarTests
    {
        [Fact]
        public void VTypeTest() // Returns CompanyCar
        {
            var car = new CompanyCar(1, "V", 2000, 2, 10000, "Ford", "T", "R1", 5);
            Assert.Equal(Project.Abstractions.VehicleType.CompanyCar, car.VType);
        }

        [Fact]
        public void CalculateWearRateWhenMoreThanFiveSeatsTest() // Increases when more than five seats
        {
            var car = new CompanyCar(1, "V", DateTime.Now.Year - 10, 2, 50000, "Ford", "T", "R1", 6);

            float wear = car.CalculateWearRate();

            Assert.True(wear > 0);
            Assert.True(wear > 0.02f);
        }

        [Fact]
        public void CalculateWearRateForFiveSeatsTest() // Checks the normal consumption calculation
        {
            var car = new CompanyCar(1, "V", DateTime.Now.Year - 5, 2, 20000, "Ford", "T", "R1", 5);

            float wear = car.CalculateWearRate();

            Assert.True(wear > 0);
        }
    }
}