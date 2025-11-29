using System;
using Project.Model;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Globalne instancje klas zarządzających
    private static DataManagement data = new DataManagement();
    private static ReportGenerator reports = new ReportGenerator();

    static void Main(string[] args)
    {
        InitializeSampleData(); // Inicjalizacja przykładowych danych testowych

        while (true)
        {
            DisplayMainMenu(); // Wyświetlenie menu głównego
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddClientInteractive(); // Dodaj nowego Klienta
                    break;
                case "2":
                    AddTrainerInteractive(); // Dodaj nowego Trenera
                    break;
                case "3":
                    AddExerciseInteractive(); // Dodaj nowe Ćwiczenie
                    break;
                case "4":
                    AddWorkoutInteractive(); // Dodaj nowy Trening (interaktywnie)
                    break;
                case "5":
                    // Generowanie raportu z grupowaniem przy użyciu wyrażenia lambda (LINQ)
                    reports.GroupClientsByGoal(data.Clients);
                    break;
                case "7":
                    Console.WriteLine("\nZamykanie aplikacji...");
                    return;
                default:
                    Console.WriteLine("\nNieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }

    // --- METODY INTERAKTYWNE ZE STANDARDOWYM ODCZYTEM ---

    private static void AddClientInteractive()
    {
        Console.WriteLine("\n--- DODAWANIE NOWEGO KLIENTA ---");

        // UWAGA: Konwersje i odczyty są teraz proste, bez walidacji pętli!

        Console.Write("Podaj ID Klienta (unikalna liczba całkowita): ");
        // Ręczne konwertowanie ciągu na liczbę całkowitą. Wystąpi błąd, jeśli dane będą złe!
        int id = int.Parse(Console.ReadLine());

        // --- Sprawdzanie unikalności ID (Walidacja logiczna musi pozostać) ---
        if (data.Clients.Any(c => c.Id == id))
        {
            Console.WriteLine($"Błąd: Klient o ID {id} już istnieje. Program zakończy działanie, by uniknąć błędu.");
            return;
        }

        Console.Write("Podaj imię: ");
        string firstName = Console.ReadLine();
        Console.Write("Podaj nazwisko: ");
        string lastName = Console.ReadLine();
        Console.Write("Podaj email: ");
        string email = Console.ReadLine();

        Console.Write("Podaj wagę (kg): ");
        double weight = double.Parse(Console.ReadLine()); // Prosta konwersja

        Console.Write("Podaj wzrost (m): ");
        double height = double.Parse(Console.ReadLine()); // Prosta konwersja

        Console.Write("Podaj cel treningowy (np. redukcja, masa): ");
        string goal = Console.ReadLine();

        var newClient = new Client(id, firstName, lastName, email, weight, height, goal);
        data.AddClient(newClient);
        Console.WriteLine($"\n✅ Dodano klienta: {newClient.FirstName} {newClient.LastName} (ID: {newClient.Id})");
    }

    private static void AddTrainerInteractive()
    {
        Console.WriteLine("\n--- DODAWANIE NOWEGO TRENERA ---");

        // UWAGA: Konwersje i odczyty są teraz proste, bez walidacji pętli!

        Console.Write("Podaj ID Trenera (unikalna liczba całkowita): ");
        int id = int.Parse(Console.ReadLine());

        // --- Sprawdzanie unikalności ID (Walidacja logiczna musi pozostać) ---
        if (data.Trainers.Any(t => t.Id == id))
        {
            Console.WriteLine($"Błąd: Trener o ID {id} już istnieje. Program zakończy działanie, by uniknąć błędu.");
            return;
        }

        Console.Write("Podaj imię: ");
        string firstName = Console.ReadLine();
        Console.Write("Podaj nazwisko: ");
        string lastName = Console.ReadLine();
        Console.Write("Podaj email: ");
        string email = Console.ReadLine();

        Console.Write("Podaj specjalizację: ");
        string specialization = Console.ReadLine();

        Console.Write("Podaj stawkę godzinową (PLN): ");
        decimal rate = decimal.Parse(Console.ReadLine()); // Prosta konwersja

        var newTrainer = new Trainer(id, firstName, lastName, email, specialization, rate);
        data.AddTrainer(newTrainer);
        Console.WriteLine($"\n✅ Dodano trenera: {newTrainer.FirstName} {newTrainer.LastName} (ID: {newTrainer.Id})");
    }

    private static void AddExerciseInteractive()
    {
        Console.WriteLine("\n--- DODAWANIE NOWEGO ĆWICZENIA ---");

        Console.Write("Podaj ID Ćwiczenia (unikalna liczba całkowita): ");
        int id = int.Parse(Console.ReadLine());

        // --- Sprawdzanie unikalności ID (Walidacja logiczna musi pozostać) ---
        if (data.Exercises.Any(e => e.Id == id))
        {
            Console.WriteLine($"Błąd: Ćwiczenie o ID {id} już istnieje. Program zakończy działanie, by uniknąć błędu.");
            return;
        }

        Console.Write("Podaj nazwę ćwiczenia: ");
        string name = Console.ReadLine();
        Console.Write("Podaj partię mięśniową: ");
        string muscleGroup = Console.ReadLine();

        var newExercise = new Exercise(id, name, muscleGroup);
        data.AddExercise(newExercise);
        Console.WriteLine($"\n✅ Dodano ćwiczenie: {newExercise.Name} (ID: {newExercise.Id})");
    }

    private static void AddWorkoutInteractive()
    {
        Console.WriteLine("\n--- DODAJ NOWY TRENING ---");

        // 1. POBIERANIE ID TRENINGU
        Console.Write("Podaj ID Treningu (unikalna liczba całkowita): ");
        int workoutId = int.Parse(Console.ReadLine());

        if (data.Workouts.Any(w => w.Id == workoutId))
        {
            Console.WriteLine($"Błąd: Trening o ID {workoutId} już istnieje. Program zakończy działanie.");
            return;
        }

        // 2. Wybór Klienta
        Console.WriteLine("Dostępni Klienci:");
        if (!data.Clients.Any())
        {
            Console.WriteLine("Brak klientów do przypisania!");
            return;
        }
        foreach (var c in data.Clients)
        {
            Console.WriteLine($"- ID {c.Id}: {c.FirstName} {c.LastName}");
        }

        Console.Write("Podaj ID Klienta dla treningu: ");
        int clientId = int.Parse(Console.ReadLine());
        Client client = data.GetClientById(clientId);

        if (client == null)
        {
            Console.WriteLine("Klient o podanym ID nie istnieje. Program zakończy działanie.");
            return;
        }

        var newWorkout = new Workout(workoutId, DateTime.Now.Date, client);
        Console.WriteLine($"\n✅ Tworzenie treningu dla {client.FirstName} {client.LastName}...");

        // 3. Dodawanie Serii Ćwiczeń (Uproszczona pętla)
        Console.WriteLine("\n--- DODAWANIE SERII ---");
        Console.WriteLine("Dostępne Ćwiczenia:");
        foreach (var e in data.Exercises)
        {
            Console.WriteLine($"- ID {e.Id}: {e.Name} ({e.MuscleGroup})");
        }

        Console.Write("Podaj ID Ćwiczenia (lub 0, aby zakończyć): ");
        int exerciseId = int.Parse(Console.ReadLine());

        if (exerciseId != 0)
        {
            Exercise exercise = data.GetExerciseById(exerciseId);
            if (exercise != null)
            {
                Console.Write("Liczba serii: ");
                int sets = int.Parse(Console.ReadLine());
                Console.Write("Liczba powtórzeń: ");
                int reps = int.Parse(Console.ReadLine());
                Console.Write("Użyty ciężar (kg): ");
                double weight = double.Parse(Console.ReadLine());

                newWorkout.Sets.Add(new Set(exercise, reps, sets, weight));
                Console.WriteLine($"  -> Dodano serię: {sets}x{reps} {exercise.Name} z ciężarem {weight}kg.");
            }
        }

        data.AddWorkout(newWorkout);
        client.PlannedWorkouts.Add(newWorkout);
        Console.WriteLine($"\n✅ Trening o ID {newWorkout.Id} zapisany i przypisany do {client.FirstName}.");
    }

    // --- METODY POMOCNICZE (ZOSTAWIAMY TYLKO DISPLAY MENU) ---

    private static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("=== SYSTEM ZARZĄDZANIA TRENINGAMI PERSONALNYMI ===");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("1. Dodaj nowego Klienta");
        Console.WriteLine("2. Dodaj nowego Trenera");
        Console.WriteLine("3. Dodaj nowe Ćwiczenie");
        Console.WriteLine("4. Dodaj nowy Trening");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("5. Raport: Grupowanie Klientów wg Celów (LINQ)");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("7. ZAMKNIJ APLIKACJĘ");
        Console.Write("\nWybierz opcję: ");
    }

    // Usunięto metody Value, ReadInt, ReadDouble, ReadDecimal zgodnie z prośbą.

    // --- FUNKCJA INICJALIZUJĄCA DANE TESTOWE ---

    private static void InitializeSampleData()
    {
        // Dodawanie Trenerów
        var trainer1 = new Trainer(1, "Kamil", "Ważny", "k.w@gym.com", "Strength", 120.00m);
        var trainer2 = new Trainer(2, "Eryk", "Wysoki", "e.w@gym.com", "Endurance", 100.00m);
        data.AddTrainer(trainer1);
        data.AddTrainer(trainer2);

        // Dodawanie Klientów
        var client1 = new Client(101, "Anna", "Wojcik", "a.w@client.com", 65.5, 1.70, "Mass Gain");
        var client2 = new Client(102, "Bartek", "Lis", "b.l@client.com", 90.0, 1.85, "Fat Loss");
        var client3 = new Client(103, "Cecylia", "Kruk", "c.k@client.com", 70.0, 1.65, "Mass Gain");
        data.AddClient(client1);
        data.AddClient(client2);
        data.AddClient(client3);

        // Dodawanie Ćwiczeń
        var ex1 = new Exercise(1, "Barbell Squat", "Legs");
        var ex2 = new Exercise(2, "Bench Press", "Chest");
        var ex3 = new Exercise(3, "Dumbbell Row", "Back");
        data.AddExercise(ex1);
        data.AddExercise(ex2);
        data.AddExercise(ex3);

        // Tworzenie Treningów
        var workout1 = new Workout(1001, DateTime.Now.Date.AddDays(-2), client1);
        workout1.Sets.Add(new Set(ex1, 10, 3, 60.0));
        workout1.Sets.Add(new Set(ex2, 8, 3, 50.0));
        data.AddWorkout(workout1);

        var workout2 = new Workout(1002, DateTime.Now.Date.AddDays(-1), client3);
        workout2.Sets.Add(new Set(ex1, 12, 4, 55.0));
        workout2.Sets.Add(new Set(ex3, 15, 3, 20.0));
        data.AddWorkout(workout2);

        // Przypisanie treningów do klientów
        client1.PlannedWorkouts.Add(workout1);
        client3.PlannedWorkouts.Add(workout2);

        Console.WriteLine($"Zainicjowano: {data.Clients.Count} klientów, {data.Trainers.Count} trenerów.");
    }
}