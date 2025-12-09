using Project.Model;
using System;
using Xunit;
using Moq;
namespace Project.Test
{
    public class DrugTest
    {
        [Fact]
        public void KonstruktorParametrycznyTest()
        {
            string nazwa = "Apap";
            string typ = "Przeciwbolowy";
            string cena = "10z³";
            string opis = "Przeciwbolowy lek oparty na paracetamolu";
            Pharmacy phar = new Pharmacy();
            var drug = new Drug(nazwa, typ, cena, opis, phar);

            Assert.Equal(nazwa, drug.Name);
            Assert.Equal(typ, drug.TypeOfMedicine);
            Assert.Equal(cena, drug.Price);
            Assert.Equal(opis, drug.Description);
        }
        [Fact]
        public void KonstruktorDomyslny()
        {
            var drug = new Drug();
            Assert.Equal(string.Empty, drug.Name);
            Assert.Equal(string.Empty, drug.TypeOfMedicine);
            Assert.Equal(string.Empty, drug.Price);
            Assert.Equal(string.Empty, drug.Description);
        }
        [Fact]
        public void ToStringTest()
        {
            string nazwa = "Apap";
            string typ = "Przeciwbolowy";
            string cena = "10z³";
            string opis = "Przeciwbolowy lek oparty na paracetamolu";
            Pharmacy phar = new Pharmacy();
            Drug drug = new Drug(nazwa, typ, cena, opis, phar);
            drug.ToString();
        }
    }
}