using System;

namespace Project.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int GuestId { get; set; }
        public Guest Gosc { get; set; } = null!;
        public int PokojId { get; set; }
        public Room Pokoj { get; set; } = null!;
        public int LiczbaOsob { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        public decimal Koszt => (decimal)(DataDo - DataOd).TotalDays * (Pokoj?.CenaZaDobe ?? 0);

        public override string ToString() => $"Rezerwacja: {DataOd:dd-MM-yyyy} - {DataDo:dd-MM-yyyy} ({LiczbaOsob} os.)";
    }
}
