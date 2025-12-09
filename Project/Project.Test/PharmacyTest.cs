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

            var pharmacy = new Pharmacy(name, address) { Id = id};

            Assert.Equal(id, pharmacy.Id);
            Assert.Equal(name, pharmacy.Name);
            Assert.Equal(address, pharmacy.Address);
        }
        [Fact]
        public void KonstruktorDomyslnyTest()
        {
            

        }
    }
}
