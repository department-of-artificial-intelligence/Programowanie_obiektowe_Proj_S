using System;

namespace Project.Model
{
    public class Reservation : IHotelElement
    {
        public int Id { get; set; }
        public Guest Gosc { get; set; } = new();
        public Room Pokoj { get; set; } = new();
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public decimal Koszt => (decimal)(DataDo - DataOd).TotalDays * Pokoj.CenaZaDobe;

        public string Info() => ToString();
        public override string ToString() => $"Rezerwacja #{Id}: {Gosc.PelneDane()}, pokój {Pokoj.Numer}, {DataOd:dd-MM-yyyy} - {DataDo:dd-MM-yyyy} | koszt: {Koszt:C}";
    }
}
