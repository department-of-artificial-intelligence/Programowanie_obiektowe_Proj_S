using BikeRental.Interfaces;
using BikeRental.Models;

namespace BikeRental.Services;

public class BikeRentalManager
{
    private readonly List<Bike> _bikes = new();
    private readonly List<Customer> _customers = new();
    private readonly List<RentalStation> _stations = new();
    private readonly List<Rental> _rentals = new();
    private readonly IRentalPriceCalculator _priceCalculator;

    public IReadOnlyList<Bike> Bikes => _bikes.AsReadOnly();
    public IReadOnlyList<Customer> Customers => _customers.AsReadOnly();
    public IReadOnlyList<RentalStation> Stations => _stations.AsReadOnly();
    public IReadOnlyList<Rental> Rentals => _rentals.AsReadOnly();

    public BikeRentalManager(IRentalPriceCalculator priceCalculator)
    {
        _priceCalculator = priceCalculator;
    }

    public Customer AddCustomer(string name, string email)
    {
        var customer = new Customer(_customers.Count + 1, name, email);
        _customers.Add(customer);
        return customer;
    }

    public Bike AddBike(string model, decimal pricePerHour)
    {
        var bike = new Bike(_bikes.Count + 1, model, pricePerHour);
        _bikes.Add(bike);
        return bike;
    }

    public RentalStation AddStation(string name)
    {
        var station = new RentalStation(_stations.Count + 1, name);
        _stations.Add(station);
        return station;
    }

    public void AddBikeToStation(int bikeId, int stationId)
    {
        Bike bike = _bikes.FirstOrDefault(b => b.Id == bikeId)
            ?? throw new InvalidOperationException("Bike not found.");
        RentalStation station = _stations.FirstOrDefault(s => s.Id == stationId)
            ?? throw new InvalidOperationException("Station not found.");

        if (!bike.IsAvailable)
            throw new InvalidOperationException("This bike is rented.");

        bool bikeIsAtAnotherStation = _stations.Any(s => s.Bikes.Any(b => b.Id == bikeId));
        if (bikeIsAtAnotherStation)
            throw new InvalidOperationException("This bike is already at a station.");

        station.AddBike(bike);
    }

    public Rental RentBike(int customerId, int bikeId)
    {
        Customer customer = _customers.FirstOrDefault(c => c.Id == customerId)
            ?? throw new InvalidOperationException("Customer not found.");
        Bike bike = _bikes.FirstOrDefault(b => b.Id == bikeId)
            ?? throw new InvalidOperationException("Bike not found.");

        if (!bike.IsAvailable)
            throw new InvalidOperationException("This bike is already rented.");

        RentalStation? station = _stations.FirstOrDefault(
            s => s.Bikes.Any(b => b.Id == bikeId));

        if (station == null)
            throw new InvalidOperationException("Bike is not at any station.");

        station.RemoveBike(bike);
        bike.Rent();

        var rental = new Rental(_rentals.Count + 1, customer, bike, station);
        _rentals.Add(rental);
        return rental;
    }

    public decimal ReturnBike(int rentalId, int stationId)
    {
        Rental rental = _rentals.FirstOrDefault(r => r.Id == rentalId)
            ?? throw new InvalidOperationException("Rental not found.");
        RentalStation station = _stations.FirstOrDefault(s => s.Id == stationId)
            ?? throw new InvalidOperationException("Station not found.");

        if (!rental.IsActive)
            throw new InvalidOperationException("This rental is already completed.");

        decimal price = _priceCalculator.CalculatePrice(rental.Bike, rental.Period.GetDuration());

        rental.Complete(station, price);
        rental.Bike.Return();
        station.AddBike(rental.Bike);

        return price;
    }
}
