namespace BikeRental.Models;

public class Rental
{
    public int Id { get; }
    public Customer Customer { get; }
    public Bike Bike { get; }
    public RentalStation StartStation { get; }
    public RentalStation? ReturnStation { get; private set; }
    public RentalPeriod Period { get; }
    public decimal TotalPrice { get; private set; }
    public bool IsActive { get; private set; } = true;

    public Rental(int id, Customer customer, Bike bike, RentalStation startStation)
    {
        Id = id;
        Customer = customer;
        Bike = bike;
        StartStation = startStation;
        Period = new RentalPeriod();
    }

    public void Complete(RentalStation returnStation, decimal price)
    {
        Period.Finish();
        ReturnStation = returnStation;
        TotalPrice = price;
        IsActive = false;
    }
}
