using Moq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{
    public class PharmacyTest
    {
        [Fact]
        public void KonstuktorParametrycznyTest()
        {
            int id = 1;
            string name = "Apteka Centralna";
            var address = new Address();

            var employeeManagerMock = new Mock<IEmployeeManager>();
            var drugManagerMock = new Mock<IDrugManager>();

            var pharmacy = new Pharmacy(id, name, address, employeeManagerMock.Object, drugManagerMock.Object);

            Assert.Equal(id, pharmacy.Id);
            Assert.Equal(name, pharmacy.Name);
            Assert.Equal(address, pharmacy.Address);
            Assert.Equal(employeeManagerMock.Object, pharmacy.Employees);
            Assert.Equal(drugManagerMock.Object, pharmacy.Drugs);
        }
    }
}
