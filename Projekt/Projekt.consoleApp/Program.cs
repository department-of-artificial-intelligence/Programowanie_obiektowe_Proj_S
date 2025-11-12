namespace Projekt.Model
{
    public class Program
    {
        static List<Brach> BazaOdzialow = new List<Brach>();
        static List<Car> BazaSamochodow = new List<Car>();
        static List<Customer> BazaKlientow = new List<Customer>();
        static List<Employere> BazaPracownikow = new List<Employere>();
        static List<Rental> BazaWypozyczalni = new List<Rental>();
}
    static void Main()
    {
        InicjalizujDane();
        MenuGlowne();
    }
    static void InicjalizujDane()
    {
        var oddzialCzew = new Brach
        {
            Id = nextBrachId++
            Name = "Częstochowa Centrum"
            Address = "ul. Warszawska 38"
        }
        var oddzialKrak = new Brach
        {
            Id = nextBrachId++
            Name = "Krakow Centrum"
            Address = "ul. Częstochowska 34"
        }
        BazaOddzialow.AddRange(new[]
        {
            oddzialCzew, oddzialKrak
        });

    }
}

