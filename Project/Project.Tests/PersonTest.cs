using Project.Model;

namespace Project.Tests
{
    public class PersonTest
    {
        [Fact]
        public void PersonCreationTest()
        {
            Person p1 = new Person() { FirstName = "Jan", LastName = "Kowalski" };
            Assert.NotNull(p1.FirstName);
            Assert.Null(p1.LastName);
            Assert.NotEmpty(p1.FirstName);
            Assert.True(p1.FirstName.Length > 2);
        }
    }
}