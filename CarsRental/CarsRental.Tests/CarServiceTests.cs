using CarsRental.Logic;
using CarsRental.Model;
using Xunit;

namespace CarsRental.Tests
{
    public class CarServiceTests
    {
        private CarService _carService;
        private Department _testDepartment;

        public CarServiceTests()
        {
            _carService = new CarService();
            _testDepartment = new Department();
        }

        [Fact]
        public void AddCar_Success()
        {
            var car = new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, true, _testDepartment);
            _carService.AddCar(car);
            Assert.Equal(1, car.Id);
            Assert.Single(_carService.GetAllCars());
        }

        [Fact]
        public void AddCar_Null_Fail()
        {
            _carService.AddCar(null);
            Assert.Empty(_carService.GetAllCars());
        }

        [Fact]
        public void GetCar_ExistingId_Success()
        {
            var car = new Car(0, "Audi", "A6", 2021, 2.0, 245, 370, 6.2, "AWD", "Automatyczna", 5, 280.0, true, _testDepartment);
            _carService.AddCar(car);

            var result = _carService.GetCar(1);

            Assert.NotNull(result);
            Assert.Equal("Audi", result.Brand);
            Assert.Equal("A6", result.Model);
        }

        [Fact]
        public void GetCar_NonExistingId_Fail()
        {
            var result = _carService.GetCar(999);
            Assert.Null(result);
        }

        [Fact]
        public void RemoveCar_ExistingCar_Remove()
        {
            var car = new Car(0, "Toyota", "Corolla", 2020, 1.8, 140, 180, 9.5, "FWD", "Manualna", 5, 180.0, true, _testDepartment);
            _carService.AddCar(car);

            _carService.RemoveCar(1);

            Assert.Empty(_carService.GetAllCars());
        }

        [Fact]
        public void RentCar_AvailableCar_Success()
        {
            var car = new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, true, _testDepartment);
            _carService.AddCar(car);

            bool result = _carService.RentCar(1);

            Assert.True(result);
            Assert.False(_carService.GetCar(1).IsAvailable);
        }

        [Fact]
        public void RentCar_UnavailableCar_Fail()
        {
            var car = new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, false, _testDepartment);
            _carService.AddCar(car);

            bool result = _carService.RentCar(1);

            Assert.False(result);
        }

        [Fact]
        public void ReturnCar_RentedCar_Success()
        {
            var car = new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, false, _testDepartment);
            _carService.AddCar(car);

            bool result = _carService.ReturnCar(1);

            Assert.True(result);
            Assert.True(_carService.GetCar(1).IsAvailable);
        }

        [Fact]
        public void GetCarsByDepartment_Success()
        {
            var dep2 = new Department(2, "Other Rental", "Other Address", "987654321", "other@test.pl");
            _carService.AddCar(new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, true, _testDepartment));
            _carService.AddCar(new Car(0, "Audi", "A6", 2021, 2.0, 245, 370, 6.2, "AWD", "Automatyczna", 5, 280.0, true, dep2));
            _carService.AddCar(new Car(0, "Toyota", "Corolla", 2020, 1.8, 140, 180, 9.5, "FWD", "Manualna", 5, 180.0, true, _testDepartment));

            var result = _carService.GetCarsByDepartment(_testDepartment);

            Assert.Equal(2, result.Count);
            Assert.All(result, car => Assert.Equal(_testDepartment, car.Department));
        }

        [Fact]
        public void GetAllCars_Success()
        {
            _carService.AddCar(new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, true, _testDepartment));
            _carService.AddCar(new Car(0, "Audi", "A6", 2021, 2.0, 245, 370, 6.2, "AWD", "Automatyczna", 5, 280.0, true, _testDepartment));
            _carService.AddCar(new Car(0, "Toyota", "Corolla", 2020, 1.8, 140, 180, 9.5, "FWD", "Manualna", 5, 180.0, true, _testDepartment));

            var result = _carService.GetAllCars();

            Assert.Equal(3, result.Count);
        }
    }
}
