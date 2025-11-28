using Project.Models.Common;

namespace Project.Models
{
    public enum TicketType
    {
        Standard,
        Student,
        Senior,
        Child,
        VIP
    }

    public class Ticket : Base
    {
        public string ReservationId { get; private set; }
        public string CinemaId { get; private set; }
        public string AuditoriumId { get; private set; }
        public string SeanceId { get; private set; }
        public string FilmId { get; private set; }
        public string SeatId { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public decimal FinalPrice { get; private set; }
        public TicketType Type { get; private set; }
        public decimal Discount => OriginalPrice - FinalPrice;

        public Ticket(string reservationId, string cinemaId, string auditoriumId,
            string seanceId, string filmId, string seatId, decimal originalPrice,
            TicketType type) : base()
        {
            ValidateTicketData(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, originalPrice);

            ReservationId = reservationId;
            CinemaId = cinemaId;
            AuditoriumId = auditoriumId;
            SeanceId = seanceId;
            FilmId = filmId;
            SeatId = seatId;
            OriginalPrice = originalPrice;
            Type = type;
            FinalPrice = CalculateFinalPrice(originalPrice, type);
        }

        public Ticket(string id, string reservationId, string cinemaId, string auditoriumId,
            string seanceId, string filmId, string seatId, decimal originalPrice,
            TicketType type, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            ValidateTicketData(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, originalPrice);

            ReservationId = reservationId;
            CinemaId = cinemaId;
            AuditoriumId = auditoriumId;
            SeanceId = seanceId;
            FilmId = filmId;
            SeatId = seatId;
            OriginalPrice = originalPrice;
            Type = type;
            FinalPrice = CalculateFinalPrice(originalPrice, type);
        }

        private static void ValidateTicketData(string reservationId, string cinemaId, string auditoriumId,
            string seanceId, string filmId, string seatId, decimal originalPrice)
        {
            if (string.IsNullOrWhiteSpace(reservationId))
                throw new ArgumentException("Reservation ID is required", nameof(reservationId));

            if (string.IsNullOrWhiteSpace(cinemaId))
                throw new ArgumentException("Cinema ID is required", nameof(cinemaId));

            if (string.IsNullOrWhiteSpace(auditoriumId))
                throw new ArgumentException("Auditorium ID is required", nameof(auditoriumId));

            if (string.IsNullOrWhiteSpace(seanceId))
                throw new ArgumentException("Seance ID is required", nameof(seanceId));

            if (string.IsNullOrWhiteSpace(filmId))
                throw new ArgumentException("Film ID is required", nameof(filmId));

            if (string.IsNullOrWhiteSpace(seatId))
                throw new ArgumentException("Seat ID is required", nameof(seatId));

            if (originalPrice <= 0)
                throw new ArgumentException("Price must be greater than 0", nameof(originalPrice));
        }

        private static decimal CalculateFinalPrice(decimal originalPrice, TicketType type)
        {
            return type switch
            {
                TicketType.Student => originalPrice * 0.8m,
                TicketType.Senior => originalPrice * 0.7m,
                TicketType.Child => originalPrice * 0.5m,
                TicketType.VIP => originalPrice * 1.2m,
                _ => originalPrice,
            };
        }

        public void UpdateTicketType(TicketType newType)
        {
            Type = newType;
            FinalPrice = CalculateFinalPrice(OriginalPrice, newType);

            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Ticket: {Type}\n" +
                   $"Seat: {SeatId} | Price: {FinalPrice}\n" +
                   $"Film: {FilmId} | Auditorium: {AuditoriumId}\n" +
                   $"Discount: {(Discount > 0 ? $"{Discount}" : "None")}\n" +
                   $"ID: {Id}";
        }
    }
}