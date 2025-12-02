using RestaurantNetwork.Model;

namespace RestaurantManagement.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void ReservationCreationTest()
        {
            Reservation r1 = new Reservation() { CustomerName = "Jan", PhoneNumber = "123456789" };
            Assert.Null(r1.NumberOfPeople);
            Assert.NotNull(r1.CustomerName);
            Assert.NotEmpty(r1.CustomerName);
            Assert.True(r1.CustomerName.Length > 2);
        }
    }
}
