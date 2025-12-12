using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using System;
using System.Linq;
using System.Collections.Generic;

// --- 1. Konfiguracja i budowanie hosta (DI) ---

IHost _host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Pobieranie Connection String z appsettings.json
        var cns = context.Configuration.GetConnectionString("DefaultConnection");

        // A. REJESTRACJA ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(cns));

        // B. REJESTRACJA serwisów (DataManagement używa DbContext, ReportGenerator logiki)
        services.AddScoped<DataManagement>();
        services.AddScoped<ReportGenerator>();
    })
    .Build();

// --- 2. LOGIKA STARTOWA (Migracja i Seed Data) ---

// Operacje na DbContext muszą być w zakresie (Scope)
using (var scope = _host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dataManager = services.GetRequiredService<DataManagement>();

    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        // Uruchamia migracje, tworząc/aktualizując tabele w bazie
        dbContext.Database.Migrate();
        Console.WriteLine("✅ Baza danych zmigrowana pomyślnie.");

        // Inicjalizacja danych startowych TYLKO jeśli baza jest pusta
        if (!dataManager.Clients.Any())
        {
            Console.WriteLine("Inicjalizacja danych startowych...");
            InitializeSampleData(dataManager);
            Console.WriteLine("✅ Dane startowe dodane do bazy.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Wystąpił krytyczny błąd podczas startu lub migracji: {ex.Message}");
        return;
    }

    // 3. Uruchomienie głównego menu konsolowego
    var reports = services.GetRequiredService<ReportGenerator>();
    RunApplication(dataManager, reports);
}

// ----------------------------------------------------------------------------------
// --- Definicje Metod Używane w Aplikacji (przeniesione z Twojego starego pliku) ---
// ----------------------------------------------------------------------------------

void RunApplication(DataManagement data, ReportGenerator reports)
{
    while (true)
    {
        DisplayMainMenu();
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": AddClientInteractive(data); break;
            case "2": AddTrainerInteractive(data); break;
            case "3": AddExerciseInteractive(data); break;
            case "4": AddWorkoutInteractive(data); break;
            case "5": reports.GroupClientsByGoal(data.Clients); break;
            case "6": reports.DisplayAllData(data.Clients, data.Trainers, data.Exercises, data.Workouts); break;
            case "7": Console.WriteLine("\nZamykanie aplikacji..."); return;
            default: Console.WriteLine("\nNieprawidłowy wybór. Spróbuj ponownie."); break;
        }
        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
}

void InitializeSampleData(DataManagement data)
{
    // Dodawanie Trenerów
    var trainer1 = new Trainer("Kamil", "Ważny", "k.w@gym.com", "Strength", 120.00m);
    var trainer2 = new Trainer("Eryk", "Wysoki", "e.w@gym.com", "Endurance", 100.00m);
    data.AddTrainer(trainer1);
    data.AddTrainer(trainer2);

    // Dodawanie Klientów
    var client1 = new Client("Anna", "Wojcik", "a.w@client.com", 65.5, 1.70, "Mass Gain");
    var client2 = new Client("Bartek", "Lis", "b.l@client.com", 90.0, 1.85, "Fat Loss");
    var client3 = new Client("Cecylia", "Kruk", "c.k@client.com", 70.0, 1.65, "Mass Gain");
    data.AddClient(client1);
    data.AddClient(client2);
    data.AddClient(client3);

    // Dodawanie Ćwiczeń
    var ex1 = new Exercise("Barbell Squat", "Legs");
    var ex2 = new Exercise("Bench Press", "Chest");
    var ex3 = new Exercise("Dumbbell Row", "Back");
    data.AddExercise(ex1);
    data.AddExercise(ex2);
    data.AddExercise(ex3);

    // Tworzenie Treningów
    var workout1 = new Workout(DateTime.Now.Date.AddDays(-2), client1);
    // UWAGA: Zmieniliśmy Set.cs, aby działał z bazą danych, musimy używać nowych danych (z kluczem)
    workout1.Sets.Add(new Set(ex1, 10, 3, 60.0));
    workout1.Sets.Add(new Set(ex2, 8, 3, 50.0));
    data.AddWorkout(workout1);

    var workout2 = new Workout(DateTime.Now.Date.AddDays(-1), client3);
    workout2.Sets.Add(new Set(ex1, 12, 4, 55.0));
    workout2.Sets.Add(new Set(ex3, 15, 3, 20.0));
    data.AddWorkout(workout2);

    Console.WriteLine($"Zainicjowano: {data.Clients.Count} klientów, {data.Trainers.Count} trenerów.");
}

