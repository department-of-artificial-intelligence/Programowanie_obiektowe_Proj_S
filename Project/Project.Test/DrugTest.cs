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
            string name = "Ibuprom";
            string typ = "Przeciwbolowy";
            string cena = "10z³";
            string opis = "Przeciwbolowy lek oparty na paracetamolu";
            var drug = new Drug(0, nazwa, typ, cena, opis);

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
            Drug drug = new Drug(0, nazwa, typ, cena, opis);
            drug.ToString();
        }
    }
}