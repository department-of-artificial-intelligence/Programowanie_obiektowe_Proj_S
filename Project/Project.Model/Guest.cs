namespace Project.Model
{
    public class Guest : IPerson
    {
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string PelneDane() => $"{Imie} {Nazwisko}";
    }
}