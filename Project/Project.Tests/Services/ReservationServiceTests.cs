using Project.Services;

using Project.Models;

namespace Project.Tests.Services
{
    public class ReservationServiceTests
    {
        private readonly List<Reservation> _reservations;

        public ReservationServiceTests()
        {
            _reservations =
            [
                new Reservation("seance1", "John", "Doe", "john@test.com", "+380441234567", "Credit Card"),
                new Reservation("seance2", "Jane", "Smith", "jane@test.com", "+380442345678", "Cash"),
                new Reservation("seance1", "Bob", "Johnson", "bob@test.com", "+380443456789", "Online Payment"),
                new Reservation("seance3", "Alice", "Williams", "alice@test.com", "+380444567890", "Credit Card")
            ];
        }

        [Fact]
        public void FilterReservationsBySeanceId_ValidSeanceId_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationService.FilterReservationsBySeanceId(_reservations, "seance1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, reservation => Assert.Equal("seance1", reservation.SeanceId));
        }

        [Fact]
        public void FilterReservationsBySeanceId_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ReservationService.FilterReservationsBySeanceId(_reservations, "nonexistent-seance");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_ValidMethod_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationService.FilterReservationsByPaymentMethod(_reservations, "Credit Card");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, reservation => Assert.Equal("Credit Card", reservation.PaymentMethod));
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_CaseInsensitive_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationService.FilterReservationsByPaymentMethod(_reservations, "credit card");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_PartialMatch_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationService.FilterReservationsByPaymentMethod(_reservations, "Credit");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ReservationService.FilterReservationsByPaymentMethod(_reservations, "Bitcoin");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void DeleteReservation_ValidReservationId_RemovesReservationAndTickets()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new("seance1", "John", "Doe", "john@test.com", "+380441234567", "Cash")
            };
            var tickets = new List<Ticket>
            {
                new(reservations[0].Id, "cinema1", "auditorium1", "seance1", "film1", "A1", 100.0m,
                    TicketType.Standard)
            };

            var reservationId = reservations[0].Id;

            // Act
            ReservationService.DeleteReservation(reservations, tickets, reservationId);

            // Assert
            Assert.Empty(reservations);
            Assert.Empty(tickets);
        }
    }
}