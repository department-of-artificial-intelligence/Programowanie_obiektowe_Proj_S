using BikeRental.Interfaces;
using BikeRental.Models;

namespace BikeRental.Services;

public class HourlyRentalPriceCalculator : IRentalPriceCalculator
{
    public decimal CalculatePrice(Bike bike, TimeSpan duration)
    {

        int hours = Math.Max(1, (int)Math.Ceiling(duration.TotalHours));
        return hours * bike.PricePerHour;
    }
}
