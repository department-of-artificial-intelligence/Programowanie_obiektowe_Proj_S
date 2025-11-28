using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.Model;

public class CarLogic : ICar
{
    private readonly List<Car> _cars;
    private readonly List<Branch> _branches;

    public CarLogic(List<Car> cars, List<Branch> branches)
    {
        _cars = cars;
        _branches = branches;
    }

    public void ShowCars()
    {
        if (_cars.Count == 0)
        {
            Console.WriteLine("Brak samochodów.");
            return;
        }

        foreach (var car in _cars)
        {
            var branch = _branches.FirstOrDefault(b => b.Id == car.BranchId);
            Console.WriteLine($"{car} | Oddział: {branch?.Name} {branch?.City}");
        }
    }

    public void AddCar(Car car, int branchId)
    {
        var branch = _branches.FirstOrDefault(b => b.Id == branchId);
        if (branch == null)
        {
            Console.WriteLine("Nie znaleziono oddziału.");
            return;
        }

        car.Id = _cars.Count > 0 ? _cars.Max(c => c.Id) + 1 : 1;
        car.BranchId = branchId;
        car.IsAvailable = true;

        _cars.Add(car);
        branch.AddCar(car);

        Console.WriteLine($"Dodano samochód {car.Brand} {car.Model} do oddziału {branch.Name} {branch.City}");
    }

    public void RemoveCar(int carId)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        if (car == null)
        {
            Console.WriteLine("Nie znaleziono samochodu.");
            return;
        }

        var branch = _branches.FirstOrDefault(b => b.Id == car.BranchId);
        branch?.RemoveCar(car);
        _cars.Remove(car);

        Console.WriteLine($"Usunięto samochód {car.Brand} {car.Model}");
    }
}