using Project.Models;

namespace Project.Tests.Models
{
    public class CinemaNetworkTests
    {
        [Fact]
        public void CinemaNetwork_Constructor_ValidData_CreatesNetwork()
        {
            // Act
            var network = new CinemaNetwork("CinemaMax", "John Doe");

            // Assert
            Assert.Equal("CinemaMax", network.CompanyName);
            Assert.Equal("John Doe", network.ManagerName);
            Assert.Equal(0, network.TotalCinemas);
        }

        [Fact]
        public void SetTotalCinemas_ValidCount_UpdatesCount()
        {
            // Arrange
            var network = new CinemaNetwork("CinemaMax", "John Doe");

            // Act
            network.SetTotalCinemas(10);

            // Assert
            Assert.Equal(10, network.TotalCinemas);
        }

        [Fact]
        public void SetTotalCinemas_NegativeCount_ThrowsException()
        {
            // Arrange
            var network = new CinemaNetwork("CinemaMax", "John Doe");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => network.SetTotalCinemas(-1));
        }

        [Fact]
        public void UpdateInfo_ValidData_UpdatesInfo()
        {
            // Arrange
            var network = new CinemaNetwork("OldName", "OldManager");
            var newCompanyName = "NewName";
            var newManagerName = "NewManager";

            // Act
            network.UpdateInfo(newCompanyName, newManagerName);

            // Assert
            Assert.Equal(newCompanyName, network.CompanyName);
            Assert.Equal(newManagerName, network.ManagerName);
        }

        [Theory]
        [InlineData(null, "Manager")]
        [InlineData("Company", null)]
        [InlineData("", "Manager")]
        [InlineData("Company", "")]
        public void UpdateInfo_InvalidData_ThrowsException(string companyName, string managerName)
        {
            // Arrange
            var network = new CinemaNetwork("OldName", "OldManager");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => network.UpdateInfo(companyName, managerName));
        }

        [Fact]
        public void ToString_ContainsNetworkInfo()
        {
            // Arrange
            var network = new CinemaNetwork("CinemaMax", "John Doe");
            network.SetTotalCinemas(15);

            // Act
            var result = network.ToString();

            // Assert
            Assert.Contains("CinemaMax", result);
            Assert.Contains("John Doe", result);
            Assert.Contains("15", result);
        }
    }
}