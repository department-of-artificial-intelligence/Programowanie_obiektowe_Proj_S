namespace ConsoleApp;
public class Klient {
    private int id;
    private string imie;
    private string email;

    public Klient(int id, string imie, string email) {
        this.id = id;
        this.imie = imie;
        this.email = email;
    }

    public void Zarejestruj() {
        Console.WriteLine($"Zarejestrowano klienta: {imie}");
    }

    public void Zaloguj() {
        Console.WriteLine($"Zalogowano klienta: {imie}");
    }

    public override string ToString() => $"ID: {id}\nIMIE: {imie}\nEMAIL: {email}";
}
