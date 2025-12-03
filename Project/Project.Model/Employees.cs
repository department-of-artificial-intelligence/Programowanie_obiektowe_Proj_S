using System;

namespace Project.Model
{
    public class Employees : IPerson, IHotelElement
    {
        public int Id { get; set; }
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string Email { get; set; } = "";
        public string Stanowisko { get; set; } = "";
        public DateTime DataZatrudnienia { get; set; } = DateTime.Now;

        public string PelneDane() => $"{Imie} {Nazwisko}";
        public string Info() => ToString();
        public override string ToString() => $"{Id}: {Imie} {Nazwisko} — {Stanowisko} | tel: {Telefon} | e: {Email} | zatrudniony: {DataZatrudnienia:dd-MM-yyyy}";
    }
}
