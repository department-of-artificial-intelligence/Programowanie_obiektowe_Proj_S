using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using System;
using System.Linq;
using System.Collections.Generic;

//budujemy hosta

IHost _host = Host.CreateDefaultBuilder(args)
  .ConfigureServices((context, services) =>
  {
      // Pobieranie Connection String z appsettings.json
      var cns = context.Configuration.GetConnectionString("DefaultConnection");

      //////////////////////////wstrzykujemy baze danych
     services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cns));

      // REJESTRACJA serwisów 
      services.AddScoped<DataManagement>();
      services.AddScoped<ReportGenerator>();
      services.AddScoped<BookingManager>(); 
  })
  .Build();

// --- przygotowanie i sprawdzenie stanu magazynu

using (var scope = _host.Services.CreateScope())
{
    var services = scope.ServiceProvider;//lokazlizaja usługi
    var dataManager = services.GetRequiredService<DataManagement>();// pobieranie wymaganej usługi
    //sprawdzenie czy baza działa
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        // usuwanie bazydanych dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
        Console.WriteLine("Baza danych zmigrowana pomyślnie.");

        if (!dataManager.Clients.Any())//jeśłi baza jest pusta wrzuć podstawowe dane startowe
        {
            Console.WriteLine("Inicjalizacja danych startowych...");
            InitializeSampleData(dataManager);
            Console.WriteLine("Dane startowe dodane do bazy. Baza jest gotowa do pracy.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Wystąpił krytyczny błąd podczas startu lub migracji: {ex.Message}");
        return;
    }

    // 3. Uruchomienie głównego menu konsolowego
    var reports = services.GetRequiredService<ReportGenerator>();
    var bookingManager = services.GetRequiredService<BookingManager>(); // pobranie serwisu
    RunApplication(dataManager, reports, bookingManager); 
}

// Menu obsługa
void RunApplication(DataManagement data, ReportGenerator reports, BookingManager booking)
{
    while (true)
    {
        DisplayMainMenu();
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": AddClientInteractive(data); break;
            case "2": AddTrainerInteractive(data, booking); break;
            case "3": AddExerciseInteractive(data); break;
            case "4": AddWorkoutInteractive(data); break;
            case "5": BookSessionInteractive(data, booking); break; 
            case "6": reports.GroupClientsByGoal(data.Clients); break;
            case "7": reports.DisplayAllData(data.Clients, data.Trainers, data.Exercises, data.Workouts); break;
            case "8": Console.WriteLine("Zamykanie aplikacji..."); return;
            default: Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie."); break;
        }
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
}
// Wyświetla główne opcje menu konsoli.
void DisplayMainMenu()
{
    Console.Clear();
    Console.WriteLine("=== SYSTEM ZARZĄDZANIA TRENINGAMI PERSONALNYMI ===");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("1. Dodaj nowego Klienta");
    Console.WriteLine("2. Dodaj nowego Trenera");
    Console.WriteLine("3. Dodaj nowe Ćwiczenie");
    Console.WriteLine("4. Dodaj nowy Trening (Wykonany Workout)");
    Console.WriteLine("5. ZAREZERWUJ SESJĘ PERSONALNĄ (Reservation)");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("6. Raport: Grupowanie Klientów według celu");
    Console.WriteLine("7. Raport: wszystkie dane");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("8. ZAMKNIJ APLIKACJĘ"); 
    Console.Write("Wybierz opcję: ");
}

// Inicjowanie danych startowych
void InitializeSampleData(DataManagement data)
{
    Console.WriteLine("Inicjalizacja zakończona. Baza danych nie zawiera startowych Klientów, Trenerów ani Treningów.");
}



