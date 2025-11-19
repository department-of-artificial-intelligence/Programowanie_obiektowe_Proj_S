

using Project_Model;

namespace Project.Tests
{
    public class PersonCreationTest
    {
        [Fact]
        public void PersonTest()
        {
            User p1 = new User() { FirstName = "Jan", LastName = "Kowalski" };
            //Assert.NotNull(p1.Description);
            Assert.Null(p1.FirstName);
            Assert.Empty(p1.FirstName);
            Assert.True(p1.FirstName.Length > 2);
        }
    }
}