using CarsRental.Logic;
using CarsRental.Model;
using Xunit;

namespace CarsRental.Tests
{
    public class CarServiceTests
    {
        private Car CreateTestCar(Department dept = null)
        {
            return new Car
            {
                Brand = "BMW",
                Model = "M3",
                ProdYear = 2018,
                EngineVolume = 3.0,
                HorsePower = 431,
                Torque = 550,
                TimetoHundred = 4.1,
                DriveType = "RWD",
                GearboxType = "Automatic",
                Seats = 5,
                BasePrice = 300,
                IsAvailable = true,
                Department = dept
            };
        }

        [Fact]
        public void AddCar_ShouldAssignIdAndAddToList()
        {
            var service = new CarService();
            var car = CreateTestCar(new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com"));

            service.AddCar(car);

            var allCars = service.GetAllCars();
            Assert.Single(allCars);
            Assert.Equal(1, allCars[0].Id);
        }

        [Fact]
        public void UpdateCar_ShouldModifyExistingCar()
        {
            var service = new CarService();
            var dept = new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com");
            var car = CreateTestCar(dept);

            service.AddCar(car);

            var updated = CreateTestCar(dept);
            updated.Id = 1;
            updated.Model = "M4";

            service.UpdateCar(updated);

            var result = service.GetCar(1);
            Assert.Equal("M4", result.Model);
        }

        [Fact]
        public void RemoveCar_ShouldRemoveCorrectCar()
        {
            var service = new CarService();
            var dept = new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com");
            var car = CreateTestCar(dept);

            service.AddCar(car);
            service.RemoveCar(1);

            Assert.Empty(service.GetAllCars());
        }

        [Fact]
        public void RentCar_ShouldSetIsAvailableToFalse()
        {
            var service = new CarService();
            var dept = new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com");
            var car = CreateTestCar(dept);

            service.AddCar(car);
            bool result = service.RentCar(1);

            Assert.True(result);
            Assert.False(service.GetCar(1).IsAvailable);
        }

        [Fact]
        public void ReturnCar_ShouldSetIsAvailableToTrue()
        {
            var service = new CarService();
            var dept = new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com");

            var car = CreateTestCar(dept);
            car.IsAvailable = false;

            service.AddCar(car);
            bool result = service.ReturnCar(1);

            Assert.True(result);
            Assert.True(service.GetCar(1).IsAvailable);
        }

        [Fact]
        public void GetCarsByDepartment_ShouldReturnCorrectCars()
        {
            var service = new CarService();

            var dept1 = new Department(1, "Main", "Centrum 1", "111-111-111", "main@cars.com");
            var dept2 = new Department(2, "Airport", "Lotnisko 2", "222-222-222", "airport@cars.com");

            var car1 = CreateTestCar(dept1);
            var car2 = CreateTestCar(dept2);

            service.AddCar(car1);
            service.AddCar(car2);

            var result = service.GetCarsByDepartment(dept1);

            Assert.Single(result);
            Assert.Equal("Main", result[0].Department.Name);
        }
    }
}
