using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Tests
{
    public class BranchTests
    {
        [Fact]
        public void Test1()
        {
            int expectedId = 1;
            string expectedName = "Wypozyczalnia";
            string expectedCity = "Czestochowa";
            string expectedAddress = "Dąbrowskiego 1";
            string expectedContactNum = "111222333";

            Branch branch = new Branch(expectedId, expectedName, expectedCity, expectedAddress, expectedContactNum);

            Assert.Equal(expectedId, branch.Id);
            Assert.Equal(expectedName, branch.Name);
            Assert.Equal(expectedCity, branch.City);
            Assert.Equal(expectedAddress, branch.Address);
            Assert.Equal(expectedContactNum, branch.ContactNumber);

            Assert.Empty(branch.Cars);
        }

        [Fact]
        public void Test2()
        {
            var branch = new Branch(2, "Rental", "Myszkow", "Dworska 1", "222333444");

            string result = branch.ToString();
            string expected = "[2]: Rental (Myszkow) | Adres: Dworska 1 | Tel: 222333444";

            Assert.Equal(expected, result);
            Assert.Empty(branch.Cars);
        }
    }
}