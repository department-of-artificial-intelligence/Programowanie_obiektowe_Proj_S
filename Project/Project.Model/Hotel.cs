using System.Collections.Generic;

namespace Project.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = "";
        public string Miasto { get; set; } = "";
        public string Adres { get; set; } = "";
        public int Gwiazdki { get; set; }
        public int RokOtwarcia { get; set; }

        public List<Room> Pokoje { get; set; } = new();
        public List<Employees> Pracownicy { get; set; } = new();
        public List<Service> Uslugi { get; set; } = new();
        public List<Reservation> Rezerwacje { get; set; } = new();

        public override string ToString() => $"{Nazwa} ({Miasto}) ★{Gwiazdki}";
    }
}
