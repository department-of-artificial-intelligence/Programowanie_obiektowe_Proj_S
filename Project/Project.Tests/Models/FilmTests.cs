using Project.Models;

namespace Project.Tests.Models
{
    public class FilmTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateFilmWithCorrectProperties()
        {
            // Given
            var title = "Inception";
            var description = "A mind-bending thriller";
            var duration = 148u;
            var director = "Christopher Nolan";
            var genre = "Sci-Fi";
            var ageRestriction = false;
            var posterUrl = "http://example.com/poster.jpg";
            var trailerUrl = "http://example.com/trailer.mp4";

            // When
            var film = new Film(title, description, duration, director, genre, ageRestriction, posterUrl, trailerUrl);

            // Then
            Assert.Equal(title, film.Title);
            Assert.Equal(description, film.Description);
            Assert.Equal(duration, film.DurationMinutes);
            Assert.Equal(director, film.Director);
            Assert.Equal(genre, film.Genre);
            Assert.Equal(ageRestriction, film.HasAgeRestriction);
            Assert.Equal(posterUrl, film.PosterUrl);
            Assert.Equal(trailerUrl, film.TrailerUrl);
            Assert.Empty(film.ActorIds);
            Assert.Equal(0.0, film.Rating);
            Assert.Equal(0u, film.TotalRatings);
            Assert.NotEmpty(film.Id);
        }

        [Fact]
        public void AddItem_WithNewActorId_ShouldAddActorId()
        {
            // Given
            var film = CreateTestFilm();
            var actorId = "actor-123";

            // When
            var result = film.AddItem(actorId);

            // Then
            Assert.True(result);
            Assert.Single(film.ActorIds);
            Assert.Contains(actorId, film.ActorIds);
        }

        [Fact]
        public void AddItem_WithDuplicateActorId_ShouldNotAddActorId()
        {
            // Given
            var film = CreateTestFilm();
            var actorId = "actor-123";
            film.AddItem(actorId);

            // When
            var result = film.AddItem(actorId);

            // Then
            Assert.False(result);
            Assert.Single(film.ActorIds);
        }

        [Fact]
        public void AddItem_WhenMaxActorsReached_ShouldNotAddActorId()
        {
            // Given
            var film = CreateTestFilm();
            for (int i = 1; i <= 10; i++)
            {
                film.AddItem($"actor-{i}");
            }

            // When
            var result = film.AddItem("actor-11");

            // Then
            Assert.False(result);
            Assert.Equal(10, film.ActorIds.Count);
        }

        [Fact]
        public void RemoveItem_WithExistingActorId_ShouldRemoveActorId()
        {
            // Given
            var film = CreateTestFilm();
            var actorId = "actor-123";
            film.AddItem(actorId);

            // When
            var result = film.RemoveItem(actorId);

            // Then
            Assert.True(result);
            Assert.Empty(film.ActorIds);
        }

        [Fact]
        public void RemoveItem_WithNonExistingActorId_ShouldReturnFalse()
        {
            // Given
            var film = CreateTestFilm();

            // When
            var result = film.RemoveItem("non-existing-actor");

            // Then
            Assert.False(result);
        }

        [Fact]
        public void AddRating_WithValidRating_ShouldUpdateRatingAndTotalRatings()
        {
            // Given
            var film = CreateTestFilm();

            // When
            film.AddRating(5);
            film.AddRating(4);
            film.AddRating(3);

            // Then
            Assert.Equal(3u, film.TotalRatings);
            Assert.Equal(4.0, film.Rating);
        }

        [Fact]
        public void AddRating_WithInvalidRating_ShouldThrowArgumentException()
        {
            // Given
            var film = CreateTestFilm();

            // When & Then
            Assert.Throws<ArgumentException>(() => film.AddRating(0));
            Assert.Throws<ArgumentException>(() => film.AddRating(6));
        }

        [Fact]
        public void UpdateInfo_WithValidData_ShouldUpdateProperties()
        {
            // Given
            var film = CreateTestFilm();
            var newTitle = "Updated Film";
            var newDescription = "Updated description";
            var newDuration = 120u;
            var newDirector = "New Director";
            var newGenre = "Drama";
            var newAgeRestriction = true;
            var newPosterUrl = "http://example.com/new.jpg";
            var newTrailerUrl = "http://example.com/new.mp4";

            // When
            film.UpdateInfo(newTitle, newDescription, newDuration, newDirector, newGenre, newAgeRestriction, newPosterUrl, newTrailerUrl);

            // Then
            Assert.Equal(newTitle, film.Title);
            Assert.Equal(newDescription, film.Description);
            Assert.Equal(newDuration, film.DurationMinutes);
            Assert.Equal(newDirector, film.Director);
            Assert.Equal(newGenre, film.Genre);
            Assert.Equal(newAgeRestriction, film.HasAgeRestriction);
            Assert.Equal(newPosterUrl, film.PosterUrl);
            Assert.Equal(newTrailerUrl, film.TrailerUrl);
        }

        [Fact]
        public void Constructor_WithEmptyTitle_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Film("", "Description", 100, "Director", "Genre", false, "poster.jpg", "trailer.mp4"));
        }

        [Fact]
        public void Constructor_WithZeroDuration_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Film("Title", "Description", 0, "Director", "Genre", false, "poster.jpg", "trailer.mp4"));
        }

        [Fact]
        public void GetItemsAsString_WithMultipleActorIds_ShouldReturnCommaSeparatedString()
        {
            // Given
            var film = CreateTestFilm();
            film.AddItem("actor-1");
            film.AddItem("actor-2");
            film.AddItem("actor-3");

            // When
            var result = film.GetItemsAsString();

            // Then
            Assert.Equal("actor-1, actor-2, actor-3", result);
        }

        [Fact]
        public void Items_ShouldReturnActorIdsAsReadOnlyList()
        {
            // Given
            var film = CreateTestFilm();
            film.AddItem("actor-1");
            film.AddItem("actor-2");

            // When
            var items = film.Items;

            // Then
            Assert.Equal(2, items.Count);
            Assert.Contains("actor-1", items);
            Assert.Contains("actor-2", items);
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var film = CreateTestFilm();
            var initialUpdatedAt = film.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            film.MarkAsUpdated();

            // Then
            Assert.True(film.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var film = CreateTestFilm();
            film.AddItem("actor-123");
            film.AddRating(5);

            // When
            var result = film.ToString();

            // Then
            Assert.Contains("Film:", result);
            Assert.Contains(film.Title, result);
            Assert.Contains(film.Director, result);
            Assert.Contains(film.Genre, result);
            Assert.Contains(film.Rating.ToString(), result);
            Assert.Contains(film.Id, result);
        }

        private static Film CreateTestFilm()
        {
            return new Film("Test Film", "Test Description", 120, "Test Director", "Test Genre", 
                            false, "http://example.com/poster.jpg", "http://example.com/trailer.mp4");
        }
    }
}