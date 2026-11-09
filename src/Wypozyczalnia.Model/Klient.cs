namespace Wypozyczalnia.Model;

public class Klient : IPrintable{
    public int Id { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Klient() { }
    public Klient(string imie, string email) {
        Imie = imie;
        Email = email;
    }
    public override string ToString() => $"ID: {Id}\nIMIE: {Imie}\nEMAIL: {Email}";
}
