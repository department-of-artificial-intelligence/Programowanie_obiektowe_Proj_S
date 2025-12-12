using Project.Model;

namespace Project.Tests
{
    public class OwnerTests
    {
        [Fact]
        public void Constructor_ShouldCreateOwner()
        {
            var o = new Owner(1, "Anna", "Nowak", "a@a.com", "123");

            Assert.Equal(1, o.Id);
            Assert.Equal("Anna", o.FirstName);
            Assert.Equal("Nowak", o.LastName);
            Assert.Equal("a@a.com", o.Email);
            Assert.Equal("123", o.Phone);
        }

        [Fact]
        public void Owner_ShouldHaveEmptyAnimalList_ByDefault()
        {
            var o = new Owner();

            Assert.NotNull(o.Animals);
            Assert.Empty(o.Animals);
        }
    }
}
