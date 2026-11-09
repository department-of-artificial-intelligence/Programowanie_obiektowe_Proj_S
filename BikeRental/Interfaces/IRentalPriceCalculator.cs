using BikeRental.Models;

namespace BikeRental.Interfaces;

public interface IRentalPriceCalculator
{
    decimal CalculatePrice(Bike bike, TimeSpan duration);
}
