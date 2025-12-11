using Project.Models;

namespace Project.Tests.Models
{
    public class SeanceTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateSeanceWithCorrectProperties()
        {
            // Given
            var filmId = "film-123";
            var auditoriumId = "auditorium-123";
            var startTime = DateTime.Now.AddHours(1);
            var price = 250.0m;
            var durationMinutes = 120u;

            // When
            var seance = new Seance(filmId, auditoriumId, startTime, price, durationMinutes);

            // Then
            Assert.Equal(filmId, seance.FilmId);
            Assert.Equal(auditoriumId, seance.AuditoriumId);
            Assert.Equal(startTime, seance.StartTime);
            Assert.Equal(price, seance.Price);
            Assert.Equal(durationMinutes, seance.FilmDurationMinutes);
            Assert.Equal(startTime.AddMinutes(durationMinutes), seance.EndTime);
            Assert.Empty(seance.OccupiedSeatIds);
            Assert.NotEmpty(seance.Id);
        }

        [Fact]
        public void Constructor_WithPastStartTime_ShouldThrowArgumentException()
        {
            // Given
            var pastTime = DateTime.Now.AddHours(-1);

            // When & Then
            Assert.Throws<ArgumentException>(() => new Seance("film-123", "auditorium-123", pastTime, 250.0m, 120));
        }

        [Fact]
        public void Constructor_WithZeroPrice_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Seance("film-123", "auditorium-123", DateTime.Now.AddHours(1), 0m, 120));
        }

        [Fact]
        public void Constructor_WithZeroDuration_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Seance("film-123", "auditorium-123", DateTime.Now.AddHours(1), 250.0m, 0));
        }

        [Fact]
        public void ReserveSeat_WithValidSeatId_ShouldAddSeatToOccupiedList()
        {
            // Given
            var seance = CreateTestSeance();
            var seatId = "A1";
            var auditoriumCapacity = 100u;

            // When
            var result = seance.ReserveSeat(seatId, auditoriumCapacity);

            // Then
            Assert.True(result);
            Assert.Single(seance.OccupiedSeatIds);
            Assert.Contains(seatId, seance.OccupiedSeatIds);
        }

        [Fact]
        public void ReserveSeat_WithDuplicateSeatId_ShouldNotAddSeat()
        {
            // Given
            var seance = CreateTestSeance();
            var seatId = "A1";
            var auditoriumCapacity = 100u;
            seance.ReserveSeat(seatId, auditoriumCapacity);

            // When
            var result = seance.ReserveSeat(seatId, auditoriumCapacity);

            // Then
            Assert.False(result);
            Assert.Single(seance.OccupiedSeatIds);
        }

        [Fact]
        public void ReserveSeat_WhenCapacityReached_ShouldNotAddSeat()
        {
            // Given
            var seance = CreateTestSeance();
            var auditoriumCapacity = 2u;

            // Fill capacity
            seance.ReserveSeat("A1", auditoriumCapacity);
            seance.ReserveSeat("A2", auditoriumCapacity);

            // When
            var result = seance.ReserveSeat("A3", auditoriumCapacity);

            // Then
            Assert.False(result);
            Assert.Equal(2, seance.OccupiedSeatIds.Count);
        }

        [Fact]
        public void CancelSeatReservation_WithExistingSeat_ShouldRemoveSeat()
        {
            // Given
            var seance = CreateTestSeance();
            var seatId = "A1";
            var auditoriumCapacity = 100u;
            seance.ReserveSeat(seatId, auditoriumCapacity);

            // When
            var result = seance.CancelSeatReservation(seatId);

            // Then
            Assert.True(result);
            Assert.Empty(seance.OccupiedSeatIds);
        }

        [Fact]
        public void CancelSeatReservation_WithNonExistingSeat_ShouldReturnFalse()
        {
            // Given
            var seance = CreateTestSeance();

            // When
            var result = seance.CancelSeatReservation("NonExistingSeat");

            // Then
            Assert.False(result);
        }

        [Fact]
        public void AvailableSeats_ShouldReturnCorrectAvailableSeatsCount()
        {
            // Given
            var seance = CreateTestSeance();
            var auditoriumCapacity = 100u;
            seance.ReserveSeat("A1", auditoriumCapacity);
            seance.ReserveSeat("A2", auditoriumCapacity);
            seance.ReserveSeat("B1", auditoriumCapacity);

            // When
            var availableSeats = seance.AvailableSeats(auditoriumCapacity);

            // Then
            Assert.Equal(97, availableSeats);
        }

        [Fact]
        public void UpdateTime_WithValidNewStartTime_ShouldUpdateStartAndEndTime()
        {
            // Given
            var seance = CreateTestSeance();
            var newStartTime = DateTime.Now.AddHours(3);
            var durationMinutes = 120u;

            // When
            seance.UpdateTime(newStartTime, durationMinutes);

            // Then
            Assert.Equal(newStartTime, seance.StartTime);
            Assert.Equal(newStartTime.AddMinutes(durationMinutes), seance.EndTime);
        }

        [Fact]
        public void UpdateTime_WithPastTime_ShouldThrowArgumentException()
        {
            // Given
            var seance = CreateTestSeance();
            var pastTime = DateTime.Now.AddHours(-1);

            // When & Then
            Assert.Throws<ArgumentException>(() => seance.UpdateTime(pastTime, 120));
        }

        [Fact]
        public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
        {
            // Given
            var seance = CreateTestSeance();
            var newPrice = 300.0m;

            // When
            seance.UpdatePrice(newPrice);

            // Then
            Assert.Equal(newPrice, seance.Price);
        }

        [Fact]
        public void UpdatePrice_WithZeroPrice_ShouldThrowArgumentException()
        {
            // Given
            var seance = CreateTestSeance();

            // When & Then
            Assert.Throws<ArgumentException>(() => seance.UpdatePrice(0m));
        }

        [Fact]
        public void UpdatePrice_WithNegativePrice_ShouldThrowArgumentException()
        {
            // Given
            var seance = CreateTestSeance();

            // When & Then
            Assert.Throws<ArgumentException>(() => seance.UpdatePrice(-50.0m));
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var seance = CreateTestSeance();

            // When
            var result = seance.ToString();

            // Then
            Assert.Contains("Seance:", result);
            Assert.Contains(seance.StartTime.ToString(), result);
            Assert.Contains(seance.EndTime.ToString(), result);
            Assert.Contains(seance.Price.ToString(), result);
            Assert.Contains(seance.FilmId, result);
            Assert.Contains(seance.AuditoriumId, result);
            Assert.Contains(seance.Id, result);
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var seance = CreateTestSeance();
            var initialUpdatedAt = seance.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            seance.MarkAsUpdated();

            // Then
            Assert.True(seance.UpdatedAt > initialUpdatedAt);
        }

        private static Seance CreateTestSeance()
        {
            return new Seance("film-123", "auditorium-123", DateTime.Now.AddHours(1), 250.0m, 120);
        }
    }
}