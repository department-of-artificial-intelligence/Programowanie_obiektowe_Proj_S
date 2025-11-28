using Project.Models;

namespace Project.Tests.Models
{
    public class SeanceTests
    {
        [Fact]
        public void Seance_Constructor_ValidData_CreatesSeance()
        {
            // Arrange
            var filmId = "film123";
            var auditoriumId = "auditorium123";
            var startTime = DateTime.Now.AddHours(1);
            var price = 100.0m;
            var duration = 120u;

            // Act
            var seance = new Seance(filmId, auditoriumId, startTime, price, duration);

            // Assert
            Assert.Equal(filmId, seance.FilmId);
            Assert.Equal(auditoriumId, seance.AuditoriumId);
            Assert.Equal(startTime, seance.StartTime);
            Assert.Equal(price, seance.Price);
            Assert.Equal(startTime.AddMinutes(duration), seance.EndTime);
        }

        [Fact]
        public void ReserveSeat_ValidSeat_AddsSeat()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var seatId = "A1";
            var capacity = 100u;

            // Act
            var result = seance.ReserveSeat(seatId, capacity);

            // Assert
            Assert.True(result);
            Assert.Contains(seatId, seance.OccupiedSeatIds);
        }

        [Fact]
        public void ReserveSeat_DuplicateSeat_ReturnsFalse()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var seatId = "A1";
            var capacity = 100u;
            seance.ReserveSeat(seatId, capacity);

            // Act
            var result = seance.ReserveSeat(seatId, capacity);

            // Assert
            Assert.False(result);
            Assert.Single(seance.OccupiedSeatIds);
        }

        [Fact]
        public void ReserveSeat_ExceedingCapacity_ReturnsFalse()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var capacity = 1u;
            seance.ReserveSeat("A1", capacity);

            // Act
            var result = seance.ReserveSeat("A2", capacity);

            // Assert
            Assert.False(result);
            Assert.Single(seance.OccupiedSeatIds);
        }

        [Fact]
        public void CancelSeatReservation_ExistingSeat_RemovesSeat()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var seatId = "A1";
            var capacity = 100u;
            seance.ReserveSeat(seatId, capacity);

            // Act
            var result = seance.CancelSeatReservation(seatId);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(seatId, seance.OccupiedSeatIds);
        }

        [Fact]
        public void CancelSeatReservation_NonExistentSeat_ReturnsFalse()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);

            // Act
            var result = seance.CancelSeatReservation("A1");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AvailableSeats_ReturnsCorrectCount()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var capacity = 100u;
            seance.ReserveSeat("A1", capacity);
            seance.ReserveSeat("A2", capacity);

            // Act
            var available = seance.AvailableSeats(capacity);

            // Assert
            Assert.Equal(98, available);
        }

        [Fact]
        public void UpdateTime_ValidTime_UpdatesTime()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var newStartTime = DateTime.Now.AddHours(2);
            var duration = 120u;

            // Act
            seance.UpdateTime(newStartTime, duration);

            // Assert
            Assert.Equal(newStartTime, seance.StartTime);
            Assert.Equal(newStartTime.AddMinutes(duration), seance.EndTime);
        }

        [Fact]
        public void UpdateTime_PastTime_ThrowsException()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var pastTime = DateTime.Now.AddHours(-1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => seance.UpdateTime(pastTime, 120));
        }

        [Fact]
        public void UpdatePrice_ValidPrice_UpdatesPrice()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);
            var newPrice = 150.0m;

            // Act
            seance.UpdatePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, seance.Price);
        }

        [Fact]
        public void UpdatePrice_InvalidPrice_ThrowsException()
        {
            // Arrange
            var seance = new Seance("film123", "auditorium123", DateTime.Now.AddHours(1), 100.0m, 120);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => seance.UpdatePrice(0));
            Assert.Throws<ArgumentException>(() => seance.UpdatePrice(-50));
        }

        [Fact]
        public void ToString_ContainsSeanceInfo()
        {
            // Arrange
            var startTime = DateTime.Now.AddHours(1);
            var seance = new Seance("film123", "auditorium123", startTime, 100.0m, 120);

            // Act
            var result = seance.ToString();

            // Assert
            Assert.Contains(startTime.ToString(), result);
            Assert.Contains("film123", result);
            Assert.Contains("auditorium123", result);
            Assert.Contains("100,0", result);
        }
    }
}