void DisplayMainMenu()
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
    Console.WriteLine("6. Raport: wszystkie dane");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("7. ZAMKNIJ APLIKACJĘ");
    Console.Write("\nWybierz opcję: ");
}

void AddClientInteractive(DataManagement data)
{
    Console.WriteLine("\n--- DODAWANIE NOWEGO KLIENTA ---");
    Console.Write("Podaj imię: ");
    string firstName = Console.ReadLine();
    Console.Write("Podaj nazwisko: ");
    string lastName = Console.ReadLine();
    Console.Write("Podaj email: ");
    string email = Console.ReadLine();

    Console.Write("Podaj wagę (kg): ");
    double weight = double.Parse(Console.ReadLine());

    Console.Write("Podaj wzrost (m): ");
    double height = double.Parse(Console.ReadLine());

    Console.Write("Podaj cel treningowy (np. redukcja, masa): ");
    string goal = Console.ReadLine();

    var newClient = new Client(firstName, lastName, email, weight, height, goal);
    data.AddClient(newClient); // Zapis do bazy
    Console.WriteLine($"\n Dodano klienta: {newClient.FirstName} {newClient.LastName} (ID: {newClient.Id})");
}

void AddTrainerInteractive(DataManagement data)
{
    Console.WriteLine("\n--- DODAWANIE NOWEGO TRENERA ---");

    Console.Write("Podaj imię: ");
    string firstName = Console.ReadLine();
    Console.Write("Podaj nazwisko: ");
    string lastName = Console.ReadLine();
    Console.Write("Podaj email: ");
    string email = Console.ReadLine();

    Console.Write("Podaj specjalizację: ");
    string specialization = Console.ReadLine();

    Console.Write("Podaj stawkę godzinową (PLN): ");
    decimal rate = decimal.Parse(Console.ReadLine());

    var newTrainer = new Trainer(firstName, lastName, email, specialization, rate);
    data.AddTrainer(newTrainer); // Zapis do bazy
    Console.WriteLine($"\n✅ Dodano trenera: {newTrainer.FirstName} {newTrainer.LastName} (ID: {newTrainer.Id})");
}

void AddExerciseInteractive(DataManagement data)
{
    Console.WriteLine("\n--- DODAWANIE NOWEGO ĆWICZENIA ---");

    Console.Write("Podaj nazwę ćwiczenia: ");
    string name = Console.ReadLine();
    Console.Write("Podaj partię mięśniową: ");
    string muscleGroup = Console.ReadLine();

    var newExercise = new Exercise(name, muscleGroup);
    data.AddExercise(newExercise); // Zapis do bazy
    Console.WriteLine($"\n Dodano ćwiczenie: {newExercise.Name} (ID: {newExercise.Id})");
}

void AddWorkoutInteractive(DataManagement data)
{
    Console.WriteLine("\n--- DODAJ NOWY TRENING ---");

    // 1. Wybór Klienta
    List<Client> availableClients = data.Clients; // Pobiera Klientów z bazy
    Console.WriteLine("Dostępni Klienci:");
    if (!availableClients.Any())
    {
        Console.WriteLine("Brak klientów do przypisania!");
        return;
    }
    foreach (var c in availableClients)
    {
        Console.WriteLine($"- ID {c.Id}: {c.FirstName} {c.LastName}");
    }

    Console.Write("Podaj ID Klienta dla treningu: ");
    int clientId = int.Parse(Console.ReadLine());
    Client client = data.GetClientById(clientId);

    if (client == null)
    {
        Console.WriteLine("Klient o podanym ID nie istnieje.");
        return;
    }

    var newWorkout = new Workout(DateTime.Now.Date, client);
    Console.WriteLine($"\n✅ Tworzenie treningu dla {client.FirstName} {client.LastName}...");

    // 2. Dodawanie Serii Ćwiczeń
    Console.WriteLine("\n--- DODAWANIE SERII ---");
    List<Exercise> availableExercises = data.Exercises; // Pobiera Ćwiczenia z bazy
    Console.WriteLine("Dostępne Ćwiczenia:");
    foreach (var e in availableExercises)
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

            // UWAGA: Musisz pobrać Exercise, aby Series miało referencję do obiektu Exercise
            newWorkout.Sets.Add(new Set(exercise, reps, sets, weight));
            Console.WriteLine($"  -> Dodano serię: {sets}x{reps} {exercise.Name} z ciężarem {weight}kg.");
        }
    }

    data.AddWorkout(newWorkout); // Zapis do bazy
    Console.WriteLine($"\n Trening o ID {newWorkout.Id} zapisany i przypisany do {client.FirstName}.");
}