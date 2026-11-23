using BikeRental.Interfaces;
namespace BikeRental.Models;

public abstract class Bike : IRentable
{
    public int Id { get; }
    public string Model { get; }
    public decimal PricePerHour { get; }
    public bool IsAvailable { get; private set; } = true;

    protected Bike(int id, string model, decimal pricePerHour)
    {
        Id = id;
        Model = model;
        PricePerHour = pricePerHour;
    }

    public abstract decimal GetHourlyPrice();



    public void Rent()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("This bike is already rented.");

        IsAvailable = false;
    }

    public void Return()
    {
        IsAvailable = true;
    }
}
