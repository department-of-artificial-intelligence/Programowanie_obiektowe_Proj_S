namespace BikeRental.Models;

public class RentalStation
{
    private readonly List<Bike> _bikes = new();

    public int Id { get; }
    public string Name { get; }
    public IReadOnlyList<Bike> Bikes => _bikes.AsReadOnly();

    public RentalStation(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public void AddBike(Bike bike)
    {
        if (!_bikes.Contains(bike))
            _bikes.Add(bike);
    }

    public void RemoveBike(Bike bike)
    {
        _bikes.Remove(bike);
    }
}
