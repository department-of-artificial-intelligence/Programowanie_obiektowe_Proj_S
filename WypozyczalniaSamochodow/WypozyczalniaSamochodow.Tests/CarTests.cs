using Microsoft.VisualBasic.FileIO;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Tests
{
    public class CarTests
    {
        [Fact]
        public void Test1()
        {
            int expectedId = 1;
            string expectedBrand = "BMW";
            string expectedModel = "M4";
            int expectedProductionYear = 2024;
            float expectedEngineVolume = 3.0f;
            double expectedAvgConsumption = 12.5;
            int expectedPower = 500;
            string expectedGearbox = "Automatyczna";
            string expectedFuelType = "Benzyna";
            double expectedPricePerDay = 600.0;
            bool expectedIsAvailable = true;
            int expectedBranchId = 3;

            var car = new Car(expectedId, expectedBrand, expectedModel, expectedProductionYear, expectedEngineVolume,
                              expectedAvgConsumption, expectedPower, expectedGearbox, expectedFuelType,
                              expectedPricePerDay, expectedIsAvailable, expectedBranchId);

            Assert.Equal(expectedId, car.Id);
            Assert.Equal(expectedBrand, car.Brand);
            Assert.Equal(expectedModel, car.Model);
            Assert.Equal(expectedProductionYear, car.ProductionYear);
            Assert.Equal(expectedEngineVolume, car.EngineVolume);
            Assert.Equal(expectedAvgConsumption, car.AvgConsumption);
            Assert.Equal(expectedPower, car.Power);
            Assert.Equal(expectedGearbox, car.Gearbox);
            Assert.Equal(expectedFuelType, car.FuelType);
            Assert.Equal(expectedPricePerDay, car.PricePerDay);
            Assert.Equal(expectedIsAvailable, car.IsAvailable);
            Assert.Equal(expectedBranchId, car.BranchId);
        }

        [Fact]
        public void Test2()
        {
            var car = new Car(1, "Mercedes", "AMG GT", 2025, 6.3f, 15.5, 720, "Automatyczna", "Benzyna", 800.0, false, 2);

            string result = car.ToString();
            string expected = $"[1]: Mercedes AMG GT (2025) | Cena/dzień: 800zł | Specyfikacja: \n     Moc: 720KM | Pojemność silnika: 6.3l | Średnie spalanie: 15.5l/100km | Skrzynia: Automatyczna | Typ paliwa: Benzyna";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test3()
        {
            var car = new Car();

            car.Id = 2;
            car.Brand = "Honda";
            car.Model = "Civic";
            car.ProductionYear = 2008;
            car.EngineVolume = 2.0f;
            car.AvgConsumption = 7.0;
            car.Power = 110;
            car.Gearbox = "Manualna";
            car.FuelType = "Diesel";
            car.PricePerDay = 150.0;
            car.IsAvailable = true;
            car.BranchId = 4;

            Assert.Equal(2, car.Id);
            Assert.Equal("Honda", car.Brand);
            Assert.Equal("Civic", car.Model);
            Assert.Equal(2008, car.ProductionYear);
            Assert.Equal(2.0f, car.EngineVolume);
            Assert.Equal(7.0, car.AvgConsumption);
            Assert.Equal(110, car.Power);
            Assert.Equal("Manualna", car.Gearbox);
            Assert.Equal("Diesel", car.FuelType);
            Assert.Equal(150.0, car.PricePerDay);
            Assert.True(car.IsAvailable);
            Assert.Equal(4, car.BranchId);
        }
    }
}
