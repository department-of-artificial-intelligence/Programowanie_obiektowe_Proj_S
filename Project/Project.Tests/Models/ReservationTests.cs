using Project.Models;

namespace Project.Tests.Models
{
    public class ReservationTests
    {
        [Fact]
        public void Reservation_Constructor_ValidData_CreatesReservation()
        {
            // Arrange
            var seanceId = "seance123";
            var firstName = "John";
            var lastName = "Doe";
            var email = "john@example.com";
            var phone = "+380441234567";
            var paymentMethod = "Credit Card";

            // Act
            var reservation = new Reservation(seanceId, firstName, lastName, email, phone, paymentMethod);

            // Assert
            Assert.Equal(seanceId, reservation.SeanceId);
            Assert.Equal(firstName, reservation.CustomerFirstName);
            Assert.Equal(lastName, reservation.CustomerLastName);
            Assert.Equal(email, reservation.CustomerEmail);
            Assert.Equal(phone, reservation.CustomerPhone);
            Assert.Equal(paymentMethod, reservation.PaymentMethod);
            Assert.Equal("John Doe", reservation.CustomerFullName);
        }

        [Fact]
        public void UpdateCustomerInfo_ValidData_UpdatesInfo()
        {
            // Arrange
            var reservation = new Reservation("seance123", "John", "Doe", "john@example.com", "+380441234567", "Cash");
            var newFirstName = "Jane";
            var newLastName = "Smith";
            var newEmail = "jane@example.com";
            var newPhone = "+380441111111";

            // Act
            reservation.UpdateCustomerInfo(newFirstName, newLastName, newEmail, newPhone);

            // Assert
            Assert.Equal(newFirstName, reservation.CustomerFirstName);
            Assert.Equal(newLastName, reservation.CustomerLastName);
            Assert.Equal(newEmail, reservation.CustomerEmail);
            Assert.Equal(newPhone, reservation.CustomerPhone);
        }

        [Theory]
        [InlineData(null, "Doe", "email@test.com", "123456789")]
        [InlineData("John", null, "email@test.com", "123456789")]
        [InlineData("John", "Doe", null, "123456789")]
        [InlineData("John", "Doe", "email@test.com", null)]
        public void UpdateCustomerInfo_InvalidData_ThrowsException(string firstName, string lastName, string email, string phone)
        {
            // Arrange
            var reservation = new Reservation("seance123", "John", "Doe", "john@example.com", "+380441234567", "Cash");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => reservation.UpdateCustomerInfo(firstName, lastName, email, phone));
        }

        [Fact]
        public void ToString_ContainsCustomerInfo()
        {
            // Arrange
            var reservation = new Reservation("seance123", "John", "Doe", "john@example.com", "+380441234567", "Credit Card");

            // Act
            var result = reservation.ToString();

            // Assert
            Assert.Contains("John Doe", result);
            Assert.Contains("john@example.com", result);
            Assert.Contains("+380441234567", result);
            Assert.Contains("Credit Card", result);
        }
    }
}