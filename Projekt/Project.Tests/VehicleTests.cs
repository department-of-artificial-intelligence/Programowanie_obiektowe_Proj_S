using Xunit;
using Projekt.Model;

namespace Projekt.Tests
{
    public class VehicleTests
    {
        [Fact]
        //test konstruktora czy dobrze przypisuje wartosci
        public void CarKonstruktorTest()
        {
            var auto = new Car
            {
                Marka = "MarkaTest",
                Model = "ModelTest",
                Przebieg = 123,
                Silnik = 1.0,
                Rocznik = 2022,
                Paliwo = "PaliwoTest",
                Tablica = "TABLICA",
                LiczbaDrzwi = 5,
                Nadwozie = "Kombi"
            };
            Assert.Equal("MarkaTest", auto.Marka);
            Assert.Equal("ModelTest", auto.Model);
            Assert.Equal(5, auto.LiczbaDrzwi);
            Assert.Equal("Kombi", auto.Nadwozie);
        }
        [Fact]
        public void ToStringDlaSamochoduTest()
        {
            var auto = new Car
            {
                Marka = "Skoda",
                Model = "Octavia",
                Przebieg = 100,
                Silnik = 1.0,
                Rocznik = 2020,
                Paliwo = "Diesel",
                Tablica = "WGM123",
                LiczbaDrzwi = 4,
                Nadwozie = "Sedan"
            };
            //wywolanie metody
            string opis = auto.ToString();
            //sprwdzenie jej
            Assert.Contains("Skoda", opis);
            Assert.Contains("Octavia", opis);
            Assert.Contains("WGM123", opis);
        }
        [Fact]
        public void ToStringDlaCiezarowkiTest()
        {
            var truck = new Truck
            {
                Marka = "Volvo",
                Model = "FH",
                Przebieg = 1000,
                Silnik = 12.0,
                Rocznik = 2021,
                Paliwo = "ON",
                Tablica = "TIR1",
                Ladownosc = 24.5
            };
            string opis = truck.ToString();
            Assert.Contains("Volvo", opis);
            Assert.Contains("FH", opis);
        }
        [Fact]
        public void ToStringZKierowcaTest()
        {
            var auto = new Car
            {
                Marka = "Fiat",
                Model = "500",
                Tablica = "F1",
                Nadwozie = "Hatchback"
            };

            var kierowca = new Driver
            {
                Imie = "Adam",
                Nazwisko = "Małysz",
                NumerPrawaJazdy = "X"
            };
            auto.PrzypiszKierowce(kierowca);
            //pobranie opisu
            string opis = kierowca.ToString();
            //sprawdzenie czy zadziala metoda
            Assert.NotNull(auto.PrzypisanyKierowca);
            //sprawdzenie toStinga
            Assert.Contains("Adam", opis);
            Assert.Contains("Małysz", opis);
        }
    }
}