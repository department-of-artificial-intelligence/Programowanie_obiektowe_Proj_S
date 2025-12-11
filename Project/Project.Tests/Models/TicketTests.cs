using Project.Models;

namespace Project.Tests.Models
{
    public class TicketTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateTicketWithCorrectProperties()
        {
            // Given
            var reservationId = "reservation-123";
            var cinemaId = "cinema-123";
            var auditoriumId = "auditorium-123";
            var seanceId = "seance-123";
            var filmId = "film-123";
            var seatId = "A1";
            var originalPrice = 250.0m;
            var ticketType = TicketType.Standard;

            // When
            var ticket = new Ticket(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, originalPrice, ticketType);

            // Then
            Assert.Equal(reservationId, ticket.ReservationId);
            Assert.Equal(cinemaId, ticket.CinemaId);
            Assert.Equal(auditoriumId, ticket.AuditoriumId);
            Assert.Equal(seanceId, ticket.SeanceId);
            Assert.Equal(filmId, ticket.FilmId);
            Assert.Equal(seatId, ticket.SeatId);
            Assert.Equal(ticketType, ticket.Type);
            Assert.Equal(originalPrice, ticket.OriginalPrice);
            Assert.Equal(originalPrice, ticket.FinalPrice);
            Assert.Equal(0.0m, ticket.Discount);
            Assert.NotEmpty(ticket.Id);
        }

        [Fact]
        public void Constructor_WithStudentTicketType_ShouldApplyDiscount()
        {
            // Given
            var originalPrice = 250.0m;
            var ticketType = TicketType.Student;

            // When
            var ticket = CreateTestTicket(originalPrice, ticketType);

            // Then
            Assert.Equal(originalPrice * 0.8m, ticket.FinalPrice);
            Assert.Equal(originalPrice * 0.2m, ticket.Discount);
        }

        [Fact]
        public void Constructor_WithSeniorTicketType_ShouldApplyDiscount()
        {
            // Given
            var originalPrice = 250.0m;
            var ticketType = TicketType.Senior;

            // When
            var ticket = CreateTestTicket(originalPrice, ticketType);

            // Then
            Assert.Equal(originalPrice * 0.7m, ticket.FinalPrice);
            Assert.Equal(originalPrice * 0.3m, ticket.Discount);
        }

        [Fact]
        public void Constructor_WithChildTicketType_ShouldApplyDiscount()
        {
            // Given
            var originalPrice = 250.0m;
            var ticketType = TicketType.Child;

            // When
            var ticket = CreateTestTicket(originalPrice, ticketType);

            // Then
            Assert.Equal(originalPrice * 0.5m, ticket.FinalPrice);
            Assert.Equal(originalPrice * 0.5m, ticket.Discount);
        }

        [Fact]
        public void Constructor_WithVIPTicketType_ShouldApplyPremium()
        {
            // Given
            var originalPrice = 250.0m;
            var ticketType = TicketType.VIP;

            // When
            var ticket = CreateTestTicket(originalPrice, ticketType);

            // Then
            Assert.Equal(originalPrice * 1.2m, ticket.FinalPrice);
            Assert.Equal(-50.0m, ticket.Discount);
        }

        [Fact]
        public void UpdateTicketType_WithNewType_ShouldUpdatePriceAndDiscount()
        {
            // Given
            var ticket = CreateTestTicket(250.0m, TicketType.Standard);

            // When
            ticket.UpdateTicketType(TicketType.Student);

            // Then
            Assert.Equal(TicketType.Student, ticket.Type);
            Assert.Equal(200.0m, ticket.FinalPrice);
            Assert.Equal(50.0m, ticket.Discount);
        }

        [Fact]
        public void Constructor_WithZeroPrice_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() =>
                new Ticket("reservation-123", "cinema-123", "auditorium-123", "seance-123", "film-123", "A1", 0m, TicketType.Standard));
        }

        [Fact]
        public void Constructor_WithNegativePrice_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() =>
                new Ticket("reservation-123", "cinema-123", "auditorium-123", "seance-123", "film-123", "A1", -100m, TicketType.Standard));
        }

        [Fact]
        public void Constructor_WithEmptyReservationId_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() =>
                new Ticket("", "cinema-123", "auditorium-123", "seance-123", "film-123", "A1", 250m, TicketType.Standard));
        }

        [Fact]
        public void Constructor_WithEmptySeatId_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() =>
                new Ticket("reservation-123", "cinema-123", "auditorium-123", "seance-123", "film-123", "", 250m, TicketType.Standard));
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var ticket = CreateTestTicket(250.0m, TicketType.Standard);
            var initialUpdatedAt = ticket.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            ticket.MarkAsUpdated();

            // Then
            Assert.True(ticket.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var ticket = CreateTestTicket(250.0m, TicketType.Student);

            // When
            var result = ticket.ToString();

            // Then
            Assert.Contains("Ticket:", result);
            Assert.Contains(ticket.Type.ToString(), result);
            Assert.Contains(ticket.SeatId, result);
            Assert.Contains(ticket.FinalPrice.ToString(), result);
            Assert.Contains(ticket.Discount.ToString(), result);
            Assert.Contains(ticket.Id, result);
        }

        [Fact]
        public void Discount_ShouldCalculateCorrectDiscountAmount()
        {
            // Given
            var originalPrice = 100.0m;
            var ticket = CreateTestTicket(originalPrice, TicketType.Student);

            // When
            var discount = ticket.Discount;

            // Then
            Assert.Equal(20.0m, discount);
        }

        [Fact]
        public void UpdateTicketType_ToSameType_ShouldNotChangeFinalPrice()
        {
            // Given
            var ticket = CreateTestTicket(300.0m, TicketType.VIP);
            var originalFinalPrice = ticket.FinalPrice;

            // When
            ticket.UpdateTicketType(TicketType.VIP);

            // Then
            Assert.Equal(originalFinalPrice, ticket.FinalPrice);
        }

        private static Ticket CreateTestTicket(decimal originalPrice, TicketType ticketType)
        {
            return new Ticket("reservation-123", "cinema-123", "auditorium-123",
                "seance-123", "film-123", "A1", originalPrice, ticketType);
        }
    }
}