using System.Collections.Generic;

namespace SiecHoteli
{
    public class Hotel
    {
        public string Nazwa { get; set; } = string.Empty;
        public string Miasto { get; set; } = string.Empty;
        public List<Employees> Pracownicy { get; set; } = new();
        public List<Room> Pokoje { get; set; } = new();
        public List<Reservation> Rezerwacje { get; set; } = new();

        public override string ToString() => $"{Nazwa} ({Miasto})";
    }
}
