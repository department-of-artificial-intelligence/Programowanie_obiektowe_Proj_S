using System;

namespace Project.Model
{
    public class Room : IReservable, IHotelElement
    {
        public int Numer { get; set; }
        public int LiczbaMiejsc { get; set; }
        public RoomType Typ { get; set; } = RoomType.Standard;
        public decimal CenaZaDobe { get; set; } = 100m;
        public bool Dostepny { get; private set; } = true;
        public DateTime? Od { get; private set; }
        public DateTime? Do { get; private set; }

        public void Zarezerwuj(DateTime od, DateTime doo)
        {
            Dostepny = false;
            Od = od;
            Do = doo;
        }

        public void Zwolnij()
        {
            Dostepny = true;
            Od = null;
            Do = null;
        }

        public string Info() => ToString();
        public override string ToString()
        {
            string status = Dostepny ? "wolny" : $"zajęty do {Do:dd-MM-yyyy}";
            return $"Pokój {Numer} ({Typ}) - miejsc: {LiczbaMiejsc} - {status} - {CenaZaDobe:C}";
        }
    }
}