// Interaktywna obsługa rezerwacji.
void BookSessionInteractive(DataManagement data, BookingManager booking)
{
    Console.WriteLine("\n--- REZERWACJA SESJI PERSONALNEJ ---");

    // 1. Wybór Klienta
    Console.WriteLine("Dostępni Klienci:");
    data.Clients.ForEach(c => Console.WriteLine($"- ID {c.Id}: {c.FirstName} {c.LastName}"));
    Console.Write("Podaj ID Klienta: ");
    if (!int.TryParse(Console.ReadLine(), out int clientId)) return;

    // 2. Wybór Trenera i wyświetlenie jego dostępności
    Console.WriteLine("Dostępni Trenerzy:");
    data.Trainers.ForEach(t => Console.WriteLine($"- ID {t.Id}: {t.FirstName} {t.LastName} ({t.Specialization})"));
    Console.Write("Podaj ID Trenera: ");
    if (!int.TryParse(Console.ReadLine(), out int trainerId)) return;

    // Używamy metody GetTrainerById
    Trainer trainer = data.GetTrainerById(trainerId);
    if (trainer == null) { Console.WriteLine("Trener o podanym ID nie istnieje."); return; }

    // Wyświetlenie wolnych slotów
    booking.DisplayTrainerAvailability(trainer);

    // Wybór Terminu
    Console.Write("Podaj datę i godzinę rezerwacji (YYYY-MM-DD HH:MM): ");
    if (DateTime.TryParse(Console.ReadLine(), out DateTime slot))
    {
        // Wywołanie logiki rezerwacji w BookingManager
        booking.BookSession(clientId, trainerId, slot);
    }
    else
    {
        Console.WriteLine("Nieprawidłowy format daty/godziny.");
    }
}

// Interaktywne dodawanie nowego Klienta.
void AddClientInteractive(DataManagement data)
{
    Console.Write("\nPodaj imię Klienta: ");
    string firstName = Console.ReadLine();
    Console.Write("Podaj nazwisko Klienta: ");
    string lastName = Console.ReadLine();
    Console.Write("Podaj email: ");
    string email = Console.ReadLine();
    Console.Write("Podaj wagę (np. 75.5): ");
    if (!double.TryParse(Console.ReadLine(), out double weight)) return;
    Console.Write("Podaj wzrost (np. 1.80): ");
    if (!double.TryParse(Console.ReadLine(), out double height)) return;
    Console.Write("Podaj cel treningowy (masa,redukcja,wydolnosc): ");
    string goal = Console.ReadLine();

    var client = new Client(firstName, lastName, email, weight, height, goal);
    data.AddClient(client);
    Console.WriteLine($"Klient {client.FirstName} {client.LastName} dodany pomyślnie.");
}

// Interaktywne dodawanie nowego Trenera i jego wolnych terminów.
void AddTrainerInteractive(DataManagement data, BookingManager booking)
{
    Console.Write("\nPodaj imię Trenera: ");
    string firstName = Console.ReadLine();
    Console.Write("Podaj nazwisko Trenera: ");
    string lastName = Console.ReadLine();
    Console.Write("Podaj email: ");
    string email = Console.ReadLine();
    Console.Write("Podaj specjalizację (np.trójbój,cross,kalistenika,pilates): ");
    string specialization = Console.ReadLine();
    Console.Write("Podaj stawkę godzinową (np. 150.00): ");

    // Zakończenie metody w przypadku błędu
    if (!decimal.TryParse(Console.ReadLine(), out decimal rate))
    {
        Console.WriteLine(" Nieprawidłowy format stawki. Anulowano.");
        return;
    }

    // 1. Zapis trenera do bazy danych (uzyskanie ID)
    var trainer = new Trainer(firstName, lastName, email, specialization, rate);
    data.AddTrainer(trainer);
    Console.WriteLine($"Trener {trainer.FirstName} {trainer.LastName} dodany pomyślnie (ID: {trainer.Id}).");

    // 2.Sprawdzanie poprawności dodawania dat
    Console.WriteLine("--- DODAJ WOLNY TERMIN TRENERA (HH:MM) ---");

    bool addingSlots = true;
    while (addingSlots)
    {
        Console.Write("Podaj datę i godzinę wolnego slotu (YYYY-MM-DD HH:MM) lub wpisz 'k' aby zakończyć: ");
        string input = Console.ReadLine();

        // Sprawdzenie warunku wyjścia z pętli, usuwa spacje i zmienia na małe litery
        if (input != null && input.Trim().ToLower() == "q")
        {
            addingSlots = false;
            continue;
        }

        if (DateTime.TryParse(input, out DateTime slot))
        {
        
            if (slot < DateTime.Now)
            {
                Console.WriteLine("Ostrzeżenie: Nie można dodać terminu w przeszłości.");
                continue;
            }

            // Wywołanie metody w BookingManager
            booking.AddTrainerSlot(trainer.Id, slot);
        }
        else
        {
            Console.WriteLine("Nieprawidłowy format daty/godziny. Spróbuj YYYY-MM-DD HH:MM.");
        }
    }
}

