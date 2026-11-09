namespace BikeRental.Models;

public class Customer
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }

    public Customer(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
