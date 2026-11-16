namespace Wypozyczalnia.Model;

public class Rower : IPrintable {
    public int Id { get; set; }
    public string Nazwa { get; set; } = string.Empty;
    public StatusRoweru Status { get; set; } = StatusRoweru.Dostepny;
    public Rower() { }
    public Rower(string nazwa) {
        Nazwa = nazwa;
    }

    public override string ToString() => $"ID: {Id} \nROWER: {Nazwa} \nSTATUS: {Status}";
}