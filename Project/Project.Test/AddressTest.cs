using Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{
    public class AddressTest
    {
        [Fact]
        public void KonstruktorDomyslnyTest()
        {
            Address adr = new Address();

            Assert.Equal(string.Empty, adr.City);
            Assert.Equal(string.Empty, adr.Street);
            Assert.Equal(string.Empty, adr.PostalCode);
            Assert.Equal(0, adr.Street_Number);
        }
        [Fact]
        public void KonstruktorParametrycznyTest()
        {
            string miasto = "Czestochowa";
            string Ulica = "Kilinskiego";
            string kod_pocztowy = "42-100";
            int numer = 10;
            Address adr = new Address(miasto, Ulica, kod_pocztowy, numer);
            Assert.Equal(miasto, adr.City);
            Assert.Equal(Ulica, adr.Street);
            Assert.Equal(kod_pocztowy, adr.PostalCode);
            Assert.Equal(numer, adr.Street_Number);
        }
        [Fact]
        public void ToStringTest()
        {
            string miasto = "Czestochowa";
            string Ulica = "Kilinskiego";
            string kod_pocztowy = "42-100";
            int numer = 10;
            Address adr = new Address(miasto, Ulica, kod_pocztowy, numer);
            string adres = adr.ToString();
            string poprawny = "Adres Miasto: Czestochowa, Ulica: Kilinskiego, Kod-Pocztowy: 42-100, Numer Budynku: 10";
            Assert.Equal(adres, poprawny);
        }
    }
}
