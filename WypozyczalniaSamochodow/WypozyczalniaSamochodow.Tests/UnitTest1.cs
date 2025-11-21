using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Branch branch = new Branch()
            {
                Name = "Rentalsy",
                City = "Katowice"
            };

            Assert.NotNull(branch?.Id);
            Assert.NotEmpty(branch.Name);
            Assert.NotEmpty(branch.City);
        }
    }
}