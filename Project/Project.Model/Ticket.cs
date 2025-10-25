using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.Entities;
using Project.Utils;

namespace Project.Models
{
    public class Ticket : BaseEntity
    {
        public string Id { get; private set; }
        public string ReservationId { get; private set; }
        public string CinemaId { get; private set; }
        public string AuditoriumId { get; private set; }
        public string SeanceId { get; private set; }
        public string FilmId { get; private set; }
        public string SeatId { get; private set; }
        public double Price { get; private set; }
        public string TicketType { get; private set; }

        public Ticket()
        {
            throw new NotImplementedException("Cannot create an epty Ticket obj");
        }

        public Ticket
        (
            string reservationId,
            string cinemaId,
            string auditoriumId,
            string seanceId,
            string filmId,
            string seatId,
            string ticketType,
            Seance seance
        )
        {
            this.Id = IdHandler.CreateId();
            this.ReservationId = reservationId;
            this.CinemaId = cinemaId;
            this.AuditoriumId = auditoriumId;
            this.SeanceId = seanceId;
            this.FilmId = filmId;
            this.SeatId = seatId;
            this.Price = CalculateTicketDiscount(seance.Price, ticketType);
            this.TicketType = ticketType;
        }

        public Ticket
        (
            string id,
            string reservationId,
            string cinemaId,
            string auditoriumId,
            string seanceId,
            string filmId,
            string seatId,
            double price,
            string ticketType,
            DateTime updatedAt,
            DateTime createdAt
        )
        {
            this.Id = id;
            this.ReservationId = reservationId;
            this.CinemaId = cinemaId;
            this.AuditoriumId = auditoriumId;
            this.SeanceId = seanceId;
            this.FilmId = filmId;
            this.SeatId = seatId;
            this.Price = CalculateTicketDiscount(price, ticketType);
            this.TicketType = ticketType;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        private static double CalculateTicketDiscount(double fullPrice, string ticketType)
        {
            return ticketType.ToLower() switch
            {
                "student" => fullPrice * 0.8,
                "senior" => fullPrice * 0.7,
                "child" => fullPrice * 0.5,
                "vip" => 0,
                _ => fullPrice,
            };
        }

        public override string ToString()
        {
            return $"Ticket Id: {this.Id} \n" +
                   $"Reservation Id: {this.ReservationId} \n" +
                   $"Cinema Id: {this.CinemaId} \n" +
                   $"Auditorium Id: {this.AuditoriumId} \n" +
                   $"Seance Id: {this.SeanceId} \n" +
                   $"Film Id: {this.FilmId} \n" +
                   $"Seat Id: {this.SeatId} \n" +
                   $"Price: {this.Price} \n" +
                   $"Ticket type: {this.TicketType} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }

    }
}
