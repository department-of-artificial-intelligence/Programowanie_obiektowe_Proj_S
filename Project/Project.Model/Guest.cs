using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Guest : IPerson
    {
        [Key]
        public int Id { get; set; }

        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";

        public string PelneDane() => $"{Imie} {Nazwisko}";
    }
}
