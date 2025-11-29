using Project.Services;
using Project.Models;

namespace Project.Tests.Services
{
    public class ActorServiceTests
    {
        private readonly List<Actor> _actors;

        public ActorServiceTests()
        {
            _actors =
            [
                new Actor("John", "Doe", "American", new DateTime(1990, 1, 1),
                    "http://example.com/john.jpg", "Bio 1", 85.0),
                new Actor("Jane", "Smith", "British", new DateTime(1985, 5, 15),
                    "http://example.com/jane.jpg", "Bio 2", 92.5),
                new Actor("Bob", "Johnson", "Canadian", new DateTime(1978, 12, 10),
                    "http://example.com/bob.jpg", "Bio 3", 78.3),
                new Actor("Alice", "Williams", "Australian", new DateTime(1992, 3, 20),
                    "http://example.com/alice.jpg", "Bio 4", 88.7)
            ];
        }

        [Fact]
        public void FilterActorsByLastName_ValidLastName_ReturnsMatchingActors()
        {
            // Act
            var result = ActorService.FilterActorsByLastName(_actors, "son");

            // Assert
            Assert.Single(result);
            Assert.Contains(result, a => a.LastName == "Johnson");
        }

        [Fact]
        public void FilterActorsByLastName_CaseInsensitive_ReturnsMatchingActors()
        {
            // Act
            var result = ActorService.FilterActorsByLastName(_actors, "SMITH");

            // Assert
            Assert.Single(result);
            Assert.Contains(result, a => a.LastName == "Smith");
        }

        [Fact]
        public void FilterActorsByLastName_EmptyString_ReturnsAllActors()
        {
            // Act
            var result = ActorService.FilterActorsByLastName(_actors, "");

            // Assert
            Assert.Equal(_actors.Count, result.Count);
        }

        [Fact]
        public void FilterActorsByLastName_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ActorService.FilterActorsByLastName(_actors, "xyz");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortActorsByPopularity_ReturnsActorsInDescendingOrder()
        {
            // Act
            var result = ActorService.SortActorsByPopularity(_actors);

            // Assert
            Assert.Equal(92.5, result[0].Popularity);
            Assert.Equal(88.7, result[1].Popularity);
            Assert.Equal(85.0, result[2].Popularity);
            Assert.Equal(78.3, result[3].Popularity);
        }

        [Fact]
        public void SortActorsByPopularity_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Actor>();

            // Act
            var result = ActorService.SortActorsByPopularity(emptyList);

            // Assert
            Assert.Empty(result);
        }

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
            ActorService.DeleteActor(actors, films, actorId);

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
            ActorService.DeleteActor(actors, films, "non-existent-id");

            // Assert
            Assert.Equal(originalCount, actors.Count);
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
            ActorService.DeleteActor(actors, films, actorId);

            // Assert
            Assert.Empty(actors);
            Assert.All(films, film => Assert.DoesNotContain(actorId, film.Items));
        }
    }
}