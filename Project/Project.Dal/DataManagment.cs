using Microsoft.EntityFrameworkCore;
using Project.Model;
using Project.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Project.DAL
{
    public class DataManagement
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor przyjmujący kontekst bazy danych (DI)
        public DataManagement(ApplicationDbContext context)
        {
            _context = context;
        }

       //Pobieranie kolekcji z bazy danych, zagladamy

        
        public List<Client> Clients => _context.Persons.OfType<Client>().Include(c => c.PlannedWorkouts).ToList();

        public List<Trainer> Trainers => _context.Persons.OfType<Trainer>().Include(t => t.ScheduledReservations).ToList();

        public List<Exercise> Exercises => _context.Exercises.ToList();

       
        public List<TrainerSlot> TrainerSlots => _context.TrainerSlots.Include(ts => ts.Trainer).ToList();

        //Łączy treningi z trenerami, klientami i seriami
        public List<Workout> Workouts => _context.Workouts
            .Include(w => w.Client)
            .Include(w => w.Sets)
                .ThenInclude(s => s.Exercise)
            .ToList();

        // Pobiera wszystkie Rezerwacje z relacjami Klienta i Trenera.
        public List<Reservation> Reservations => _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Trainer)
            .ToList();

       //Pobieranie danych po id

        public Client GetClientById(int id)
        {
            return _context.Persons.OfType<Client>().FirstOrDefault(c => c.Id == id)!;
        }

    
        public Trainer GetTrainerById(int id)
        {
            return _context.Persons.OfType<Trainer>().FirstOrDefault(t => t.Id == id)!;
        }

        public Exercise GetExerciseById(int id)
        {
            return _context.Exercises.FirstOrDefault(e => e.Id == id)!;
        }

        //  Metody do dodawania do bazy-----------------
        public void AddClient(Client client)
        {
            _context.Persons.Add(client);
            _context.SaveChanges();
        }

        public void AddTrainer(Trainer trainer)
        {
            _context.Persons.Add(trainer);
            _context.SaveChanges();
        }
        public void AddExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
        }
        public void AddWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }
        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        
        public void AddTrainerSlot(TrainerSlot slot)
        {
           
            _context.TrainerSlots.Add(slot);
            _context.SaveChanges();
        }

        // Usuwanie terminu gdy przypisujemy go do klienta
        public void RemoveTrainerSlot(TrainerSlot slot)
        {
            _context.TrainerSlots.Remove(slot);
            _context.SaveChanges();
        }
    }
}