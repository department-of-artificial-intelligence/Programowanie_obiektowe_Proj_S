using Project.Models;
using Project.Services;

namespace Project.Tests.Services
{
    public class CinemaNetworkServiceTests
    {
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
            CinemaNetworkService.DeleteCinemaNetwork(networks, networkId);

            // Assert
            Assert.Empty(networks);
        }
    }
}
