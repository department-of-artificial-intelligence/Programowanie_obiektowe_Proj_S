using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Model
{
    public class Hotel : IHotelElement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nazwa { get; set; } = "";
        public string Miasto { get; set; } = "";
        public string Adres { get; set; } = "";
        public int Gwiazdki { get; set; } = 3;
        public int RokOtwarcia { get; set; } = DateTime.Now.Year;
        public string Nip { get; set; } = "";
        public string Regulamin { get; set; } = "";
        public List<Employees> Pracownicy { get; set; } = new();
        public List<Room> Pokoje { get; set; } = new();
        public List<Reservation> Rezerwacje { get; set; } = new();
        public List<Service> Uslugi { get; set; } = new();
        public List<ServiceReservation> RezerwacjeUslug { get; set; } = new();

        public IEnumerable<Room> PokojeWolne => Pokoje.Where(p => p.Dostepny);
        public IEnumerable<Room> PokojeZajete => Pokoje.Where(p => !p.Dostepny);

        public string Info() => ToString();
        public override string ToString() => $"{Nazwa} | {Miasto} | {Adres} | {Gwiazdki}★ | otw: {RokOtwarcia}";
    }
}
