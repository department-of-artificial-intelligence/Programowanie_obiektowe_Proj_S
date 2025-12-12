using Microsoft.EntityFrameworkCore; // <-- Potrzebne dla ToList, FirstOrDefault, SaveChanges, OfType
using Project.Model;
using Project.DAL; // Ta klasa jest teraz w Project.DAL, więc to jest jej namespace
//...

namespace Project.DAL
{
    public class DataManagement
    {
        private readonly ApplicationDbContext _context;

        // 1. Konstruktor przyjmuje ApplicationDbContext (DI)
        public DataManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        // 2. Właściwości pobierające dane z bazy (zamiast List)
        // Używamy .OfType<T>() dla dziedziczenia i .Include() dla relacji
        public List<Client> Clients => _context.Persons.OfType<Client>().Include(c => c.PlannedWorkouts).ToList();
        public List<Trainer> Trainers => _context.Persons.OfType<Trainer>().ToList();
        public List<Exercise> Exercises => _context.Exercises.ToList();

        // Zagnieżdżone Include, aby pobrać Klienta i szczegóły Ćwiczeń w Seriach
        public List<Workout> Workouts => _context.Workouts
            .Include(w => w.Client)
            .Include(w => w.Sets)
                .ThenInclude(s => s.Exercise)
            .ToList();

        // 3. Metody dodawania (zapis do bazy)
        public void AddClient(Client client)
        {
            _context.Persons.Add(client); // Dodajemy do Persons (Client dziedziczy)
            _context.SaveChanges();
        }

        public void AddTrainer(Trainer trainer)
        {
            _context.Persons.Add(trainer); // Dodajemy do Persons (Trainer dziedziczy)
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

        // 4. Metody pobierania po Id
        public Client GetClientById(int id)
        {
            return _context.Persons.OfType<Client>().FirstOrDefault(c => c.Id == id);
        }

        public Exercise GetExerciseById(int id)
        {
            return _context.Exercises.FirstOrDefault(e => e.Id == id);
        }
    }
}