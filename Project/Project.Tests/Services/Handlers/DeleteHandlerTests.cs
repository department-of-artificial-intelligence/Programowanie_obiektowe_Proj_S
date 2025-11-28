using Project.Services.Handlers;
using Project.Models;

namespace Project.Tests.Logic.Handlers
{
    public class DeleteHandlerTests
    {
        [Fact]
        public void DeleteActor_ValidActorId_RemovesActorAndFromFilms()
        {
            // Arrange
            var actors = new List<Actor>
            {
                new("John", "Doe", "American", new DateTime(1990, 1, 1),
                    "test.jpg", "Bio", 85.0)
            };
            var films = new List<Film>
            {
                new("Test Film", "Description", 120, "Director", "Action", false,
                    "poster.jpg", "trailer.mov")
            };

            var actorId = actors[0].Id;
            films[0].AddItem(actorId);

            // Act
            DeleteHandler.DeleteActor(actors, films, actorId);

            // Assert
            Assert.Empty(actors);
            Assert.DoesNotContain(actorId, films[0].Items);
        }

        [Fact]
        public void DeleteActor_NonExistentActorId_DoesNothing()
        {
            // Arrange
            var actors = new List<Actor>
            {
                new("John", "Doe", "American", new DateTime(1990, 1, 1),
                    "test.jpg", "Bio", 85.0)
            };
            var films = new List<Film>();
            var originalCount = actors.Count;

            // Act
            DeleteHandler.DeleteActor(actors, films, "non-existent-id");

            // Assert
            Assert.Equal(originalCount, actors.Count);
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
            DeleteHandler.DeleteFilm(films, cinemas, seances, reservations, tickets, filmId);

            // Assert
            Assert.Empty(films);
            Assert.DoesNotContain(filmId, cinemas[0].Items);
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
            DeleteHandler.DeleteCinema(cinemas, auditoriums, seances, reservations, tickets, cinemaId);

            // Assert
            Assert.Empty(cinemas);
        }

        [Fact]
        public void DeleteAuditorium_ValidAuditoriumId_RemovesAuditoriumAndSeances()
        {
            // Arrange
            var auditoriums = new List<Auditorium>
            {
                new("cinema1", "Test Hall", 1, 10, 20)
            };
            var seances = new List<Seance>
            {
                new("film1", auditoriums[0].Id, DateTime.Now.AddHours(1), 100.0m, 120)
            };
            var reservations = new List<Reservation>();
            var tickets = new List<Ticket>();

            var auditoriumId = auditoriums[0].Id;

            // Act
            DeleteHandler.DeleteAuditorium(auditoriums, seances, reservations, tickets, auditoriumId);

            // Assert
            Assert.Empty(auditoriums);
            Assert.Empty(seances);
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
            DeleteHandler.DeleteSeance(seances, reservations, tickets, seanceId);

            // Assert
            Assert.Empty(seances);
            Assert.Empty(reservations);
        }

        [Fact]
        public void DeleteReservation_ValidReservationId_RemovesReservationAndTickets()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new("seance1", "John", "Doe", "john@test.com", "+380441234567", "Cash")
            };
            var tickets = new List<Ticket>
            {
                new(reservations[0].Id, "cinema1", "auditorium1", "seance1", "film1", "A1", 100.0m,
                    TicketType.Standard)
            };

            var reservationId = reservations[0].Id;

            // Act
            DeleteHandler.DeleteReservation(reservations, tickets, reservationId);

            // Assert
            Assert.Empty(reservations);
            Assert.Empty(tickets);
        }

        [Fact]
        public void DeleteTicket_ValidTicketId_RemovesTicket()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new("reservation1", "cinema1", "auditorium1", "seance1", "film1", "A1", 100.0m,
                    TicketType.Standard)
            };

            var ticketId = tickets[0].Id;

            // Act
            DeleteHandler.DeleteTicket(tickets, ticketId);

            // Assert
            Assert.Empty(tickets);
        }

        [Fact]
        public void DeleteCinemaNetwork_ValidNetworkId_RemovesNetwork()
        {
            // Arrange
            var networks = new List<CinemaNetwork>
            {
                new("Test Network", "Manager")
            };

            var networkId = networks[0].Id;

            // Act
            DeleteHandler.DeleteCinemaNetwork(networks, networkId);

            // Assert
            Assert.Empty(networks);
        }

        [Fact]
        public void DeleteActor_ActorInMultipleFilms_RemovesFromAllFilms()
        {
            // Arrange
            var actors = new List<Actor>
            {
                new("John", "Doe", "American", new DateTime(1990, 1, 1), "test.jpg", "Bio", 85.0)
            };
            var films = new List<Film>
            {
                new("Film 1", "Description 1", 120, "Director 1", "Action", false, "poster1.jpg", "trailer1.mov"),
                new("Film 2", "Description 2", 90, "Director 2", "Comedy", false, "poster2.jpg", "trailer2.mov")
            };

            var actorId = actors[0].Id;
            films[0].AddItem(actorId);
            films[1].AddItem(actorId);

            // Act
            DeleteHandler.DeleteActor(actors, films, actorId);

            // Assert
            Assert.Empty(actors);
            Assert.All(films, film => Assert.DoesNotContain(actorId, film.Items));
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
            DeleteHandler.DeleteFilm(films, cinemas, seances, reservations, tickets, filmId);

            // Assert
            Assert.Empty(films);
            Assert.All(cinemas, cinema => Assert.DoesNotContain(filmId, cinema.Items));
        }
    }
}