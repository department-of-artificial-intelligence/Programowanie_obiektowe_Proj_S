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

        // =========================================================
        // 1. READ PROPERTIES (Pobieranie wszystkich kolekcji)
        // =========================================================

        // Pobiera wszystkich Klientów z relacjami (Dziedziczenie TPH).
        public List<Client> Clients => _context.Persons.OfType<Client>().Include(c => c.PlannedWorkouts).ToList();

        // Pobiera wszystkich Trenerów z relacjami (Dziedziczenie TPH).
        public List<Trainer> Trainers => _context.Persons.OfType<Trainer>().Include(t => t.ScheduledReservations).ToList();

        // Pobiera wszystkie Ćwiczenia.
        public List<Exercise> Exercises => _context.Exercises.ToList();

        // Pobiera wszystkie Treningi z detalami Klienta i Serii (zagnieżdżone Include).
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

        // Pobiera wszystkie dostępne sloty Trenerów (TrainerSlot).
        public List<TrainerSlot> TrainerSlots => _context.TrainerSlots.Include(ts => ts.Trainer).ToList();

        // =========================================================
        // 2. RETRIEVAL METHODS (Pobieranie po ID)
        // =========================================================

        // Pobiera Klienta po ID.
        public Client GetClientById(int id)
        {
            return _context.Persons.OfType<Client>().FirstOrDefault(c => c.Id == id);
        }

        // Pobiera Trenera po ID.
        public Trainer GetTrainerById(int id)
        {
            return _context.Persons.OfType<Trainer>().FirstOrDefault(t => t.Id == id);
        }

        // Pobiera Ćwiczenie po ID.
        public Exercise GetExerciseById(int id)
        {
            return _context.Exercises.FirstOrDefault(e => e.Id == id);
        }

        // =========================================================
        // 3. ADD METHODS (Dodawanie i zapis do bazy)
        // =========================================================

        // Dodaje nowego Klienta i zapisuje zmiany (dodanie do tabeli Persons).
        public void AddClient(Client client)
        {
            _context.Persons.Add(client);
            _context.SaveChanges();
        }

        // Dodaje nowego Trenera i zapisuje zmiany (dodanie do tabeli Persons).
        public void AddTrainer(Trainer trainer)
        {
            _context.Persons.Add(trainer);
            _context.SaveChanges();
        }

        // Dodaje nowe Ćwiczenie i zapisuje zmiany.
        public void AddExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
        }

        // Dodaje nowy Trening (Workout) i zapisuje zmiany.
        public void AddWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }

        // Dodaje nową Rezerwację (Reservation) i zapisuje zmiany.
        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        // Dodaje nowy wolny Slot Trenera (TrainerSlot) i zapisuje zmiany.
        public void AddTrainerSlot(TrainerSlot slot)
        {
            _context.TrainerSlots.Add(slot);
            _context.SaveChanges();
        }

        // =========================================================
        // 4. REMOVE METHODS (Usuwanie i zapis do bazy)
        // =========================================================

        // Usuwa wolny termin Trenera z bazy danych (używane po zarezerwowaniu slotu).
        public void RemoveTrainerSlot(TrainerSlot slot)
        {
            _context.TrainerSlots.Remove(slot);
            _context.SaveChanges();
        }
    }
}