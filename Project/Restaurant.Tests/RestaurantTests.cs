using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;
using RestaurantNetwork.Model;

namespace RestaurantManagement.Tests
{
    public class RestaurantTests
    {
        [Fact]
        public void RestaurantCreation_ShouldSetAllProperties()
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
                Clients = new List<Person>(),
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
            Assert.NotNull(restaurant.Clients);
            Assert.NotNull(restaurant.Reservations);
        }

        [Fact]
        public void GetEmployeesByType_ShouldReturnCorrectEmployees()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            var waiter1 = CreateTestEmployee("Jan", "Kowalski", EmployeeType.Kelner, address);
            var waiter2 = CreateTestEmployee("Anna", "Nowak", EmployeeType.Kelner, address);
            var chef = CreateTestEmployee("Piotr", "Wiśniewski", EmployeeType.Kucharz, address);

            restaurant.Employees.Add(waiter1);
            restaurant.Employees.Add(waiter2);
            restaurant.Employees.Add(chef);

            // Act
            var waiters = restaurant.GetEmployeesByType(EmployeeType.Kelner);

            // Assert
            Assert.Equal(2, waiters.Count);
            Assert.All(waiters, e => Assert.Equal(EmployeeType.Kelner, e.EmployeeType));
        }

        [Fact]
        public void GetEmployeesByType_ShouldReturnEmptyListWhenNoMatch()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var chef = CreateTestEmployee("Piotr", "Wiśniewski", EmployeeType.Kucharz, address);
            restaurant.Employees.Add(chef);

            // Act
            var bartenders = restaurant.GetEmployeesByType(EmployeeType.Barman);

            // Assert
            Assert.Empty(bartenders);
        }

        [Fact]
        public void GetEmployeesFirstAndLastNameByType_ShouldReturnCorrectTuples()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            var waiter1 = CreateTestEmployee("Jan", "Kowalski", EmployeeType.Kelner, address);
            var waiter2 = CreateTestEmployee("Anna", "Nowak", EmployeeType.Kelner, address);
            var chef = CreateTestEmployee("Piotr", "Wiśniewski", EmployeeType.Kucharz, address);

            restaurant.Employees.Add(waiter1);
            restaurant.Employees.Add(waiter2);
            restaurant.Employees.Add(chef);

            // Act
            var waiterNames = restaurant.GetEmployeesFirstAndLastNameByType(EmployeeType.Kelner);

            // Assert
            Assert.Equal(2, waiterNames.Count);
            Assert.Contains(("Jan", "Kowalski"), waiterNames);
            Assert.Contains(("Anna", "Nowak"), waiterNames);
        }

        [Fact]
        public void AddMenus_ShouldAddMultipleMenuItems()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.00f },
                new MenuItem { Name = "Pasta", Description = "Italian pasta", Price = 20.00f }
            };

            // Act
            restaurant.AddMenus(menuItems);

            // Assert
            Assert.Equal(2, restaurant.Menu.Count);
            Assert.Contains(restaurant.Menu, m => m.Name == "Pizza");
            Assert.Contains(restaurant.Menu, m => m.Name == "Pasta");
        }

        [Fact]
        public void RemoveMenu_ShouldRemoveMenuItemByName()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var pizza = new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.00f };
            var pasta = new MenuItem { Name = "Pasta", Description = "Italian pasta", Price = 20.00f };
            restaurant.Menu.Add(pizza);
            restaurant.Menu.Add(pasta);

            // Act
            restaurant.RemoveMenu("Pizza");

            // Assert
            Assert.Single(restaurant.Menu);
            Assert.DoesNotContain(restaurant.Menu, m => m.Name == "Pizza");
            Assert.Contains(restaurant.Menu, m => m.Name == "Pasta");
        }

        [Fact]
        public void RemoveMenu_ShouldBeCaseInsensitive()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var pizza = new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.00f };
            restaurant.Menu.Add(pizza);

            // Act
            restaurant.RemoveMenu("pizza");

            // Assert
            Assert.Empty(restaurant.Menu);
        }

        [Fact]
        public void RemoveMenu_ShouldNotFailWhenMenuItemNotFound()
        {
            // Arrange
            var restaurant = CreateTestRestaurant();
            var pizza = new MenuItem { Name = "Pizza", Description = "Italian pizza", Price = 25.00f };
            restaurant.Menu.Add(pizza);

            // Act
            restaurant.RemoveMenu("Burger");

            // Assert
            Assert.Single(restaurant.Menu);
            Assert.Contains(restaurant.Menu, m => m.Name == "Pizza");
        }

        [Fact]
        public void Restaurant_ShouldInitializeReservationsList()
        {
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            // Act
            var restaurant = new Restaurant
            {
                Name = "Test Restaurant",
                Address = address,
                PhoneNumber = "123456789",
                Email = "test@restaurant.com",
                OpeningHours = new TimeOnly(10, 0),
                ClosingHours = new TimeOnly(22, 0),
                Menu = new List<MenuItem>(),
                Employees = new List<Employee>(),
                Clients = new List<Person>()
            };

            // Assert
            Assert.NotNull(restaurant.Reservations);
            Assert.Empty(restaurant.Reservations);
        }

        // Helper methods
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
                Clients = new List<Person>(),
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

