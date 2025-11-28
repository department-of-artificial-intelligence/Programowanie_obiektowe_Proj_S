using Project.Models;

namespace Project.Tests.Models
{
    public class AuditoriumTests
    {
        [Fact]
        public void Auditorium_Constructor_ValidData_CreatesAuditorium()
        {
            // Act
            var auditorium = new Auditorium("cinema123", "Main Hall", 1, 10, 20);

            // Assert
            Assert.Equal("cinema123", auditorium.CinemaId);
            Assert.Equal("Main Hall", auditorium.Name);
            Assert.Equal(1u, auditorium.RoomNumber);
            Assert.Equal(10u, auditorium.Rows);
            Assert.Equal(20u, auditorium.SeatsPerRow);
            Assert.Equal(200u, auditorium.Capacity);
        }

        [Fact]
        public void AddRating_ValidRating_UpdatesRating()
        {
            // Arrange
            var auditorium = new Auditorium("cinema123", "Hall", 1, 10, 10);

            // Act
            auditorium.AddRating(5);
            auditorium.AddRating(4);

            // Assert
            Assert.Equal(4.5, auditorium.Rating);
            Assert.Equal(2u, auditorium.TotalRatings);
        }

        [Fact]
        public void AddFeature_ValidFeature_AddsFeature()
        {
            // Arrange
            var auditorium = new Auditorium("cinema123", "Hall", 1, 10, 10);

            // Act
            var result = auditorium.AddItem("Dolby Atmos");

            // Assert
            Assert.True(result);
            Assert.Contains("Dolby Atmos", auditorium.Features);
        }
    }
}