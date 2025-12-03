using CarsRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Logic
{
    public class CarService : ICarService
    {
        private List<Car> _cars = new List<Car>();
        private int _carCounter = 0;

        public void AddCar(Car car)
        {
            Console.WriteLine("Dodawanie pojazdu...");

            if (car == null)
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            _carCounter++;
            car.Id = _carCounter;
            _cars.Add(car);

            Console.WriteLine($"Dodano {car.Brand} {car.Model} {car.ProdYear} do wypożyczalni!\n");
        }

        public void UpdateCar(Car car)
        {
            Console.WriteLine("Aktualizowanie pojazdu...");
            if (car == null)
            {
                Console.WriteLine("Pola nie mogą być puste!");
                return;
            }

            var existingCar = GetCar(car.Id);
            if (existingCar == null)
            {
                Console.WriteLine("Nie znaleziono takiego samochodu");
                return;
            }

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.ProdYear = car.ProdYear;
            existingCar.EngineVolume = car.EngineVolume;
            existingCar.HorsePower = car.HorsePower;
            existingCar.Torque = car.Torque;
            existingCar.TimetoHundred = car.TimetoHundred;
            existingCar.DriveType = car.DriveType;
            existingCar.GearboxType = car.GearboxType;
            existingCar.Seats = car.Seats;
            existingCar.BasePrice = car.BasePrice;
            existingCar.IsAvailable = car.IsAvailable;
            existingCar.Department = car.Department;

            Console.WriteLine($"Zaktualizowano samochód [{car.Id}] {car.Brand} {car.Model}");
        }

        public void RemoveCar(int id)
        {
            Console.WriteLine("Usuwanie pojazdu...");

            Car? carToRemove = GetCar(id);
            if (carToRemove != null)
            {
                _cars.Remove(carToRemove);
                Console.WriteLine($"Usunięto {carToRemove.Brand} {carToRemove.Model} {carToRemove.ProdYear} z wypożyczalni\n");
            }
        }

        public Car? GetCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                Console.WriteLine($"Nie znaleziono samochodu ID({id})\n");
                return null;
            }
            return car;
        }

        public List<Car> GetAllCars()
        {
            return _cars;
        }

        public bool RentCar(int id)
        {
            var car = GetCar(id);
            if (car == null || !car.IsAvailable)
            {
                return false;
            }

            car.IsAvailable = false;
            return true;
        }

        public bool ReturnCar(int id)
        {
            var car = GetCar(id);
            if(car == null || car.IsAvailable)
            {
                return false;
            }

            car.IsAvailable = true;
            return true;
        }

        public List<Car> GetCarsByDepartment(Department department)
        {
            return _cars.Where(c => c.Department == department).ToList();
        }
    }
}
