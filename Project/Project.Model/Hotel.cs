using System.Collections.Generic;

namespace SiecHoteli
{
    public class Hotel
    {
        public string Nazwa { get; set; } = string.Empty;
        public string Miasto { get; set; } = string.Empty;
        public List<Pracownik> Pracownicy { get; set; } = new();
        public List<Pokoj> Pokoje { get; set; } = new();
        public List<Rezerwacja> Rezerwacje { get; set; } = new();

        public override string ToString() => $"{Nazwa} ({Miasto})";
    }
}
