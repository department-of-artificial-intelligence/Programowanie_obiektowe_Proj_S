using Project.Logic.SortingFiltering;
using Project.Models;

namespace Project.Tests.Logic
{
    public class TicketSortingFilteringTests
    {
        private readonly List<Ticket> _tickets;

        public TicketSortingFilteringTests()
        {
            _tickets =
            [
                new Ticket("reservation1", "cinema1", "auditorium1", "seance1", "film1", "A1", 100.0m, TicketType.Standard),
                new Ticket("reservation1", "cinema1", "auditorium1", "seance1", "film1", "A2", 100.0m, TicketType.Student),
                new Ticket("reservation2", "cinema2", "auditorium2", "seance2", "film2", "B1", 150.0m, TicketType.VIP),
                new Ticket("reservation3", "cinema1", "auditorium3", "seance3", "film3", "C1", 120.0m, TicketType.Senior),
                new Ticket("reservation4", "cinema3", "auditorium4", "seance4", "film1", "D1", 100.0m, TicketType.Child)
            ];
        }

        [Fact]
        public void FilterTicketsByReservationId_ValidId_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByReservationId(_tickets, "reservation1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, ticket => Assert.Equal("reservation1", ticket.ReservationId));
        }

        [Fact]
        public void FilterTicketsByCinemaId_ValidId_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByCinemaId(_tickets, "cinema1");

            // Assert
            Assert.Equal(3, result.Count);
            Assert.All(result, ticket => Assert.Equal("cinema1", ticket.CinemaId));
        }

        [Fact]
        public void FilterTicketsBySeanceId_ValidId_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsBySeanceId(_tickets, "seance1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, ticket => Assert.Equal("seance1", ticket.SeanceId));
        }

        [Fact]
        public void FilterTicketsByFilmId_ValidId_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByFilmId(_tickets, "film1");

            // Assert
            Assert.Equal(3, result.Count);
            Assert.All(result, ticket => Assert.Equal("film1", ticket.FilmId));
        }

        [Fact]
        public void FilterTicketsByAuditoriumId_ValidId_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByAuditoriumId(_tickets, "auditorium1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, ticket => Assert.Equal("auditorium1", ticket.AuditoriumId));
        }

        [Fact]
        public void FilterTicketsByTicketType_ValidType_ReturnsMatchingTickets()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByTicketType(_tickets, TicketType.Standard);

            // Assert
            Assert.Single(result);
            Assert.All(result, ticket => Assert.Equal(TicketType.Standard, ticket.Type));
        }

        [Fact]
        public void SortTicketsByFinalPrice_ReturnsTicketsInDescendingOrder()
        {
            // Act
            var result = TicketSortingFiltering.SortTicketsByFinalPrice(_tickets);

            // Assert
            Assert.Equal(180.0m, result[0].FinalPrice);
            Assert.Equal(100.0m, result[1].FinalPrice);
            Assert.Equal(84.0m, result[2].FinalPrice);
            Assert.Equal(80.0m, result[3].FinalPrice);
            Assert.Equal(50.0m, result[4].FinalPrice);
        }

        [Fact]
        public void FilterTicketsByReservationId_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = TicketSortingFiltering.FilterTicketsByReservationId(_tickets, "nonexistent-reservation");

            // Assert
            Assert.Empty(result);
        }
    }
}