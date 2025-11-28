using Project.Models;

namespace Project.Tests.Models
{
    public class CinemaTests
    {
        [Fact]
        public void Cinema_Constructor_ValidData_CreatesCinema()
        {
            // Act
            var cinema = new Cinema("Multiplex", "123 Main St", "+380441234567",
                "info@cinema.ua", "John Smith");

            // Assert
            Assert.Equal("Multiplex", cinema.Name);
            Assert.Equal("123 Main St", cinema.Address);
            Assert.Equal("+380441234567", cinema.ContactPhone);
            Assert.Equal("info@cinema.ua", cinema.ContactEmail);
            Assert.Equal("John Smith", cinema.ManagerName);
        }

        [Fact]
        public void AddRating_ValidRating_UpdatesRating()
        {
            // Arrange
            var cinema = new Cinema("Test Cinema", "Address", "Phone", "Email", "Manager");

            // Act
            cinema.AddRating(5);
            cinema.AddRating(4);

            // Assert
            Assert.Equal(4.5, cinema.Rating);
            Assert.Equal(2u, cinema.TotalRatings);
        }

        [Fact]
        public void AddFilm_ValidFilm_AddsFilm()
        {
            // Arrange
            var cinema = new Cinema("Test Cinema", "Address", "Phone", "Email", "Manager");
            var filmId = "film123";

            // Act
            var result = cinema.AddItem(filmId);

            // Assert
            Assert.True(result);
            Assert.Contains(filmId, cinema.AvailableFilmIds);
        }
    }
}