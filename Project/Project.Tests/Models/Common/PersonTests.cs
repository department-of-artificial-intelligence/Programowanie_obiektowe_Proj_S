using Project.Models.Common;

namespace Project.Tests.Models.Common
{
    public class PersonTests
    {
        private class TestPerson : Person
        {
            public TestPerson(string firstName, string lastName, string nationality,
                DateTime birthDate, string profileImageUrl)
                : base(firstName, lastName, nationality, birthDate, profileImageUrl)
            {
            }

            public TestPerson(string id, string firstName, string lastName, string nationality,
                DateTime birthDate, string profileImageUrl, DateTime createdAt, DateTime updatedAt)
                : base(id, firstName, lastName, nationality, birthDate, profileImageUrl, createdAt, updatedAt)
            {
            }
        }

        [Fact]
        public void Person_Constructor_ValidData_SetsProperties()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var nationality = "American";
            var birthDate = new DateTime(1990, 1, 1);
            var profileImageUrl = "http://example.com/image.jpg";

            // Act
            var person = new TestPerson(firstName, lastName, nationality, birthDate, profileImageUrl);

            // Assert
            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
            Assert.Equal(nationality, person.Nationality);
            Assert.Equal(birthDate, person.BirthDate);
            Assert.Equal(profileImageUrl, person.ProfileImageUrl);
            Assert.Equal($"{firstName} {lastName}", person.FullName);
        }

        [Fact]
        public void Age_Calculation_IsCorrect()
        {
            // Arrange
            var birthDate = DateTime.Now.AddYears(-25).AddDays(-1);
            var person = new TestPerson("John", "Doe", "American", birthDate, "test.jpg");

            // Assert
            Assert.Equal(25, person.Age);
        }

        [Theory]
        [InlineData("", "Doe", "American", "1990-01-01")] 
        [InlineData("John", "", "American", "1990-01-01")] 
        [InlineData("John", "Doe", "", "1990-01-01")]
        public void Person_InvalidData_ThrowsException(string firstName, string lastName,
            string nationality, string birthDateString)
        {
            // Arrange
            var birthDate = DateTime.Parse(birthDateString);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TestPerson(firstName, lastName, nationality, birthDate, "test.jpg"));
        }

        [Fact]
        public void Person_FutureBirthDate_ThrowsException()
        {
            // Arrange
            var futureBirthDate = DateTime.Now.AddYears(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TestPerson("John", "Doe", "American", futureBirthDate, "test.jpg"));
        }

        [Fact]
        public void UpdatePersonalInfo_ValidData_UpdatesProperties()
        {
            // Arrange
            var person = new TestPerson("John", "Doe", "American",
                new DateTime(1990, 1, 1), "old.jpg");
            var newFirstName = "Jane";
            var newLastName = "Smith";
            var newNationality = "Canadian";
            var newBirthDate = new DateTime(1995, 1, 1);
            var newProfileImageUrl = "new.jpg";

            // Act
            person.UpdatePersonalInfo(newFirstName, newLastName, newNationality,
                newBirthDate, newProfileImageUrl);

            // Assert
            Assert.Equal(newFirstName, person.FirstName);
            Assert.Equal(newLastName, person.LastName);
            Assert.Equal(newNationality, person.Nationality);
            Assert.Equal(newBirthDate, person.BirthDate);
            Assert.Equal(newProfileImageUrl, person.ProfileImageUrl);
            Assert.True(person.UpdatedAt > person.CreatedAt);
        }

        [Fact]
        public void FullName_Property_ReturnsCorrectFormat()
        {
            // Arrange
            var person = new TestPerson("John", "Doe", "American",
                new DateTime(1990, 1, 1), "test.jpg");

            // Act & Assert
            Assert.Equal("John Doe", person.FullName);
        }

        [Fact]
        public void Age_BornToday_ReturnsZero()
        {
            // Arrange
            var today = DateTime.Today;
            var person = new TestPerson("Baby", "New", "Unknown", today, "test.jpg");

            // Assert
            Assert.Equal(0, person.Age);
        }

        [Fact]
        public void Age_BornYesterdayLastYear_ReturnsOne()
        {
            // Arrange
            var birthDate = DateTime.Today.AddYears(-1).AddDays(-1);
            var person = new TestPerson("John", "Doe", "American", birthDate, "test.jpg");

            // Assert
            Assert.Equal(1, person.Age);
        }

        [Theory]
        [InlineData(" John ", " Doe ")]
        [InlineData("John-Michael", "O'Conner")] 
        [InlineData("Łukasz", "Żółć")]
        public void Person_NamesWithSpecialCharacters_Valid(string firstName, string lastName)
        {
            // Arrange
            var birthDate = new DateTime(1990, 1, 1);

            // Act
            var person = new TestPerson(firstName, lastName, "American", birthDate, "test.jpg");

            // Assert
            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
        }
    }
}