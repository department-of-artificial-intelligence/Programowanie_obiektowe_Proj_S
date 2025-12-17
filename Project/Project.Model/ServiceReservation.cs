using System;

namespace Project.Model
{
    public class ServiceReservation
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public DateTime Data { get; set; }
        public decimal CenaWChwiliZakupu { get; set; }

        public override string ToString() => $"{Service?.Nazwa} ({Data:DD-MM}) - {CenaWChwiliZakupu:C}";
    }
}
