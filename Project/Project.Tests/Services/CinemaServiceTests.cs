using Project.Services;
using Project.Models;

namespace Project.Tests.Logic
{
    public class CinemaServiceTests
    {
        private readonly List<Cinema> _cinemas;

        public CinemaServiceTests()
        {
            _cinemas =
            [
                new Cinema("Multiplex Cinema", "123 Main St", "+380441234567",
                    "info@multiplex.ua", "Ivan Petrenko"),
                new Cinema("Star Cinema", "456 Central Ave", "+380441234568",
                    "info@starcinema.ua", "Olena Kovalenko"),
                new Cinema("City Lights", "789 Broadway", "+380441234569",
                    "info@citylights.ua", "Petro Sydorenko")
            ];

            _cinemas[0].AddItem("film1");
            _cinemas[0].AddItem("film2");
            _cinemas[0].AddItem("film3");

            _cinemas[1].AddItem("film1");
            _cinemas[1].AddItem("film2");

            _cinemas[2].AddItem("film1");

            _cinemas[0].AddRating(5);
            _cinemas[0].AddRating(4);

            _cinemas[1].AddRating(3);
            _cinemas[1].AddRating(3);
            _cinemas[1].AddRating(4);

            _cinemas[2].AddRating(5);
            _cinemas[2].AddRating(5);
            _cinemas[2].AddRating(5);
        }

        [Fact]
        public void SortByNumberOfAvailableFilms_ReturnsCinemasInDescendingOrder()
        {
            // Act
            var result = CinemaService.SortByNumberOfAvailableFilms(_cinemas);

            // Assert
            Assert.Equal(3, result[0].Items.Count);
            Assert.Equal(2, result[1].Items.Count);
            Assert.Single(result[2].Items);
        }

        [Fact]
        public void FilterCinemasByName_ValidName_ReturnsMatchingCinemas()
        {
            // Act
            var result = CinemaService.FilterCinemasByName(_cinemas, "Cinema");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, cinema => Assert.Contains("Cinema", cinema.Name));
        }

        [Fact]
        public void FilterCinemasByName_CaseInsensitive_ReturnsMatchingCinemas()
        {
            // Act
            var result = CinemaService.FilterCinemasByName(_cinemas, "multiplex");

            // Assert
            Assert.Single(result);
            Assert.Equal("Multiplex Cinema", result[0].Name);
        }

        [Fact]
        public void FilterCinemasByName_EmptyString_ReturnsAllCinemas()
        {
            // Act
            var result = CinemaService.FilterCinemasByName(_cinemas, "");

            // Assert
            Assert.Equal(_cinemas.Count, result.Count);
        }

        [Fact]
        public void FilterCinemasWhereFilmAvailable_ValidFilmId_ReturnsCinemasWithFilm()
        {
            // Act
            var result = CinemaService.FilterCinemasWhereFilmAvailable(_cinemas, "film1");

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void FilterCinemasWhereFilmAvailable_FilmNotAvailable_ReturnsEmptyList()
        {
            // Act
            var result = CinemaService.FilterCinemasWhereFilmAvailable(_cinemas, "nonexistent-film");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FilterCinemasWhereFilmAvailable_NullFilmId_ReturnsEmptyList()
        {
            // Act
            var result = CinemaService.FilterCinemasWhereFilmAvailable(_cinemas, null);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void DeleteCinema_ValidCinemaId_RemovesCinemaAndRelatedData()
        {
            // Arrange
            var cinemas = new List<Cinema>
            {
                new("Test Cinema", "Address", "+380441234567", "test@test.com", "Manager")
            };
            var auditoriums = new List<Auditorium>();
            var seances = new List<Seance>();
            var reservations = new List<Reservation>();
            var tickets = new List<Ticket>();

            var cinemaId = cinemas[0].Id;

            // Act
            CinemaService.DeleteCinema(cinemas, auditoriums, seances, reservations, tickets, cinemaId);

            // Assert
            Assert.Empty(cinemas);
        }
    }
}