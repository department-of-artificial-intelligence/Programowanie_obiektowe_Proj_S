using System;

namespace Project.Model
{
    public class ServiceReservation : IHotelElement
    {
        public int Id { get; set; }
        public Guest Gosc { get; set; } = new();
        public Service Usluga { get; set; } = new();
        public DateTime Data { get; set; }
        public int? PowiazanyNumerPokoju { get; set; }

        public string Info() => ToString();
        public override string ToString() => $"Rezerwacja usługi #{Id}: {Usluga.Nazwa} dla {Gosc.PelneDane()} {(PowiazanyNumerPokoju.HasValue ? $"(pokój {PowiazanyNumerPokoju})" : "")} - {Data:dd-MM-yyyy} - {Usluga.Cena:C}";
    }
}