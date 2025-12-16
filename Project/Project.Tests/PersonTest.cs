using Project.Model;

namespace Project.Tests
{
    public class PersonTest
    {
        private class TestPerson : Person
        {
            public TestPerson() : base() { }
            public TestPerson(string fn, string ln) : base(fn, ln) { }
        }
        [Fact]
        public void PersonCreationTest()
        {
            Person p = new TestPerson() { FirstName = "Jan", LastName = "Kowalski" };

            Assert.NotNull(p.FirstName);
            Assert.Equal("Jan", p.FirstName);

            Assert.NotNull(p.LastName);
            Assert.Equal("Kowalski", p.LastName);

            Assert.True(p.FirstName.Length > 2);
        }

        [Fact]
        public void GetFullNameTest()
        {
            Person p = new TestPerson("Anna", "Nowak");

            string fullName = p.GetFullName();

            Assert.Equal("Anna Nowak", fullName);
        }
    }
}