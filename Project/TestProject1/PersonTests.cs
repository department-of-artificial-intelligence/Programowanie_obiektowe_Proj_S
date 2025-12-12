using Project.Model;

namespace Project.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Constructor_ShouldCreatePerson()
        {
            var p = new Person(1, "Adam", "Kowalski", "a@a.com", "123");

            Assert.Equal(1, p.Id);
            Assert.Equal("Adam", p.FirstName);
            Assert.Equal("Kowalski", p.LastName);
            Assert.Equal("a@a.com", p.Email);
            Assert.Equal("123", p.Phone);
        }

        [Fact]
        public void EmptyConstructor_ShouldCreateDefaultValues()
        {
            var p = new Person();

            Assert.NotNull(p.FirstName);
            Assert.NotNull(p.LastName);
        }
    }
}
