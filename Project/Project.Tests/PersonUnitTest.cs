namespace Project.Tests
{
    public class PersonUnitTest
    {
        [Fact]
        public void PersonCreationTest()
        {

            Person p1 = new Person(FirstName = "Jan", LastName = "Kowalski");

        }
    }
}