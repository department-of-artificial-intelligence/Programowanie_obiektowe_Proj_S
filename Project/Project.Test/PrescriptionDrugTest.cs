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

            PrescriptionDrug drug = new PrescriptionDrug(0, nazwa, typ, cena, opis);

            Assert.Equal(nazwa, drug.Name);
            Assert.Equal(typ, drug.TypeOfMedicine);
            Assert.Equal(cena, drug.Price);
            Assert.Equal(opis, drug.Description);
        }
    }
}
