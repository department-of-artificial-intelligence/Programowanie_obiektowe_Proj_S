using Project.Model;

namespace Project.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PersonCreationTest()
        {
            Person p1 = new Person("Jan", "Kowalski");
            Assert.NotNull(p1.FirstName);
            Assert.NotEmpty(p1.LastName);
            Assert.True(p1.FirstName.Length > 2);
        }
    }
}