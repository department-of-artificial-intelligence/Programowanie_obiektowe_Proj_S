using Project.Model;
using Xunit;
using System;
using System.Collections.Generic;

namespace Project.TESTS
{
    public class WyplataTests
    {
        [Fact]

        public void doPayment()
        {
            var pracownik = new Pracownik
            {
                FirstName = "Marek",
                LastName = "Kowalski",
                ListaProjektow = new List<Project>()
            };

            var wyplata = new Wyplata();

            int kwotaPodstawowa = 3000;

            wyplata.doPayment(pracownik, kwotaPodstawowa);

            Assert.Equal(3000m, wyplata.Amount);

        }
    }

    public class WyszukiwanieTests
    {
        [Fact]

        public void SlabyPracownik()
        {
            var pracownikSlaby = new Pracownik
            {
                FirstName = "Jan",
                LastName = "Kowalczyk",
                ListaProjektow = new List<Project>
                {
                    new Projekt { Ocena 2 },
                    new Projekt { Ocena 3 }
                }
            };
        }

        public void DobryPracownik()
        {
            var pracownikDobry = new Pracownik
            {
                FirstName = "Hubert",
                LastName = "Nowak",
                ListaProjektow = new List<Project>
                {
                    new Projekt { Ocena 4 },
                    new Projekt { Ocena 5 }
                }
            };
        }

        var dzial = new Dzial();
        dzial.ListaPracownikow.Add(pracownikSlaby);
        dzial.ListaPracownikow.Add(pracownikDobry);

        var wynik = dzial.FindBestEmployee()
        {
            var dzial = new Dzial();

        var wynik = dzial.FindBestEmployeeByGrade();

        Assert.Null(wynik);
        }
    }
    
    public class InfoKontaktowe()
    {
        [Fact]
        public void LoadContactInfo()
        {
            var pracownik = new Pracownik
            {
                Email = "fgdgjjk@gmail.com",
                Telefon = "214-413-234",
                Adres = new Adres()
                {
                    Miasto = "Częstochowa"
                }
            };
    
            string wynik = LoadContactInfo(pracownik);
        }
    
    }
}

