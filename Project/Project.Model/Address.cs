namespace Project.Model;

public class Address
{
    // Właściwości
    public string Country { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;

    // Konstruktory
    public Address() { }

    public Address(string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Kraj nie może być null lub pusty", nameof(country));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("Miasto nie może być null lub puste", nameof(city));
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Ulica nie może być null lub pusta", nameof(street));
        Country = country;
        City = city;
        Street = street;
    }

    // Metody string
    public override string ToString()
    {
        return $"{Country}, {City}, {Street}";
    }
}
