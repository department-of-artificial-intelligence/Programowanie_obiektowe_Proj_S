using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class ReservationTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeWartosci()
        {
            
            var reservation = new Reservation();

            
            Assert.Null(reservation.Trainer);
            Assert.Null(reservation.Client);
            Assert.Equal(0, reservation.TrainerId);
            Assert.Equal(0, reservation.ClientId);
            Assert.Equal(DateTime.MinValue, reservation.ScheduledTime);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
           
            var client = new Client("Ewa", "Polak", "e@p.pl", 70, 1.70, "Mass Gain");
            client.Id = 10;
            var trainer = new Trainer("Lukasz", "Zielony", "l@z.pl", "CrossFit", 130.00m);
            trainer.Id = 3;
            var slot = DateTime.Now.Date.AddDays(7).AddHours(10);

           
            var reservation = new Reservation(trainer, client, slot);

            Assert.Equal(trainer, reservation.Trainer);
            Assert.Equal(client, reservation.Client);
            Assert.Equal(trainer.Id, reservation.TrainerId);
            Assert.Equal(client.Id, reservation.ClientId);
            Assert.Equal(slot, reservation.ScheduledTime);
            Assert.True((DateTime.Now - reservation.CreationDate).TotalSeconds < 1);
        }
    }
}