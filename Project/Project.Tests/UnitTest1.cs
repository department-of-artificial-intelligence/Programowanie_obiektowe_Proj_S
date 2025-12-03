using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Project.Model;

namespace Project.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PersonCreationTest()
        {
            User p1 = new User() { Name = "Jan", Last_Name = "Kowalski", Role = 1, Contact_Details = 1 };
            Assert.Null(p1.Name);
            Assert.NotEmpty(p1.Name);
            Assert.True(p1.Name.Length>2);
    }
    }
}