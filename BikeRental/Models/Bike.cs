namespace BikeRental.Models;

public class Bike
{
    public int Id { get; }
    public string Model { get; }
    public decimal PricePerHour { get; }
    public bool IsAvailable { get; private set; } = true;

    public Bike(int id, string model, decimal pricePerHour)
    {
        Id = id;
        Model = model;
        PricePerHour = pricePerHour;
    }

    public virtual decimal GetHourlyPrice() { return PricePerHour; }



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
