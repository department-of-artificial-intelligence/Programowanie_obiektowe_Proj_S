using RestaurantManagement.Models;
using RestaurantNetwork.Model;
using System;
using Xunit;

namespace RestaurantManagement.Tests
{
    public class ReservationTest
    {
        [Fact]
        public void Name_Works()
        {
            // Arrange & Act
            var reservation = new Reservation
            {
                CustomerName = "Jan",
                PhoneNumber = "123456789",
                NumberOfPeople = 1,
                Date = DateTime.Now,
                Time = new TimeOnly(18, 0)
            };

            // Assert
            Assert.NotNull(reservation.CustomerName);
            Assert.NotEmpty(reservation.CustomerName);
            Assert.Equal("Jan", reservation.CustomerName);
        }

        [Fact]
        public void Create_Works()
        {
            // Arrange
            var date = new DateTime(2025, 12, 15);
            var time = new TimeOnly(18, 30);

            // Act
            var reservation = new Reservation
            {
                CustomerName = "Jan Kowalski",
                NumberOfPeople = 4,
                PhoneNumber = "123456789",
                Date = date,
                Time = time
            };

            // Assert
            Assert.Equal("Jan Kowalski", reservation.CustomerName);
            Assert.Equal(4, reservation.NumberOfPeople);
            Assert.Equal("123456789", reservation.PhoneNumber);
            Assert.Equal(date, reservation.Date);
            Assert.Equal(time, reservation.Time);
        }

        [Fact]
        public void ToString_Works()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Anna Nowak",
                NumberOfPeople = 2,
                PhoneNumber = "987654321",
                Date = new DateTime(2025, 12, 20),
                Time = new TimeOnly(19, 0)
            };

            // Act
            var result = reservation.ToString();

            // Assert
            Assert.Contains("Anna Nowak", result);
            Assert.Contains("2", result);
        }

        [Fact]
        public void Phone_Works()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Piotr Wiśniewski",
                PhoneNumber = "555666777"
            };

            // Assert
            Assert.Equal("555666777", reservation.PhoneNumber);
            Assert.NotNull(reservation.PhoneNumber);
            Assert.NotEmpty(reservation.PhoneNumber);
        }

        [Fact]
        public void Change_Works()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Test Name",
                NumberOfPeople = 2,
                PhoneNumber = "111111111"
            };

            // Act
            reservation.CustomerName = "New Name";
            reservation.NumberOfPeople = 4;
            reservation.PhoneNumber = "999999999";

            // Assert
            Assert.Equal("New Name", reservation.CustomerName);
            Assert.Equal(4, reservation.NumberOfPeople);
            Assert.Equal("999999999", reservation.PhoneNumber);
        }
    }
}