//Dodawanie nowego Ćwiczenia.
void AddExerciseInteractive(DataManagement data)
{
    Console.Write("Podaj nazwę Ćwiczenia: ");
    string name = Console.ReadLine();
    Console.Write("Podaj partię mięśniową: ");
    string muscleGroup = Console.ReadLine();

    var exercise = new Exercise(name, muscleGroup);
    data.AddExercise(exercise);
    Console.WriteLine($"Ćwiczenie '{exercise.Name}' dodane pomyślnie.");
}

//dodawanie Treningu
void AddWorkoutInteractive(DataManagement data)
{
    Console.WriteLine("--- DODAWANIE NOWEGO WYKONANEGO TRENINGU ---");
    Console.WriteLine("Dostępni Klienci:");
    data.Clients.ForEach(c => Console.WriteLine($"- ID {c.Id}: {c.FirstName} {c.LastName}"));
    Console.Write("Podaj ID Klienta: ");
    if (!int.TryParse(Console.ReadLine(), out int clientId)) return;

    var client = data.GetClientById(clientId);
    if (client == null) { Console.WriteLine("Klient o podanym ID nie istnieje."); return; }

    var workout = new Workout(DateTime.Now.Date, client);

    //  Dodawanie Serii
    bool addingSets = true;
    while (addingSets)
    {
        Console.WriteLine("Dostępne Ćwiczenia (ID | Nazwa):");
        data.Exercises.ForEach(e => Console.WriteLine($"- ID {e.Id}: {e.Name}"));
        Console.Write("Podaj ID Ćwiczenia do dodania (lub 'q' aby zakończyć): ");
        string input = Console.ReadLine();
        if (input.ToLower() == "q")
        {
            addingSets = false;
            continue;
        }
        // sprawdzmay czy uzytkownik podaje liczbe
        if (int.TryParse(input, out int exerciseId))
        {
            var exercise = data.GetExerciseById(exerciseId);
            if (exercise == null) { Console.WriteLine("Ćwiczenie o podanym ID nie istnieje."); continue; }

            Console.Write("Ilość powtórzeń: ");
            if (!int.TryParse(Console.ReadLine(), out int reps)) continue;
            Console.Write("Ilość serii: ");
            if (!int.TryParse(Console.ReadLine(), out int sets)) continue;
            Console.Write("Użyty ciężar (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double weight)) continue;

            // Tworzymy nową Serię
            var newSet = new Set(exercise, reps, sets, weight);

            workout.Sets.Add(newSet);
            Console.WriteLine($" Dodano serię: {exercise.Name}");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy wybór ID.");
        }
    }

    if (workout.Sets.Any())
    {
        data.AddWorkout(workout);
        Console.WriteLine($"Trening z {workout.Sets.Count} seriami dodany pomyślnie do bazy.");
    }
    else
    {
        Console.WriteLine("Nie dodano żadnych serii. Trening anulowany.");
    }
}