using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Tests
{
    public class BranchTests
    {
        [Fact]
        public void Test1()
        {
            int expectedId = 1;
            string expectedName = "Wypozyczalnia";
            string expectedCity = "Czestochowa";

            Branch branch = new Branch(expectedId, expectedName, expectedCity);

            Assert.Equal(expectedId, branch.Id);
            Assert.Equal(expectedName, branch.Name);
            Assert.Equal(expectedCity, branch.City);

            Assert.Empty(branch.Cars);
        }

        [Fact]
        public void Test2()
        {
            var branch = new Branch(2, "Rental", "Myszkow");

            string result = branch.ToString();
            string expected = "Oddział 2: Rental (Myszkow), Samochody: 0";

            Assert.Equal(expected, result);
            Assert.Empty(branch.Cars);
        }

        [Fact]
        public void Test3()
        {
            var branch = new Branch(5, "Rental", "Katowice");

            string brand = "BMW";
            string model = "M3";
            int productionYear = 2024;
            var car = new Car
            {
                Brand = brand,
                Model = model,
                ProductionYear = productionYear
            };

            branch.AddCar(car);

            string result = branch.ToString();
            string expected = "Oddział 5: Rental (Katowice), Samochody: 1";

            Assert.Equal(expected, result);
            Assert.NotEmpty(branch.Cars);
            Assert.Contains(car, branch.Cars);
            Assert.Single(branch.Cars);
        }

        [Fact]
        public void Test4()
        {
            var branch = new Branch(6, "Wypozyczalnia", "Gdansk");
            var car = new Car { Id = 1, Brand = "Ford", Model = "Focus" };
            branch.AddCar(car);

            Assert.NotEmpty(branch.Cars);
            Assert.Contains(car, branch.Cars);

            branch.RemoveCar(car);

            string result = branch.ToString();
            string expected = "Oddział 6: Wypozyczalnia (Gdansk), Samochody: 0";

            Assert.Equal(expected, result);
            Assert.Empty(branch.Cars);
            Assert.DoesNotContain(car, branch.Cars);
        }
    }
}