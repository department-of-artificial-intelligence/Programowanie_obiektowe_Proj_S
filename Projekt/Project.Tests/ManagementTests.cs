using Xunit;
using Projekt.Model;
using System.Linq;
using Projekt.ConsoleApp;
using Projekt.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Projekt.Tests
{
    public class ManagementTests
    {
        //pomocnicza metoda tworzy baze zeby inne testy nie kolidowaly ze soba
        private ApplicationDbContext DajBaze()
        {
            var opcje = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(opcje);
        }
        [Fact]
        public void DodajPojazdTest()
        {
            var baza = DajBaze();
            var manager = new Management(baza);
            //trzeba stworzyc kierowce bo jest wymagany przez klucz obcy
            var kierowca = new Driver
            {
                Imie = "Test",
                Nazwisko = "User",
                NumerPrawaJazdy = "123"
            };

            var auto = new Car
            {
                Marka = "Audi",
                Model = "A4",
                Tablica = "TEST1",
                Rocznik = 2020,
                Przebieg = 100,
                Silnik = 2.0,
                Paliwo = "PB",
                LiczbaDrzwi = 4,
                Nadwozie = "Sedan",
                PrzypisanyKierowca = kierowca
            };
            bool rezultat = manager.DodajPojazd(auto);
            //sprawdzenie
            Assert.True(rezultat);
            ///sprawdzenie czy sie zapisalo
            var znalezione = manager.ZnajdzPojazdPoRejestracji("TEST1");
            Assert.NotNull(znalezione);
            Assert.Equal("Audi", znalezione.Marka);
        }
        [Fact]
        public void UsunPojazdTest()
        {
            var baza = DajBaze();
            var manager = new Management(baza);
            //trzeba dodac zeby usunac
            var auto = new Car
            {
                Marka = "Opel",
                Model = "Astra",
                Tablica = "USUN",
                Nadwozie = "Hatchback",
                Paliwo = "Benzyna",
                PrzypisanyKierowca = new Driver { Imie = "Jan", Nazwisko = "X", NumerPrawaJazdy = "123" }
            };
            manager.DodajPojazd(auto);
            bool usunieto = manager.UsunPojazd("USUN");
            Assert.True(usunieto);
            Assert.Null(manager.ZnajdzPojazdPoRejestracji("USUN"));
        }
        [Fact]
        public void ZnajdzPojazdTest()
        {
            var baza = DajBaze();
            var manager = new Management(baza);
            var auto = new Car
            {
                Marka = "Fiat",
                Model = "Panda",
                //duze litery zeby szukalo dokladnie tego
                Tablica = "WA 111",
                Nadwozie = "Hatchback",
                Paliwo = "Benzyna",
                PrzypisanyKierowca = new Driver { Imie = "Test", Nazwisko = "User", NumerPrawaJazdy = "123" }
            };
            manager.DodajPojazd(auto);
            var znaleziony = manager.ZnajdzPojazdPoRejestracji("WA 111");
            Assert.NotNull(znaleziony);
            Assert.Equal("WA 111", znaleziony.Tablica);
        }
        [Fact]
        public void DodajWpisSerwisowyTest()
        {
            var baza = DajBaze();
            var manager = new Management(baza);
            //dodanie pojazdu zeby bylo do czego dodac wpis
            var auto = new Car
            {
                Marka = "BMW",
                Model = "X5",
                Tablica = "SERWIS",
                Nadwozie = "SUV",
                Paliwo = "Diesel",
                PrzypisanyKierowca = new Driver { Imie = "Adam", Nazwisko = "Mechanik", NumerPrawaJazdy = "X" }
            };
            manager.DodajPojazd(auto);
            bool wynik = manager.DodajWpisSerwisowy("SERWIS", "Olej", 500.0);
            Assert.True(wynik);
            var autoZBazy = manager.ZnajdzPojazdPoRejestracji("SERWIS");
            //sprawdzenie czy wpis sie dodal
            Assert.Single(autoZBazy.HistoriaSerwisowa);
            //sprawdzenie czy kwota sie zgadza
            Assert.Equal(500.0, autoZBazy.HistoriaSerwisowa[0].Koszt);
        }
    }
}