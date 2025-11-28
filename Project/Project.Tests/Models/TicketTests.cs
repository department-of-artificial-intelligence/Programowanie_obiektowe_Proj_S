using Project.Models;

namespace Project.Tests.Models
{
    public class TicketTests
    {
        [Fact]
        public void Ticket_Constructor_ValidData_CreatesTicket()
        {
            // Act
            var ticket = new Ticket("reservation123", "cinema123", "auditorium123",
                "seance123", "film123", "A1", 100.0m, TicketType.Standard);

            // Assert
            Assert.Equal("reservation123", ticket.ReservationId);
            Assert.Equal("cinema123", ticket.CinemaId);
            Assert.Equal("auditorium123", ticket.AuditoriumId);
            Assert.Equal("seance123", ticket.SeanceId);
            Assert.Equal("film123", ticket.FilmId);
            Assert.Equal("A1", ticket.SeatId);
            Assert.Equal(100.0m, ticket.OriginalPrice);
            Assert.Equal(100.0m, ticket.FinalPrice);
            Assert.Equal(TicketType.Standard, ticket.Type);
            Assert.Equal(0.0m, ticket.Discount);
        }

        [Fact]
        public void Ticket_StudentType_AppliesDiscount()
        {
            // Act
            var ticket = new Ticket("reservation123", "cinema123", "auditorium123",
                "seance123", "film123", "A1", 100.0m, TicketType.Student);

            // Assert
            Assert.Equal(80.0m, ticket.FinalPrice);
            Assert.Equal(20.0m, ticket.Discount);
        }

        [Fact]
        public void UpdateTicketType_ChangesTypeAndRecalculatesPrice()
        {
            // Arrange
            var ticket = new Ticket("reservation123", "cinema123", "auditorium123",
                "seance123", "film123", "A1", 100.0m, TicketType.Standard);

            // Act
            ticket.UpdateTicketType(TicketType.VIP);

            // Assert
            Assert.Equal(TicketType.VIP, ticket.Type);
            Assert.Equal(120.0m, ticket.FinalPrice);
        }
    }
}