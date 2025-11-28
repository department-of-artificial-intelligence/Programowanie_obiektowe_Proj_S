using Project.Services.SortingFiltering;
using Project.Models;

namespace Project.Tests.Logic
{
    public class ActorSortingFilteringTests
    {
        private readonly List<Actor> _actors;

        public ActorSortingFilteringTests()
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
            var result = ActorSortingFiltering.FilterActorsByLastName(_actors, "son");

            // Assert
            Assert.Single(result);
            Assert.Contains(result, a => a.LastName == "Johnson");
        }

        [Fact]
        public void FilterActorsByLastName_CaseInsensitive_ReturnsMatchingActors()
        {
            // Act
            var result = ActorSortingFiltering.FilterActorsByLastName(_actors, "SMITH");

            // Assert
            Assert.Single(result);
            Assert.Contains(result, a => a.LastName == "Smith");
        }

        [Fact]
        public void FilterActorsByLastName_EmptyString_ReturnsAllActors()
        {
            // Act
            var result = ActorSortingFiltering.FilterActorsByLastName(_actors, "");

            // Assert
            Assert.Equal(_actors.Count, result.Count);
        }

        [Fact]
        public void FilterActorsByLastName_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = ActorSortingFiltering.FilterActorsByLastName(_actors, "xyz");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortActorsByPopularity_ReturnsActorsInDescendingOrder()
        {
            // Act
            var result = ActorSortingFiltering.SortActorsByPopularity(_actors);

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
            var result = ActorSortingFiltering.SortActorsByPopularity(emptyList);

            // Assert
            Assert.Empty(result);
        }
    }
}