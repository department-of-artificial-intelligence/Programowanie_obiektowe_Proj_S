using Projekt.Model;

namespace Project.Test
{
    public class CarTest
    {
        [Fact]
        static void TestujCar()
        {
            var auto = new Car
            {
                Id = 1,
                Marka = "TestAudi",
                Model = "A4",
                Year = 2020,
                RegistrationNumber = "T1 TEST",
                Status = CarStatus.Available,
                DailyRate = 200
            };

            Assert.True(auto.Marka == "TestAudi", "Marka powinna byæ 'TestAudi'");
            Assert.True(auto.Status == CarStatus.Available, "Domyœlny status to Available");

            string opis = auto.ToString();
            Assert.True(opis.Contains("TestAudi") && opis.Contains("A4"), "ToString musi zawieraæ markê i model");  
        }
    }
}