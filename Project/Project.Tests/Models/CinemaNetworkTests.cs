using Project.Models;

namespace Project.Tests.Models
{
    public class CinemaNetworkTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateCinemaNetworkWithCorrectProperties()
        {
            // Given
            var companyName = "CinemaMax Ukraine";
            var managerName = "Olena Sydorenko";

            // When
            var network = new CinemaNetwork(companyName, managerName);

            // Then
            Assert.Equal(companyName, network.CompanyName);
            Assert.Equal(managerName, network.ManagerName);
            Assert.Equal(0, network.TotalCinemas);
            Assert.NotEmpty(network.Id);
        }

        [Fact]
        public void UpdateInfo_WithValidData_ShouldUpdateCompanyAndManagerName()
        {
            // Given
            var network = CreateTestNetwork();
            var newCompanyName = "Updated Network";
            var newManagerName = "Updated Manager";

            // When
            network.UpdateInfo(newCompanyName, newManagerName);

            // Then
            Assert.Equal(newCompanyName, network.CompanyName);
            Assert.Equal(newManagerName, network.ManagerName);
        }

        [Fact]
        public void SetTotalCinemas_WithValidCount_ShouldUpdateTotalCinemas()
        {
            // Given
            var network = CreateTestNetwork();
            var newCount = 15;

            // When
            network.SetTotalCinemas(newCount);

            // Then
            Assert.Equal(newCount, network.TotalCinemas);
        }

        [Fact]
        public void SetTotalCinemas_WithZero_ShouldUpdateTotalCinemas()
        {
            // Given
            var network = CreateTestNetwork();

            // When
            network.SetTotalCinemas(0);

            // Then
            Assert.Equal(0, network.TotalCinemas);
        }

        [Fact]
        public void SetTotalCinemas_WithNegativeCount_ShouldThrowArgumentException()
        {
            // Given
            var network = CreateTestNetwork();

            // When & Then
            Assert.Throws<ArgumentException>(() => network.SetTotalCinemas(-5));
        }

        [Fact]
        public void Constructor_WithEmptyCompanyName_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new CinemaNetwork("", "Manager"));
        }

        [Fact]
        public void Constructor_WithEmptyManagerName_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new CinemaNetwork("Company", ""));
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var network = CreateTestNetwork();
            var initialUpdatedAt = network.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            network.MarkAsUpdated();

            // Then
            Assert.True(network.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var network = CreateTestNetwork();
            network.SetTotalCinemas(25);

            // When
            var result = network.ToString();

            // Then
            Assert.Contains("Cinema Network:", result);
            Assert.Contains(network.CompanyName, result);
            Assert.Contains(network.ManagerName, result);
            Assert.Contains("25", result);
            Assert.Contains(network.Id, result);
        }

        [Fact]
        public void UpdateInfo_ShouldAlsoUpdateTimestamp()
        {
            // Given
            var network = CreateTestNetwork();
            var initialUpdatedAt = network.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            network.UpdateInfo("New Company", "New Manager");

            // Then
            Assert.True(network.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void SetTotalCinemas_ShouldAlsoUpdateTimestamp()
        {
            // Given
            var network = CreateTestNetwork();
            var initialUpdatedAt = network.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            network.SetTotalCinemas(10);

            // Then
            Assert.True(network.UpdatedAt > initialUpdatedAt);
        }

        private static CinemaNetwork CreateTestNetwork()
        {
            return new CinemaNetwork("Test Network", "Test Manager");
        }
    }
}