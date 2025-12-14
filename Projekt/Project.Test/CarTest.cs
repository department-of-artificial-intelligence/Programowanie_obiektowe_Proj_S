using Projekt.Model;
namespace Project.Test
{
    public class CarTests
    {
        [Fact]
        public void TestDanych()
        {
            var branch = new Branch { Name = "Warszawa Centrum" };
            var car = new Car
            {
                Id = 1,
                Marka = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "WA 12345",
                Status = CarStatus.Available,
                CurrentBranch = branch
            };

            var result = car.ToString();


            Assert.Contains("Oddzia³: Warszawa Centrum", result);
            Assert.Contains("[1] Toyota Corolla", result);
        }

        [Fact]
        public void TestBraacha()
        {

            var car = new Car
            {
                Id = 2,
                Marka = "Ford",
                Model = "Focus",
                RegistrationNumber = "KR 99999",
                Status = CarStatus.Rented,
                CurrentBranch = null
            };

            var result = car.ToString();

            Assert.Contains("Oddzia³: Brak", result);

            Assert.Contains("Ford Focus", result);
        }

        [Fact]
        public void TestKosztu()
        {
            var car = new Car();
            decimal expectedRate = 150.50m;
            car.DailyRate = expectedRate;

            Assert.Equal(expectedRate, car.DailyRate);
        }
    }
}