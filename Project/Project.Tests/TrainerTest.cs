using Xunit;
using Project.Model;
using System;
using System.Linq;

namespace Project.Tests
{
    public class TrainerTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeKolekcje()
        {
            
            var trainer = new Trainer();
            Assert.Equal(string.Empty, trainer.FirstName);
            Assert.Equal(0, trainer.HourlyRate);

            Assert.NotNull(trainer.ScheduledReservations);
            Assert.Empty(trainer.ScheduledReservations);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            string imie = "Robert";
            string specjalizacja = "Rehabilitacja";
            decimal stawka = 180.50m;

            var trainer = new Trainer(imie, "Kowalski", "r@k.pl", specjalizacja, stawka);

            Assert.Equal(imie, trainer.FirstName);
            Assert.Equal(stawka, trainer.HourlyRate);
            Assert.Equal(specjalizacja, trainer.Specialization);
        }

        [Fact]
        public void OpisRaportu_ZwrociPoprawnyFormat()
        {
            var trainer = new Trainer("Alicja", "Nowak", "a@n.com", "Pilates", 110.00m);
            trainer.Id = 22;
            string oczekiwanyFragment = "TRAINER (ID: 22): Alicja Nowak | Specialization: Pilates";

            string wynik = trainer.ReportDescription;

            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}