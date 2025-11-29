using Project.Services;
using Project.Models;

namespace Project.Tests.Services
{
    public class FilmServiceTests
    {
        private readonly List<Film> _films;

        public FilmServiceTests()
        {
            _films =
            [
                new Film("The Matrix", "Sci-fi action", 136, "Wachowskis", "Sci-Fi", false,
                    "http://example.com/matrix.jpg", "http://example.com/matrix_trailer"),
                new Film("Inception", "Dream within a dream", 148, "Christopher Nolan", "Sci-Fi", false,
                    "http://example.com/inception.jpg", "http://example.com/inception_trailer"),
                new Film("The Godfather", "Crime saga", 175, "Francis Coppola", "Crime", true,
                    "http://example.com/godfather.jpg", "http://example.com/godfather_trailer"),
                new Film("La La Land", "Musical romance", 128, "Damien Chazelle", "Musical", false,
                    "http://example.com/lalaland.jpg", "http://example.com/lalaland_trailer")
            ];

            _films[0].AddRating(5);
            _films[0].AddRating(4);
            _films[1].AddRating(5);
            _films[2].AddRating(5);
            _films[2].AddRating(5);
            _films[3].AddRating(4);
        }

        [Theory]
        [InlineData(120, 150, 3)]
        [InlineData(170, 180, 1)]
        [InlineData(200, 300, 0)]
        public void FilterFilmsByDuration_ValidRange_ReturnsCorrectFilms(uint minDuration, uint maxDuration, int expectedCount)
        {
            // Act
            var result = FilmService.FilterFilmsByDuration(_films, minDuration, maxDuration);

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void FilterFilmsByDuration_MaxDurationNotSpecified_ReturnsAllFilmsAboveMin()
        {
            // Act
            var result = FilmService.FilterFilmsByDuration(_films, 140, uint.MaxValue);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterFilmsByGenre_ValidGenre_ReturnsMatchingFilms()
        {
            // Act
            var result = FilmService.FilterFilmsByGenre(_films, "Sci-Fi");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, film => Assert.Equal("Sci-Fi", film.Genre));
        }

        [Fact]
        public void FilterFilmsByGenre_PartialMatch_ReturnsMatchingFilms()
        {
            // Act
            var result = FilmService.FilterFilmsByGenre(_films, "Mus");

            // Assert
            Assert.Single(result);
            Assert.Contains("Musical", result[0].Genre);
        }

        [Fact]
        public void FilterFilmsByAgeRestriction_True_ReturnsFilmsWithRestriction()
        {
            // Act
            var result = FilmService.FilterFilmsByAgeRestriction(_films, true);

            // Assert
            Assert.Single(result);
            Assert.True(result[0].HasAgeRestriction);
            Assert.Equal("The Godfather", result[0].Title);
        }

        [Fact]
        public void FilterFilmsByAgeRestriction_False_ReturnsFilmsWithoutRestriction()
        {
            // Act
            var result = FilmService.FilterFilmsByAgeRestriction(_films, false);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.All(result, film => Assert.False(film.HasAgeRestriction));
        }

        [Fact]
        public void FilterFilmsByDirector_ValidDirector_ReturnsMatchingFilms()
        {
            // Act
            var result = FilmService.FilterFilmsByDirector(_films, "Nolan");

            // Assert
            Assert.Single(result);
            Assert.Equal("Christopher Nolan", result[0].Director);
        }

        [Fact]
        public void FilterFilmsByTitle_ValidTitle_ReturnsMatchingFilms()
        {
            // Act
            var result = FilmService.FilterFilmsByTitle(_films, "The");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, film => Assert.Contains("The", film.Title));
        }

        [Fact]
        public void SortFilmsByNumberOfActors_ReturnsFilmsInDescendingOrder()
        {
            // Arrange
            _films[0].AddItem("actor1"); 
            _films[0].AddItem("actor2"); 
            _films[1].AddItem("actor3"); 
            _films[2].AddItem("actor4"); 
            _films[2].AddItem("actor5"); 
            _films[2].AddItem("actor6");

            // Act
            var result = FilmService.SortFilmsByNumberOfActors(_films);

            // Assert
            Assert.Equal(3, result[0].Items.Count); 
            Assert.Equal(2, result[1].Items.Count); 
            Assert.Single(result[2].Items);
            Assert.Empty(result[3].Items); 
        }

        [Fact]
        public void FilterFilmsWhereActorIs_ValidActorId_ReturnsFilmsWithActor()
        {
            // Arrange
            var actorId = "test-actor-123";
            _films[0].AddItem(actorId);
            _films[2].AddItem(actorId);

            // Act
            var result = FilmService.FilterFilmsWhereActorIs(_films, actorId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, film => Assert.Contains(actorId, film.Items));
        }

        [Fact]
        public void FilterFilmsWhereActorIs_ActorNotInFilms_ReturnsEmptyList()
        {
            // Act
            var result = FilmService.FilterFilmsWhereActorIs(_films, "non-existing-actor");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void DeleteFilm_ValidFilmId_RemovesFilmAndFromCinemasAndSeances()
        {
            // Arrange
            var films = new List<Film>
            {
                new("Test Film", "Description", 120, "Director", "Action", false,
                    "poster.jpg", "trailer.mov")
            };
            var cinemas = new List<Cinema>
            {
                new("Test Cinema", "Address", "+380441234567", "test@test.com", "Manager")
            };
            var seances = new List<Seance>();
            var reservations = new List<Reservation>();
            var tickets = new List<Ticket>();

            var filmId = films[0].Id;
            cinemas[0].AddItem(filmId);

            // Act
            FilmService.DeleteFilm(films, cinemas, seances, reservations, tickets, filmId);

            // Assert
            Assert.Empty(films);
            Assert.DoesNotContain(filmId, cinemas[0].Items);
        }

        [Fact]
        public void DeleteFilm_FilmInMultipleCinemas_RemovesFromAllCinemas()
        {
            // Arrange
            var films = new List<Film>
            {
                new("Test Film", "Description", 120, "Director", "Action", false, "poster.jpg", "trailer.mov")
            };
            var cinemas = new List<Cinema>
            {
                new("Cinema 1", "Address 1", "+380441111111", "cinema1@test.com", "Manager 1"),
                new("Cinema 2", "Address 2", "+380442222222", "cinema2@test.com", "Manager 2")
            };
            var seances = new List<Seance>();
            var reservations = new List<Reservation>();
            var tickets = new List<Ticket>();

            var filmId = films[0].Id;
            cinemas[0].AddItem(filmId);
            cinemas[1].AddItem(filmId);

            // Act
            FilmService.DeleteFilm(films, cinemas, seances, reservations, tickets, filmId);

            // Assert
            Assert.Empty(films);
            Assert.All(cinemas, cinema => Assert.DoesNotContain(filmId, cinema.Items));
        }

    }
}