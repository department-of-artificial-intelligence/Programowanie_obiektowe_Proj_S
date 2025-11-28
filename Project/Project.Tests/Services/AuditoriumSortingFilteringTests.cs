using Project.Services.SortingFiltering;
using Project.Models;

namespace Project.Tests.Logic
{
    public class AuditoriumSortingFilteringTests
    {
        private readonly List<Auditorium> _auditoriums;

        public AuditoriumSortingFilteringTests()
        {
            _auditoriums =
            [
                new Auditorium("cinema1", "IMAX Hall", 1, 12, 20), 
                new Auditorium("cinema1", "VIP Hall", 2, 8, 15),  
                new Auditorium("cinema2", "Main Hall", 1, 10, 18), 
                new Auditorium("cinema3", "Premium Hall", 1, 6, 12) 
            ];

            _auditoriums[0].AddItem("Dolby Atmos");
            _auditoriums[0].AddItem("3D Projection");
            _auditoriums[0].AddItem("Laser Projection"); 

            _auditoriums[1].AddItem("Recliner Seats");
            _auditoriums[1].AddItem("Food Service"); 

            _auditoriums[2].AddItem("Dolby Digital"); 

            _auditoriums[3].AddItem("4K Projection");
            _auditoriums[3].AddItem("Atmos Sound");
            _auditoriums[3].AddItem("Butler Service");
            _auditoriums[3].AddItem("VIP Lounge"); 

            _auditoriums[0].AddRating(5);
            _auditoriums[0].AddRating(4);

            _auditoriums[1].AddRating(5);
            _auditoriums[1].AddRating(5); 

            _auditoriums[2].AddRating(3);
            _auditoriums[2].AddRating(4); 

            _auditoriums[3].AddRating(4);
            _auditoriums[3].AddRating(4);
            _auditoriums[3].AddRating(5); 
        }

        [Fact]
        public void FilterAuditoriumsByName_ValidName_ReturnsMatchingAuditoriums()
        {
            // Act
            var result = AuditoriumSortingFiltering.FilterAuditoriumsByName(_auditoriums, "Hall");

            // Assert
            Assert.Equal(4, result.Count);
            Assert.All(result, auditorium => Assert.Contains("Hall", auditorium.Name));
        }

        [Fact]
        public void FilterAuditoriumsByFeature_ValidFeature_ReturnsMatchingAuditoriums()
        {
            // Act
            var result = AuditoriumSortingFiltering.FilterAuditoriumsByFeature(_auditoriums, "Dolby");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, auditorium =>
                Assert.Contains(auditorium.Items, feature => feature.Contains("Dolby")));
        }

        [Fact]
        public void SortAuditoriumsByFeatures_ReturnsAuditoriumsInDescendingOrder()
        {
            // Act
            var result = AuditoriumSortingFiltering.SortAuditoriumsByFeatures(_auditoriums);

            // Assert
            Assert.Equal(4, result[0].Items.Count); 
            Assert.Equal(3, result[1].Items.Count); 
            Assert.Equal(2, result[2].Items.Count); 
            Assert.Single(result[3].Items); 
        }

        [Fact]
        public void SortAuditoriumsByMaxCapacity_ReturnsAuditoriumsInDescendingOrder()
        {
            // Act
            var result = AuditoriumSortingFiltering.SortAuditoriumsByMaxCapacity(_auditoriums);

            // Assert
            Assert.Equal(240, (double)result[0].Capacity); 
            Assert.Equal(180, (double)result[1].Capacity);
            Assert.Equal(120, (double)result[2].Capacity); 
            Assert.Equal(72, (double)result[3].Capacity);
        }

        [Fact]
        public void FilterAuditoriumsByFeature_PartialMatch_ReturnsMatchingAuditoriums()
        {
            // Act
            var result = AuditoriumSortingFiltering.FilterAuditoriumsByFeature(_auditoriums, "Projection");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FilterAuditoriumsByFeature_NoMatches_ReturnsEmptyList()
        {
            // Act
            var result = AuditoriumSortingFiltering.FilterAuditoriumsByFeature(_auditoriums, "NonExistentFeature");

            // Assert
            Assert.Empty(result);
        }
    }
}