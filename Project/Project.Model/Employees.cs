using System;

namespace Project.Model
{
    public class Employees : IPerson
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public string Stanowisko { get; set; } = "";
        public decimal Pensja { get; set; } 
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";
        public DateTime DataZatrudnienia { get; set; }

        public string PelneDane()
            => $"{Imie} {Nazwisko} ({Stanowisko}) | Zarobki: {Pensja:C} | Zatrudniony: {DataZatrudnienia:DD-MM-YYYY}";

        public override string ToString() => PelneDane();
    }
}
