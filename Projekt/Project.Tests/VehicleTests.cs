using Xunit;
using Projekt.Model;
namespace Projekt.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void CarKonstruktorTest()
        {
            var auto = new Car("MarkaTest", "ModelTest", 123, 1.0, 2022, "PaliwoTest", "TABLICA", 5, "Kombi");
            Assert.Equal("MarkaTest", auto.Marka);
            Assert.Equal("ModelTest", auto.Model);
            Assert.Equal(5, auto.LiczbaDrzwi);
            Assert.Equal("Kombi", auto.Nadwozie);
        }
        [Fact]
        public void ToStringDlaSamochoduTest()
        {
            var auto = new Car("Skoda", "Octavia", 100, 1.0, 2020, "Diesel", "WGM123", 4, "Sedan");
            string opis = auto.ToString();
            Assert.Contains("Skoda", opis);
            Assert.Contains("Octavia", opis);
            Assert.Contains("WGM123", opis);
            Assert.Contains("Drzwi: 4", opis);
        }

        [Fact]
        public void ToStringDlaCiezarowkiTest()
        {
            var truck = new Truck("Volvo", "FH", 1000, 12.0, 2021, "ON", "TIR1", 24.5, 3);
            string opis = truck.ToString();
            Assert.Contains("Ładowność: 24.5t", opis);
            Assert.Contains("Osie: 3", opis);
        }

        [Fact]
        public void ToStringZKierowcaTest()
        {
            var auto = new Car("Fiat", "500", 10, 1.0, 2023, "E", "F1", 2, "Hatchback");
            var kierowca = new Driver("Adam", "Małysz", "X");
            auto.PrzypiszKierowce(kierowca);
            string opis = auto.ToString();
            Assert.Contains("Adam Małysz", opis);
        }
    }
}