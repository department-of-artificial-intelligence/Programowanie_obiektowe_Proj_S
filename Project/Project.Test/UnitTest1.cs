using Project.Model;

namespace Project.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Employee e1 = new Employee(0, "Jan", "Kowalski", "Wlasciciel");
            Employee e2 = new Employee();
            Assert.NotNull(e1.FirstName);
            //Assert.NotEmpty(e1.FirstName.Length > 2);
        }
    }
}