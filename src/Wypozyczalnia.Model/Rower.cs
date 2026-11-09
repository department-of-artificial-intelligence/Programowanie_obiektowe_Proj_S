namespace Wypozyczalnia.Model;

public class Rower : IZmienStatus, IPrintable {
    public int Id { get; set; }
    public string Nazwa { get; set; } = string.Empty;
    public StatusRoweru Status { get; private set; } = StatusRoweru.Dostepny;
    public Rower() { }
    public Rower(string nazwa) {
        Nazwa = nazwa;
    }
    public void ZmienStatus(StatusRoweru nowy) {
        Status = nowy;
    }

    public override string ToString() => $"ID: {Id} \nROWER: {Nazwa} \nSTATUS: {Status}";
}