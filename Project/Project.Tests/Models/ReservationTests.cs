using Project.Models;

namespace Project.Tests.Models
{
    public class ReservationTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateReservationWithCorrectProperties()
        {
            // Given
            var seanceId = "seance-123";
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@email.com";
            var phone = "+380501234567";
            var paymentMethod = "Credit Card";

            // When
            var reservation = new Reservation(seanceId, firstName, lastName, email, phone, paymentMethod);

            // Then
            Assert.Equal(seanceId, reservation.SeanceId);
            Assert.Equal(firstName, reservation.CustomerFirstName);
            Assert.Equal(lastName, reservation.CustomerLastName);
            Assert.Equal(email, reservation.CustomerEmail);
            Assert.Equal(phone, reservation.CustomerPhone);
            Assert.Equal(paymentMethod, reservation.PaymentMethod);
            Assert.Equal("John Doe", reservation.CustomerFullName);
            Assert.NotEmpty(reservation.Id);
        }

        [Fact]
        public void CustomerFullName_ShouldReturnFirstNameAndLastName()
        {
            // Given
            var reservation = CreateTestReservation();

            // When
            var fullName = reservation.CustomerFullName;

            // Then
            Assert.Equal("John Doe", fullName);
        }

        [Fact]
        public void UpdateCustomerInfo_WithValidData_ShouldUpdateCustomerProperties()
        {
            // Given
            var reservation = CreateTestReservation();
            var newFirstName = "Jane";
            var newLastName = "Smith";
            var newEmail = "jane.smith@email.com";
            var newPhone = "+380502345678";

            // When
            reservation.UpdateCustomerInfo(newFirstName, newLastName, newEmail, newPhone);

            // Then
            Assert.Equal(newFirstName, reservation.CustomerFirstName);
            Assert.Equal(newLastName, reservation.CustomerLastName);
            Assert.Equal(newEmail, reservation.CustomerEmail);
            Assert.Equal(newPhone, reservation.CustomerPhone);
            Assert.Equal("Jane Smith", reservation.CustomerFullName);
        }

        [Fact]
        public void UpdateCustomerInfo_WithEmptyFirstName_ShouldThrowArgumentException()
        {
            // Given
            var reservation = CreateTestReservation();

            // When & Then
            Assert.Throws<ArgumentException>(() => reservation.UpdateCustomerInfo("", "Smith", "email@test.com", "+380501234567"));
        }

        [Fact]
        public void UpdateCustomerInfo_WithEmptyEmail_ShouldThrowArgumentException()
        {
            // Given
            var reservation = CreateTestReservation();

            // When & Then
            Assert.Throws<ArgumentException>(() => reservation.UpdateCustomerInfo("Jane", "Smith", "", "+380501234567"));
        }

        [Fact]
        public void Constructor_WithEmptySeanceId_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Reservation("", "John", "Doe", "email@test.com", "+380501234567", "Credit Card"));
        }

        [Fact]
        public void Constructor_WithEmptyPhone_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Reservation("seance-123", "John", "Doe", "email@test.com", "", "Credit Card"));
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var reservation = CreateTestReservation();
            var initialUpdatedAt = reservation.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            reservation.MarkAsUpdated();

            // Then
            Assert.True(reservation.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var reservation = CreateTestReservation();

            // When
            var result = reservation.ToString();

            // Then
            Assert.Contains("Reservation for:", result);
            Assert.Contains(reservation.CustomerFullName, result);
            Assert.Contains(reservation.CustomerEmail, result);
            Assert.Contains(reservation.CustomerPhone, result);
            Assert.Contains(reservation.PaymentMethod, result);
            Assert.Contains(reservation.SeanceId, result);
            Assert.Contains(reservation.Id, result);
        }

        [Fact]
        public void Constructor_WithDifferentPaymentMethods_ShouldAcceptValidPaymentMethods()
        {
            // Given
            var paymentMethods = new[] { "Cash", "Credit Card", "PayPal", "Online Payment" };

            // When & Then - Should not throw
            foreach (var method in paymentMethods)
            {
                var reservation = new Reservation("seance-123", "John", "Doe", "email@test.com", "+380501234567", method);
                Assert.Equal(method, reservation.PaymentMethod);
            }
        }

        [Fact]
        public void UpdateCustomerInfo_WithSameValues_ShouldStillUpdateTimestamp()
        {
            // Given
            var reservation = CreateTestReservation();
            var initialUpdatedAt = reservation.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            reservation.UpdateCustomerInfo("John", "Doe", "john.doe@email.com", "+380501234567");

            // Then
            Assert.True(reservation.UpdatedAt > initialUpdatedAt);
        }

        private static Reservation CreateTestReservation()
        {
            return new Reservation("seance-123", "John", "Doe", "john.doe@email.com", "+380501234567", "Credit Card");
        }
    }
}