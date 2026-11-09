namespace Korepetycje.Models;

public class GrupaKorepetycyjna
{
    private readonly List<Uczen> _uczniowie = new();

    public string Nazwa { get; private set;}
    public IReadOnlyList<Uczen> Uczniowie => _uczniowie;

    public GrupaKorepetycyjna(string nazwa)
    {
        Nazwa = nazwa;
    }

    public void DodajUcznia(Uczen uczen)
    {
        if (!_uczniowie.Contains(uczen))
        {
            _uczniowie.Add(uczen);
            Console.WriteLine($"Dodano ucznia: {uczen} do grupy {this}");
        }
    }

    public override string ToString()
    {
        return $"{Nazwa} - Liczba uczniów: {_uczniowie.Count}";
    }
}