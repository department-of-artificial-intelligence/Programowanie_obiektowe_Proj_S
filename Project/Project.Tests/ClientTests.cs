using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class KlientTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeWartosci()
        {
            
            var klient = new Client();

            
            Assert.Equal(string.Empty, klient.FirstName);
            Assert.Equal(string.Empty, klient.LastName);

          
            Assert.NotNull(klient.PlannedWorkouts);
            Assert.Empty(klient.PlannedWorkouts);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            
            string imie = "Marta";
            string nazwisko = "Zając";
            double waga = 62.5;
            string cel = "redukcja";

            
            var klient = new Client(imie, nazwisko, "marta@gym.pl", waga, 1.68, cel);

            
            Assert.Equal(imie, klient.FirstName);
            Assert.Equal(cel, klient.TrainingGoal);
            Assert.Equal(DateTime.Now.Date, klient.JoinDate);
        }

        [Fact]
        public void OpisRaportu_ZwrociPoprawnyFormat()
        {
           
            var klient = new Client("Piotr", "Lis", "p@l.com", 80, 1.80, "masa");
            klient.Id = 15;
            string oczekiwanyFragment = "CLIENT (ID: 15): Piotr Lis | Goal: masa";

           
            string wynik = klient.ReportDescription;

            
            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}