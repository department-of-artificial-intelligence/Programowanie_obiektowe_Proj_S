using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;
using RestaurantNetwork.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace RestaurantManagement.Tests
{
    public class RestaurantTest
    {
        [Fact]
        public void Create_Works()
        {
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var opening = new TimeOnly(10, 0);
            var closing = new TimeOnly(22, 0);

            // Act
            var restaurant = new Restaurant
            {
                Name = "La Bella",
                Address = address,
                PhoneNumber = "123456789",
                Email = "contact@labella.com",
                OpeningHours = opening,
                ClosingHours = closing,
                Menu = new List<MenuItem>(),
                Employees = new List<Employee>(),
                Reservations = new List<Reservation>()
            };

            // Assert
            Assert.Equal("La Bella", restaurant.Name);
            Assert.Equal(address, restaurant.Address);
            Assert.Equal("123456789", restaurant.PhoneNumber);
            Assert.Equal("contact@labella.com", restaurant.Email);
            Assert.Equal(opening, restaurant.OpeningHours);
            Assert.Equal(closing, restaurant.ClosingHours);
            Assert.NotNull(restaurant.Menu);
            Assert.NotNull(restaurant.Employees);
            Assert.NotNull(restaurant.Reservations);
        }

        [Fact]
        public void AddMenu_Works()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var pizza = new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.0f };
            var pasta = new MenuItem { Name = "Pasta", Description = "Italian pasta", Price = 20.0f };

            // Act
            restaurant.AddMenus(new List<MenuItem> { pizza, pasta });

            // Assert
            Assert.Equal(2, restaurant.Menu.Count);
            Assert.Contains(restaurant.Menu, m => m.Name == "Pizza");
            Assert.Contains(restaurant.Menu, m => m.Name == "Pasta");
        }

        [Fact]
        public void RemoveMenu_Works()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var pizza = new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.0f };
            restaurant.Menu.Add(pizza);

            // Act
            restaurant.RemoveMenu("Pizza");

            // Assert
            Assert.Empty(restaurant.Menu);
        }


        [Fact]
        public void Employees_Works()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var waiter = CreateTestEmployee("Jan", "Kowalski", EmployeeType.Kelner, address);

            restaurant.Employees.Add(waiter);

            // Assert
            Assert.Equal("Jan", restaurant.Employees[0].FirstName);
            Assert.Equal(EmployeeType.Kelner, restaurant.Employees[0].EmployeeType);
        }


        [Fact]
        public void Reservations_Works()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();

            // Assert
            Assert.NotNull(restaurant.Reservations); // lista istnieje
            Assert.Empty(restaurant.Reservations);
        }

        private Restaurant CreateTestRestaurant()
        {
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            return new Restaurant
            {
                Name = "Test Restaurant",
                Address = address,
                PhoneNumber = "123456789",
                Email = "test@restaurant.com",
                OpeningHours = new TimeOnly(10, 0),
                ClosingHours = new TimeOnly(22, 0),
                Menu = new List<MenuItem>(),
                Employees = new List<Employee>(),
                Reservations = new List<Reservation>()
            };
        }

        private Employee CreateTestEmployee(string firstName, string lastName, EmployeeType type, Address address)
        {
            return new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = "123456789",
                Email = $"{firstName.ToLower()}@restaurant.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = address,
                EmployeeType = type,
                Salary = 3000,
                HiredOn = DateTime.Now
            };
        }
    }
}
