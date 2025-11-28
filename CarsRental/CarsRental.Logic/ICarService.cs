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
        void RemoveCar(int id);
        Car? GetCar(int id);
        List<Car> GetAllCars();
        bool RentCar(int id);
        bool ReturnCar(int id);
        List<Car> GetCarsByDepartment(Department department);
    }
}
