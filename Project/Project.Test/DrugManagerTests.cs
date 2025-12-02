using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{
    public class DrugManagerTests
    {
        [Fact]
        public void AddDrugTest()
        {
            List<Drug> leki1 = new List<Drug>()
            {
                new Drug(1, "Apap", "Przeciwbolowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu"),
                new Drug(2, "Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek"),
                new Drug(3, "Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka"),
                new Drug(4, "Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy")
            };
            IDrugsSource drugs1 = new DrugsInMemory(leki1);
            IDrugManager drugManager1 = new DrugManager(drugs1);
            string nazwa = "Ibuprom";
            string typ = "Przeciwbolowy";
            string cena = "12.50zl";
            string opis = "Przeciwbolowy lek";
            bool wynik = drugManager1.AddDrug(nazwa, typ, cena, opis);

            Assert.True(wynik);

            Assert.Equal(5, drugs1.AllDrugs().Count);
            Assert.Equal(nazwa, drugs1.AllDrugs().Last().Name);
            Assert.Equal(typ, drugs1.AllDrugs().Last().TypeOfMedicine);
            Assert.Equal(cena, drugs1.AllDrugs().Last().Price);
            Assert.Equal(opis, drugs1.AllDrugs().Last().Description);
        }
        [Fact]
        public void RemoveDrugTest()
        {
            List<Drug> leki1 = new List<Drug>()
            {
                new Drug(1, "Apap", "Przeciwbolowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu"),
                new Drug(2, "Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek"),
                new Drug(3, "Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka"),
                new Drug(4, "Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy")
            };
            IDrugsSource drugs1 = new DrugsInMemory(leki1);
            IDrugManager drugManager1 = new DrugManager(drugs1);

            bool wynik = drugManager1.RemoveDrug("Apap");
            Assert.True(wynik);

            Assert.Equal(3, drugs1.AllDrugs().Count);
            Assert.Contains(drugs1.AllDrugs(), s => s.Name == "Betesda");
            Assert.Contains(drugs1.AllDrugs(), s => s.Name == "Abirateron");
            Assert.Contains(drugs1.AllDrugs(), s => s.Name == "Paracetamol");
            Assert.DoesNotContain(drugs1.AllDrugs(), s => s.Name == "Apap");
        } 
    }
}
