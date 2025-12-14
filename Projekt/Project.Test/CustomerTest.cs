using Projekt.Model;
namespace Project.Test 
{ 
    public class CustomerTests
    {
        [Fact]
        public void TestWyporzyczen()
        {
            var customer = new Customer();
            Assert.NotNull(customer.RentalHistory);
            Assert.Empty(customer.RentalHistory);
            Assert.IsAssignableFrom<ICollection<Rental>>(customer.RentalHistory);
        }

        [Fact]
        public void TestWypisu()
        {
            var customer = new Customer
            {
                Id = 5,
                FirstName = "Anna",
                LastName = "Nowak"
            };

            var result = customer.ToString();
            string expected = "[5] Anna Nowak ";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestDanych()
        {

            var dateOfBirth = new DateTime(1990, 1, 1);

            var customer = new Customer
            {
                Id = 10,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "123456789",
                DateOfBirth = dateOfBirth
            };

            Assert.Equal(10, customer.Id);
            Assert.Equal("Test", customer.FirstName);
            Assert.Equal("User", customer.LastName);
            Assert.Equal("123456789", customer.PhoneNumber);
            Assert.Equal(dateOfBirth, customer.DateOfBirth);
        }
    }
}
