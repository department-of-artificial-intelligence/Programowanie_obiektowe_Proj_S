using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.Model;

public class RentalLogic : IRental
{
    private readonly List<Rental> _rentals;
    private readonly List<Car> _cars;
    private readonly List<Customer> _customers;
    private readonly List<Branch> _branches;

    public RentalLogic(List<Rental> rentals, List<Car> cars, List<Customer> customers, List<Branch> branches)
    {
        _rentals = rentals;
        _cars = cars;
        _customers = customers;
        _branches = branches;
    }

    public void ShowRentals()
    {
        if (!_rentals.Any())
        {
            Console.WriteLine("Brak wypożyczeń.");
            return;
        }
        _rentals.ForEach(r => Console.WriteLine(r));
    }

    public void RentCar(int carId, int customerId, int days)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        var customer = _customers.FirstOrDefault(c => c.Id == customerId);

        if (car == null || !car.IsAvailable)
        {
            Console.WriteLine("Samochód niedostępny.");
            return;
        }

        if (customer == null)
        {
            Console.WriteLine("Nie znaleziono klienta.");
            return;
        }

        var branch = _branches.FirstOrDefault(b => b.Cars.Contains(car));

        var rental = new Rental
        {
            Car = car,
            Customer = customer,
            Branch = branch,
            Days = days,
            Cost = car.PricePerDay * days,
        };

        _rentals.Add(rental);
        car.IsAvailable = false;

        Console.WriteLine($"Wypożyczono {car.Brand} {car.Model} klientowi {customer.FirstName} {customer.LastName} na {days} dni.");
    }


    public void ReturnCar(int rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            Console.WriteLine("Nie znaleziono wypożyczenia.");
            return;
        }

        rental.Car.IsAvailable = true;
        _rentals.Remove(rental);

        Console.WriteLine($"Zwrócono samochód {rental.Car.Brand} {rental.Car.Model}. Koszt całkowity: {rental.Cost} zł");
    }
}