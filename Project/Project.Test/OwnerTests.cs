using Project.Model;
using Project.Logic;

namespace Project.Tests
{
    public class OwnerTests
    {
        [Fact]
        public void Constructor_ShouldCreateOwner()
        {
            var o = new Owner("Anna", "Nowak", "a@a.com", "123456789");

            Assert.Equal("Anna", o.FirstName);
            Assert.Equal("Nowak", o.LastName);
            Assert.Equal("a@a.com", o.Email);
            Assert.Equal("123456789", o.Phone);
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
