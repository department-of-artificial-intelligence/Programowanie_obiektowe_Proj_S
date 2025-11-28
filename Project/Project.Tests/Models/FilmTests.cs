using Project.Models;

namespace Project.Tests.Models
{
    public class FilmTests
    {
        [Fact]
        public void Film_Constructor_ValidData_CreatesFilm()
        {
            // Act
            var film = new Film("Inception", "Dream within a dream", 148, "Christopher Nolan",
                "Sci-Fi", false, "poster.jpg", "trailer.mov");

            // Assert
            Assert.Equal("Inception", film.Title);
            Assert.Equal("Dream within a dream", film.Description);
            Assert.Equal(148u, film.DurationMinutes);
            Assert.Equal("Christopher Nolan", film.Director);
            Assert.Equal("Sci-Fi", film.Genre);
            Assert.False(film.HasAgeRestriction);
            Assert.Equal("poster.jpg", film.PosterUrl);
            Assert.Equal("trailer.mov", film.TrailerUrl);
        }

        [Fact]
        public void AddRating_ValidRating_UpdatesRating()
        {
            // Arrange
            var film = new Film("Test Film", "Description", 120, "Director", "Genre",
                false, "poster.jpg", "trailer.mov");

            // Act
            film.AddRating(5);
            film.AddRating(4);

            // Assert
            Assert.Equal(4.5, film.Rating);
            Assert.Equal(2u, film.TotalRatings);
        }

        [Fact]
        public void AddActor_ValidActor_AddsActor()
        {
            // Arrange
            var film = new Film("Test Film", "Description", 120, "Director", "Genre",
                false, "poster.jpg", "trailer.mov");
            var actorId = "actor123";

            // Act
            var result = film.AddItem(actorId);

            // Assert
            Assert.True(result);
            Assert.Contains(actorId, film.ActorIds);
        }

        [Fact]
        public void RemoveActor_ExistingActor_RemovesActor()
        {
            // Arrange
            var film = new Film("Test Film", "Description", 120, "Director", "Genre",
                false, "poster.jpg", "trailer.mov");
            var actorId = "actor123";
            film.AddItem(actorId);

            // Act
            var result = film.RemoveItem(actorId);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(actorId, film.ActorIds);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void AddRating_InvalidRating_ThrowsException(uint invalidRating)
        {
            // Arrange
            var film = new Film("Test Film", "Description", 120, "Director", "Genre",
                false, "poster.jpg", "trailer.mov");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => film.AddRating(invalidRating));
        }
    }
}