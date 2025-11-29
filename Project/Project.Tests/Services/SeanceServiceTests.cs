using Project.Services;
using Project.Models;

namespace Project.Tests.Services
{
    public class SeanceServiceTests
    {
        private readonly List<Seance> _seances;
        private readonly List<Auditorium> _auditoriums;

        public SeanceServiceTests()
        {
            _auditoriums =
            [
                new Auditorium("cinema1", "Main Hall", 1, 10, 20),  
                new Auditorium("cinema1", "VIP Hall", 2, 8, 15),   
                new Auditorium("cinema2", "Big Hall", 1, 12, 18)   
            ];

            var baseTime = DateTime.Now.AddDays(1);

            _seances =
            [
                CreateSeance("film1", _auditoriums[0].Id, baseTime.AddHours(2), 250.0m, 120),
                CreateSeance("film2", _auditoriums[1].Id, baseTime.AddHours(4), 280.0m, 150),
                CreateSeance("film1", _auditoriums[0].Id, baseTime.AddHours(1), 200.0m, 120),
                CreateSeance("film3", _auditoriums[2].Id, baseTime.AddHours(3), 350.0m, 180)
            ];

            AddOccupiedSeats(_seances[0], ["A1", "A2", "A3"]);
            AddOccupiedSeats(_seances[1], ["B1", "B2"]);
            AddOccupiedSeats(_seances[3], ["C1", "C2", "C3", "C4"]);
        }

        private static Seance CreateSeance(string filmId, string auditoriumId, DateTime startTime, decimal price, uint duration)
        {
            try
            {
                return new Seance(filmId, auditoriumId, startTime, price, duration);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create seance: {ex.Message}", ex);
            }
        }

        private void AddOccupiedSeats(Seance seance, List<string> seatIds)
        {
            foreach (var seatId in seatIds)
            {
                var auditorium = _auditoriums.Find(a => a.Id == seance.AuditoriumId);
                if (auditorium != null)
                {
                    seance.ReserveSeat(seatId, auditorium.Capacity);
                }
            }
        }

        [Fact]
        public void FilterSeancesByFilmId_ValidFilmId_ReturnsMatchingSeances()
        {
            // Act
            var result = SeanceService.FilterSeancesByFilmId(_seances, "film1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, seance => Assert.Equal("film1", seance.FilmId));
        }

        [Fact]
        public void FilterSeancesByFilmId_NonExistentFilmId_ReturnsEmptyList()
        {
            // Act
            var result = SeanceService.FilterSeancesByFilmId(_seances, "nonexistent");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FilterSeancesByFilmId_NullOrEmptyInput_ReturnsEmptyList()
        {
            // Act & Assert
            Assert.Empty(SeanceService.FilterSeancesByFilmId(null!, "film1"));
            Assert.Empty(SeanceService.FilterSeancesByFilmId([], "film1"));
            Assert.Empty(SeanceService.FilterSeancesByFilmId(_seances, null!));
            Assert.Empty(SeanceService.FilterSeancesByFilmId(_seances, ""));
        }

        [Fact]
        public void FilterSeancesByAuditoriumId_ValidAuditoriumId_ReturnsMatchingSeances()
        {
            // Arrange
            var auditoriumId = _auditoriums[0].Id;

            // Act
            var result = SeanceService.FilterSeancesByAuditoriumId(_seances, auditoriumId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, seance => Assert.Equal(auditoriumId, seance.AuditoriumId));
        }

        [Fact]
        public void SortSeancesByStartTime_ReturnsSeancesInAscendingOrder()
        {
            // Act
            var result = SeanceService.SortSeancesByStartTime(_seances);

            // Assert
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].StartTime <= result[i + 1].StartTime,
                    $"Seances not sorted correctly: {result[i].StartTime} should be before {result[i + 1].StartTime}");
            }
        }

        [Fact]
        public void SortSeancesByPrice_ReturnsSeancesInAscendingOrder()
        {
            // Act
            var result = SeanceService.SortSeancesByPrice(_seances);

            // Assert
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].Price <= result[i + 1].Price,
                    $"Seances not sorted by price: {result[i].Price} should be <= {result[i + 1].Price}");
            }
        }

        [Fact]
        public void SortSeancesByOccupiedSeats_WithValidData_ReturnsSeancesInDescendingOccupancyRate()
        {
            // Act
            var result = SeanceService.SortSeancesByOccupiedSeats(_seances, _auditoriums);

            // Assert
            for (int i = 0; i < result.Count - 1; i++)
            {
                var auditorium1 = _auditoriums.First(a => a.Id == result[i].AuditoriumId);
                var auditorium2 = _auditoriums.First(a => a.Id == result[i + 1].AuditoriumId);

                double occupancy1 = (double)result[i].OccupiedSeatIds.Count / auditorium1.Capacity;
                double occupancy2 = (double)result[i + 1].OccupiedSeatIds.Count / auditorium2.Capacity;

                Assert.True(occupancy1 >= occupancy2,
                    $"Seance {i} occupancy {occupancy1} should be >= {occupancy2}");
            }
        }

        [Fact]
        public void SortSeancesByOccupiedSeats_WithMissingAuditorium_StillReturnsResults()
        {
            // Arrange
            var seanceWithUnknownAuditorium = CreateSeance("film1", "unknown-auditorium",
                DateTime.Now.AddHours(5), 100.0m, 120);

            var testSeances = new List<Seance> { seanceWithUnknownAuditorium };

            // Act
            var result = SeanceService.SortSeancesByOccupiedSeats(testSeances, _auditoriums);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void SortSeancesByOccupiedSeats_EmptyInput_ReturnsEmptyList()
        {
            // Act & Assert
            Assert.Empty(SeanceService.SortSeancesByOccupiedSeats([], _auditoriums));
            Assert.Empty(SeanceService.SortSeancesByOccupiedSeats(null!, _auditoriums));
        }

        [Fact]
        public void DeleteSeance_ValidSeanceId_RemovesSeanceAndReservations()
        {
            // Arrange
            var seances = new List<Seance>
            {
                new("film1", "auditorium1", DateTime.Now.AddHours(1), 100.0m, 120)
            };
            var reservations = new List<Reservation>
            {
                new(seances[0].Id, "John", "Doe", "john@test.com", "+380441234567", "Cash")
            };
            var tickets = new List<Ticket>();

            var seanceId = seances[0].Id;

            // Act
            SeanceService.DeleteSeance(seances, reservations, tickets, seanceId);

            // Assert
            Assert.Empty(seances);
            Assert.Empty(reservations);
        }
    }
}