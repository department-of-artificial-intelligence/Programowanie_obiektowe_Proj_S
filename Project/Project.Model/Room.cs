using System;

namespace Project.Model
{
    public class Room
    {
        public int Id { get; set; }
        public int Numer { get; set; }
        public int LiczbaMiejsc { get; set; }
        public RoomType Typ { get; set; }
        public decimal CenaZaDobe { get; set; }

        public bool Dostepny { get; private set; } = true;
        public DateTime? Od { get; private set; }
        public DateTime? Do { get; private set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;

        public void Zarezerwuj(DateTime od, DateTime doo)
        {
            if (doo <= od) throw new ArgumentException("Niepoprawny zakres dat.");

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

        public override string ToString() => $"Pokój {Numer} | {Typ} | {CenaZaDobe:C}";
    }
}
