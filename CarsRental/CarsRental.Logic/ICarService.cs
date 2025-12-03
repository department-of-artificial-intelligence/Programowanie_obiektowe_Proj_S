using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsRental.Model;

namespace CarsRental.Logic
{
    public interface ICarService
    {
        void AddCar(Car car);
        void UpdateCar(Car car);
        void RemoveCar(int carId);
        Car? GetCar(int carId);
        List<Car> GetAllCars();
        bool RentCar(int carId);
        bool ReturnCar(int carId);
        List<Car> GetCarsByDepartment(Department department);
    }
}
