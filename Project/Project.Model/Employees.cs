namespace SiecHoteli
{
    public class Employees
    {
        public int Id { get; set; }
        public string Imie { get; set; } = string.Empty;
        public string Nazwisko { get; set; } = string.Empty;
        public string Stanowisko { get; set; } = string.Empty;

        public override string ToString() => $"{Id}: {Imie} {Nazwisko} - {Stanowisko}";
    }
}
