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
                ListaProjektow = new List<Projekt>()
            };

            var wyplata = new Wyplata();

            int kwotaPodstawowa = 3000;

            wyplata.DoPayment(pracownik, kwotaPodstawowa, true);

            Assert.Equal(4000m, wyplata.Amount);

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
                ListaProjektow = new List<Projekt>
                {
                    new Projekt { Ocena = 2 },
                    new Projekt { Ocena = 3 }
                }
            };

            var pracownikDobry = new Pracownik
            {
                FirstName = "Hubert",
                LastName = "Nowak",
                ListaProjektow = new List<Projekt>
                {
                    new Projekt { Ocena = 4 },
                    new Projekt { Ocena = 5 }
                }
            };

            var dzial = new Dzial();
            dzial.ListaPracownikow = new List<Pracownik>();
            dzial.ListaPracownikow.Add(pracownikSlaby);
            dzial.ListaPracownikow.Add(pracownikDobry);

            var wynik = dzial.FindBestEmployeeByProjectGrade();

            Assert.NotNull(wynik);
            Assert.Equal("Hubert", wynik.FirstName);
        }
    }

    public class InfoKontaktowe
    {
        [Fact]
        public void LoadContactInfo_Test()
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

            string wynik = GetPracownikInfo(pracownik);

            Assert.Contains("fgdgjjk@gmail.com", wynik);
            Assert.Contains("Częstochowa", wynik);
        }

        private string GetPracownikInfo(Pracownik p)
        {
            return $"{p.Email} {p.Telefon} {p.Adres.Miasto}";
        }
    }
}
