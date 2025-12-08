using Xunit;
using Projekt.Model;
using System.Linq;

namespace Projekt.Tests
{
    public class ManagementTests
    {
        [Fact]
        public void DodajPojazdTest()
        {
            var manager = new Management();
            var auto = new Car("Audi", "A4", 100, 2.0, 2020, "PB", "TEST1", 4, "Sedan");
            bool rezultat = manager.DodajPojazd(auto);
            Assert.True(rezultat);
            var znaleziony = manager.ZnajdzPojazdPoRejestracji("TEST1");
            Assert.NotNull(znaleziony);
            Assert.Equal("Audi", znaleziony.Marka);
        }
        [Fact]
        public void UsunPojazdTest()
        {
            var manager = new Management();
            var auto = new Car("Opel", "Astra", 100, 1.4, 2010, "PB", "USUNMNIE", 5, "Hatchback");
            manager.DodajPojazd(auto);
            bool rezultatUsuwania = manager.UsunPojazd("USUNMNIE");
            Assert.True(rezultatUsuwania);
            var szukany = manager.ZnajdzPojazdPoRejestracji("USUNMNIE");
            Assert.Null(szukany);
        }
        [Fact]
        public void PrzypiszKierowceTest()
        {
            var manager = new Management();
            var auto = new Car("Ford", "Focus", 500, 1.6, 2015, "Diesel", "KIEROWCA1", 5, "Kombi");
            manager.DodajPojazd(auto);
            var kierowca = new Driver("Jan", "Testowy", "XYZ999");
            bool rezultat = manager.PrzypiszKierowceDoPojazdu("KIEROWCA1", kierowca);
            Assert.True(rezultat);
            var autoZBazy = manager.ZnajdzPojazdPoRejestracji("KIEROWCA1");
            Assert.NotNull(autoZBazy.PrzypisanyKierowca);
            Assert.Equal("Jan", autoZBazy.PrzypisanyKierowca.Imie);
        }
        [Fact]
        public void ZnajdzPojazdTest()
        {
            var manager = new Management();
            manager.DodajPojazd(new Car("Opel", "Corsa", 100, 1.2, 2010, "PB", "WA 12345", 3, "Hatchback"));
            var znaleziony = manager.ZnajdzPojazdPoRejestracji("wa 12345");
            Assert.NotNull(znaleziony);
            Assert.Equal("WA 12345", znaleziony.Tablica);
        }
        [Fact]
        public void DodajWpisSerwisowyTest()
        {
            var manager = new Management();
            manager.DodajPojazd(new Car("BMW", "E46", 200000, 2.0, 2003, "PB", "SERWIS1", 4, "Sedan"));
            bool wynik = manager.DodajWpisSerwisowy("SERWIS1", "Wymiana klocków", 450.00);
            Assert.True(wynik);
            var auto = manager.ZnajdzPojazdPoRejestracji("SERWIS1");
            Assert.NotEmpty(auto.HistoriaSerwisowa);
            Assert.Single(auto.HistoriaSerwisowa);
            Assert.Equal(450.00, auto.HistoriaSerwisowa[0].Koszt);
        }
    }
}