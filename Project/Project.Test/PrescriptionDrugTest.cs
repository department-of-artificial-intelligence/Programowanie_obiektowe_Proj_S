using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Xunit;
namespace Project.Test
{
    public class PrescriptionDrugTest
    {
        [Fact]
        public void KonstruktorParametrycznyTest()
        {
            string nazwa = "betesda";
            string typ = "Przeciwdepresyjne";
            string cena = "12.50zl";
            string opis = "Przeciwdepresyjny lek mocny";
            Pharmacy phar = new Pharmacy();
            PrescriptionDrug drug = new PrescriptionDrug(nazwa, typ, cena, opis, phar);

            Assert.Equal(nazwa, drug.Name);
            Assert.Equal(typ, drug.TypeOfMedicine);
            Assert.Equal(cena, drug.Price);
            Assert.Equal(opis, drug.Description);
        }
    }
}
