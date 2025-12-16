using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TrainerSlot
    {
        public int Id { get; set; }
        public DateTime SlotTime { get; set; }

        // Relacja do Trenera
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;//referencja do trenera

       //ustaw termin jako dostęny domyślnie
        public string Status { get; set; } = "Available";

        public TrainerSlot() { }

        public TrainerSlot(int trainerId, DateTime slotTime)
        {
            TrainerId = trainerId;
            SlotTime = slotTime;
        }
    }
}

