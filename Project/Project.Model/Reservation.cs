using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ScheduledTime { get; set; } // Data i godzina rezerwacji
        public DateTime CreationDate { get; set; } // Data utworzenia rezerwacji

        // Relacje do Trenera
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        // Relacje do Klienta
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public Reservation() { }

        public Reservation(Trainer trainer, Client client, DateTime scheduledTime)
        {
            Trainer = trainer;
            ClientId = client.Id;
            Client = client;
            ScheduledTime = scheduledTime;
            CreationDate = DateTime.Now; // Dodajemy to jako metadane rezerwacji
        }
    }
}
