using Moq;
using RatingSystem.BLL;
using RatingSystem.DAL;
using RatingSystem.Domain;

namespace RatingSystem.Tests
{
    public class RatingTests
    {
        private readonly Mock<IRatingDataLogic> _ratingDataMock;
        private readonly Mock<IUserLogic> _userLogicMock;
        private readonly Mock<IServiceLogic> _serviceLogicMock;

        public RatingTests()
        {
            _ratingDataMock = new Mock<IRatingDataLogic>();
            _userLogicMock = new Mock<IUserLogic>();
            _serviceLogicMock = new Mock<IServiceLogic>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenDataLogicIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RatingLogic(null, _userLogicMock.Object, _serviceLogicMock.Object));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenUserLogicIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RatingLogic(_ratingDataMock.Object, null, _serviceLogicMock.Object));
        }

        [Fact]
        public void Constructor_ShouldCreateInstance_WhenAllDependenciesAreProvided()
        {
            var logic = new RatingLogic(_ratingDataMock.Object, _userLogicMock.Object, _serviceLogicMock.Object);
            Assert.NotNull(logic);
        }


        [Fact]
        public async Task SubmitRating_ShouldThrowException_IfScoreIsInvalid()
        {
            var logic = new RatingLogic(_ratingDataMock.Object, _userLogicMock.Object, _serviceLogicMock.Object);


            await Assert.ThrowsAsync<ArgumentException>(() =>
                logic.SubmitRatingAsync(1, 1, 99, "Invalid"));
        }

        [Fact]
        public async Task GetAverageRating_ShouldReturnZero_IfNoData()
        {
            _ratingDataMock.Setup(d => d.GetByServiceIdAsync(It.IsAny<int>()))
                           .ReturnsAsync(new List<Rating>());

            var logic = new RatingLogic(_ratingDataMock.Object, _userLogicMock.Object, _serviceLogicMock.Object);
            var result = await logic.GetAverageRatingAsync(1);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetServiceIdByName_ShouldBeCaseInsensitive()
        {
            var services = new List<Service> { new Service { ServiceId = 5, Name = "Delivery" } };
            var mockServiceData = new Mock<IServiceDataLogic>();
            mockServiceData.Setup(d => d.GetAllAsync()).ReturnsAsync(services);

            var serviceLogic = new ServiceLogic(mockServiceData.Object);
            var id = await serviceLogic.GetServiceIdByNameAsync("DELIVERY");

            Assert.Equal(5, id);
        }

        [Fact]
        public async Task AddRating_ShouldCallAddOnce_WhenDataIsValid()
        {
            var logic = new RatingLogic(_ratingDataMock.Object, _userLogicMock.Object, _serviceLogicMock.Object);

            await logic.SubmitRatingAsync(1, 1, 5, "Good");

            _ratingDataMock.Verify(d => d.AddAsync(It.IsAny<Rating>()), Times.Once);
        }
    }
}