using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.Model;

public class RentalLogic : IRental
{
    private readonly List<Rental> _completedRentals = new();

    public void ShowRentals(Branch branch)
    {
        if (branch.Rentals.Count <= 0)
        {
            Console.WriteLine("\nBrak aktywnych wypożyczeń w tym oddziale.");
            return;
        }

        branch.Rentals.ForEach(r => Console.WriteLine(r));
    }

    public void ShowHistory(Branch branch)
    {
        var branchHistory = _completedRentals.Where(r => branch.Rentals.All(cr => cr.Id != r.Id)).ToList();

        if (branchHistory.Count <= 0)
        {
            Console.WriteLine("\nBrak zakończonych wypożyczeń w tym oddziale.");
            return;
        }

        branchHistory.ForEach(r => Console.WriteLine(r));
    }

    public void RentCar(int carId, int customerId, DateTime startDate, DateTime endDate, Branch branch)
    {
        if (endDate <= startDate)
        {
            Console.WriteLine("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
            return;
        }

        var car = branch.Cars.FirstOrDefault(c => c.Id == carId);
        if (car is null)
        {
            Console.WriteLine("Nie znaleziono samochodu w tym oddziale.");
            return;
        }

        var customer = branch.Customers.FirstOrDefault(c => c.Id == customerId);
        if (customer is null)
        {
            Console.WriteLine("Nie znaleziono klienta w tym oddziale.");
            return;
        }

        bool overlap = branch.Rentals.Any(r => r.Car?.Id == carId && !(endDate <= r.StartDate || startDate >= r.EndDate));

        if (overlap)
        {
            Console.WriteLine("Samochód jest już zarezerwowany w tym okresie.");
            return;
        }

        int days = (endDate - startDate).Days;

        var rental = new Rental
        {
            Id = branch.Rentals.Any() ? branch.Rentals.Max(r => r.Id) + 1 : 1,
            Car = car,
            Customer = customer,
            StartDate = startDate,
            EndDate = endDate,
            Days = days,
            Cost = car.PricePerDay * days
        };

        branch.Rentals.Add(rental);
        car.Reservations.Add(rental);

        if (startDate <= DateTime.Now)
            car.IsAvailable = false;

        Console.WriteLine($"Wypożyczono {car.Brand} {car.Model} dla {customer.FirstName} {customer.LastName} od {startDate:dd.MM.yyyy} do {endDate:dd.MM.yyyy} ({days} dni).");
    }

    public void ReturnCar(int rentalId, Branch branch)
    {
        var rental = branch.Rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            Console.WriteLine("Nie znaleziono wypożyczenia w tym oddziale.");
            return;
        }

        if (rental.StartDate > DateTime.Now)
        {
            branch.Rentals.Remove(rental);
            rental.Car.Reservations.Remove(rental);
            rental.Car.IsAvailable = true;
            Console.WriteLine($"Anulowano wypożyczenie samochodu {rental.Car.Brand} {rental.Car.Model} (bez kosztów).");
            return;
        }

        rental.Car.IsAvailable = true;
        rental.Car.Reservations.Remove(rental);

        branch.Rentals.Remove(rental);
        _completedRentals.Add(rental);

        Console.WriteLine($"Zwrócono samochód {rental.Car.Brand} {rental.Car.Model}. Koszt całkowity: {rental.Cost} zł");
    }

    public bool HasRentals(Branch branch)
    {
        return branch.Rentals.Count > 0;
    }

    // === RAPORTY dla oddziału ===
    public double TotalRevenue(Branch branch) =>
        _completedRentals.Where(r => branch.Cars.Contains(r.Car)).Sum(r => r.Cost);

    public Car? MostRentedCar(Branch branch) =>
        _completedRentals.Where(r => branch.Cars.Contains(r.Car))
                         .GroupBy(r => r.Car)
                         .OrderByDescending(g => g.Count())
                         .FirstOrDefault()?.Key;

    public Customer? TopCustomer(Branch branch) =>
        _completedRentals.Where(r => branch.Customers.Contains(r.Customer))
                         .GroupBy(r => r.Customer)
                         .OrderByDescending(g => g.Count())
                         .FirstOrDefault()?.Key;
}
