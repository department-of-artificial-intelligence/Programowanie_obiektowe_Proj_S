using Project.Logic.SortingFiltering;
using Project.Models;

namespace Project.Tests.Logic
{
    public class ReservationSortingFilteringTests
    {
        private readonly List<Reservation> _reservations;

        public ReservationSortingFilteringTests()
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
            var result = ReservationSortingFiltering.FilterReservationsBySeanceId(_reservations, "seance1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, reservation => Assert.Equal("seance1", reservation.SeanceId));
        }

        [Fact]
        public void FilterReservationsBySeanceId_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ReservationSortingFiltering.FilterReservationsBySeanceId(_reservations, "nonexistent-seance");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_ValidMethod_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationSortingFiltering.FilterReservationsByPaymentMethod(_reservations, "Credit Card");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, reservation => Assert.Equal("Credit Card", reservation.PaymentMethod));
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_CaseInsensitive_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationSortingFiltering.FilterReservationsByPaymentMethod(_reservations, "credit card");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_PartialMatch_ReturnsMatchingReservations()
        {
            // Act
            var result = ReservationSortingFiltering.FilterReservationsByPaymentMethod(_reservations, "Credit");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterReservationsByPaymentMethod_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ReservationSortingFiltering.FilterReservationsByPaymentMethod(_reservations, "Bitcoin");

            // Assert
            Assert.Empty(result);
        }
    }
}