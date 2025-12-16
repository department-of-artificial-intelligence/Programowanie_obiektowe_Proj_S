using Project.DAL;
using System;
using System.Linq;

namespace Project.Model
{
    public class BookingManager
    {
        private readonly ApplicationDbContext _context;

        public BookingManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTrainerSlot(int trainerId, DateTime slot)
        {
            bool exists = _context.Set<TrainerSlot>()
                .Any(ts => ts.TrainerId == trainerId && ts.SlotTime == slot);

            if (exists)
            {
                Console.WriteLine("Ten termin już istnieje.");
                return;
            }

            _context.Set<TrainerSlot>().Add(new TrainerSlot(trainerId, slot));
            _context.SaveChanges();

            Console.WriteLine($"Dodano wolny termin: {slot:yyyy-MM-dd HH:mm}");
        }

    

        public void DisplayTrainerAvailability(Trainer trainer)
        {
            Console.WriteLine($"--- DOSTĘPNOŚĆ TRENERA {trainer.LastName} ---");

            var slots = _context.Set<TrainerSlot>()
                .Where(ts => ts.TrainerId == trainer.Id && ts.SlotTime > DateTime.Now)
                .OrderBy(ts => ts.SlotTime)
                .ToList();

            if (!slots.Any())
            {
                Console.WriteLine("Brak wolnych terminów.");
                return;
            }

            foreach (var slot in slots)
                Console.WriteLine($"ID {slot.Id} → {slot.SlotTime:yyyy-MM-dd HH:mm}");
        }


        public bool BookSession(int clientId, int trainerId, DateTime slot)
        {
            
            var client = _context.Set<Client>().Find(clientId);
            var trainer = _context.Set<Trainer>().Find(trainerId);

            if (client == null || trainer == null)
            {
                Console.WriteLine("Nie znaleziono klienta lub trenera.");
                return false;
            }

            var freeSlot = _context.Set<TrainerSlot>()
                .FirstOrDefault(ts => ts.TrainerId == trainerId && ts.SlotTime == slot);

            if (freeSlot == null)
            {
                Console.WriteLine("Termin jest nie dostępny");
                return false;
            }

            _context.Set<TrainerSlot>().Remove(freeSlot);
            _context.Set<Reservation>().Add(new Reservation(trainer, client, slot));
            _context.SaveChanges();

            Console.WriteLine("Rezerwacja zapisana poprawnie.");
            return true;
        }
    }
}
