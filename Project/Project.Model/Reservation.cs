using System;

namespace SiecHoteli
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ImieKlienta { get; set; } = string.Empty;
        public string NazwiskoKlienta { get; set; } = string.Empty;
        public Room Pokoj { get; set; } = new Room();
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        public override string ToString() =>
            $"Rezerwacja #{Id}: {ImieKlienta} {NazwiskoKlienta}, pokój {Pokoj.Numer}, {DataOd:d} - {DataDo:d}";
    }
}
