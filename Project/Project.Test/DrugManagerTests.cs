using Moq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{ // Klasa wylacznie do testow
    public class DrugManagerTests
    {
        [Fact]
        public void AddDrugTest()
        {
            Pharmacy phar = new Pharmacy();
            List<Drug> leki1 = new List<Drug>()
            {
                new Drug("Apap", "Przeciwbolowy", "12.50zł", "Przeciwbolowy lek oparty na paracetamolu", phar) {DrugId = 1},
                new Drug("Betesda", "Przeciwdepresyjny", "50.21zł", "Antydepresyjny lek", phar) {DrugId = 2},
                new Drug("Abirateron", "Przeciwnowotworowe", "1200.51zł", "Stosowany w leczeniu raka", phar) {DrugId = 3},
                new Drug("Paracetamol", "Przeciwbólowy, Przeciwgorączkowy", "12.99zł", "Paracetamol jest lekiem stosowanym w leczeniu bólu o łagodnym i umiarkowanym nasileniu, takim jak bóle głowy", phar) {DrugId = 4}
            };
            IDrugsSource drugs1 = new DrugsInMemory(leki1);
            DrugManager drugManager1 = new DrugManager(drugs1);
            string nazwa = "Ibuprom";
            string typ = "Przeciwbolowy";
            string cena = "12.50zl";
            string opis = "Przeciwbolowy lek";
            bool wynik = drugManager1.AddDrug(nazwa, typ, cena, opis, phar);

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
            Pharmacy phar = new Pharmacy();
            var drug1 = new Drug("Apap", "Przeciwbolowy", "15zl", "Przeciwbolowka", phar) { DrugId = 1 };
            var drug2 = new Drug("Betesda", "Przeciwdepresyjny", "50zl", "Antydepresyjny", phar) { DrugId = 2 };
            phar.Drugs.Add(drug1);
            phar.Drugs.Add(drug2);
            IDrugsSource inMemorySource = new DrugsInMemory(phar.Drugs);
            var manager = new DrugManager(inMemorySource);

            bool result = manager.RemoveDrug(1, phar);


            Assert.True(result);
            Assert.Single(phar.Drugs);
            Assert.Equal(2, phar.Drugs.First().DrugId);
        }
    }
}
