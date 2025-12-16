using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;



// Tworzenie hosta
IHost _host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        var cs = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(cs));
    }).Build();

// Pobranie instancji kontekstu bazy danych z kontenera usług
var context = _host.Services.GetService<ApplicationDbContext>();

if (context != null)
{
    context.Database.EnsureCreated();
    
    Console.WriteLine("Baza danych przygotowana pomyślnie.");
}
else
{
    Console.WriteLine("Błąd: DbContext jest nullem.");
}
//łączymy surowy dostep do danych z konkretnymi funkcjami programu
var dataManager = new DataManagement(context!);
var reports = new ReportGenerator();
var bookingManager = new BookingManager(context!);

RunApplication(dataManager, reports, bookingManager);

void RunApplication(DataManagement data, ReportGenerator reports, BookingManager booking)
{
    while (true)
    {
        DisplayMainMenu();
        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1": AddClientInteractive(data); break;
            case "2": AddTrainerInteractive(data, booking); break;
            case "3": AddExerciseInteractive(data); break;
            case "4": AddWorkoutInteractive(data); break;
            case "5": BookSessionInteractive(data, booking); break;
            case "6": reports.GroupClientsByGoal(data.Clients); break;
            case "7":
                reports.DisplayAllData(
                    data.Clients,
                    data.Trainers,
                    data.Exercises,
                    data.Workouts,
                    data.Reservations);
                break;
            case "8":
                Console.WriteLine("Zamykanie aplikacji...");
                return;
            default:
                Console.WriteLine("Nieprawidłowy wybór.");
                break;
        }

        Console.WriteLine("Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }
}

void DisplayMainMenu()
{
    Console.Clear();
    Console.WriteLine("=== SYSTEM ZARZĄDZANIA TRENINGAMI PERSONALNYMI ===");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("1. Dodaj Klienta");
    Console.WriteLine("2. Dodaj Trenera");
    Console.WriteLine("3. Dodaj Ćwiczenie");
    Console.WriteLine("4. Dodaj Trening");
    Console.WriteLine("5. Zarezerwuj Sesję");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("6. Raport: Klienci wg celu");
    Console.WriteLine("7. Raport: Wszystkie dane");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("8. Wyjście");
    Console.Write("Wybór: ");
}


//operacje
void AddClientInteractive(DataManagement data)
{
    Console.Write("Imię: ");
    string firstName = Console.ReadLine()!;
    Console.Write("Nazwisko: ");
    string lastName = Console.ReadLine()!;
    Console.Write("Email: ");
    string email = Console.ReadLine()!;
    Console.Write("Waga: (np 120)kg ");
    if (!double.TryParse(Console.ReadLine(), out double weight)) return;
    Console.Write("Wzrost:(np 165)");
    if (!double.TryParse(Console.ReadLine(), out double height)) return;
    Console.Write("Cel:(masa,redukcja,wytrzymalosc) ");
    string goal = Console.ReadLine()!;

    var client = new Client(firstName, lastName, email, weight, height, goal);
    data.AddClient(client);
}

void AddTrainerInteractive(DataManagement data, BookingManager booking)
{
    Console.Write("Imię: ");
    string firstName = Console.ReadLine()!;
    Console.Write("Nazwisko: ");
    string lastName = Console.ReadLine()!;
    Console.Write("Email: ");
    string email = Console.ReadLine()!;
    Console.Write("Specjalizacja:(np cross,pilates,trojboj) ");
    string specialization = Console.ReadLine()!;
    Console.Write("Stawka:(np 120) ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal rate)) return;

    var trainer = new Trainer(firstName, lastName, email, specialization, rate);
    data.AddTrainer(trainer);

    while (true)
    {
        Console.Write("Wolny termin (YYYY-MM-DD HH:MM) lub 'q': ");
        string input = Console.ReadLine()!;
        if (input?.ToLower() == "q") break;

        if (DateTime.TryParse(input, out DateTime slot))
        { 
            if (slot >= DateTime.Now)
            {
                booking.AddTrainerSlot(trainer.Id, slot);
            }
            else
            {
                Console.WriteLine("Błąd: Nie można dodać wolnego terminu z przeszłości!");
            }
        }
        else
        {
            Console.WriteLine("Błąd: Nieprawidłowy format daty.");
        }
    }
}

void AddExerciseInteractive(DataManagement data)
{
    Console.Write("Nazwa: ");
    string name = Console.ReadLine()!;
    Console.Write("Partia: ");
    string muscleGroup = Console.ReadLine()!;

    data.AddExercise(new Exercise(name, muscleGroup));
}

void AddWorkoutInteractive(DataManagement data)
{
    Console.WriteLine("Klienci:");
    data.Clients.ForEach(c => Console.WriteLine($"{c.Id}: {c.FirstName} {c.LastName}"));
    Console.Write("Wybierz ID klienta: ");
    if (!int.TryParse(Console.ReadLine(), out int clientId)) return;

    var client = data.GetClientById(clientId);
    if (client == null)
    {
        Console.WriteLine("Nie znaleziono klienta.");
        return;
    }

    DateTime workoutDate;
    while (true)
    {
        Console.Write("Podaj datę treningu (YYYY-MM-DD HH:MM): ");
        // Teraz sprawdzamy tylko, czy format daty jest poprawny
        if (DateTime.TryParse(Console.ReadLine(), out workoutDate))
        {
            break; // Data jest poprawna, wychodzimy z pętli bez sprawdzania czy jest "stara"
        }
        else
        {
            Console.WriteLine("Błąd: Nieprawidłowy format daty.");
        }
    }

    var workout = new Workout(workoutDate, client);
    while (true)
    {
        Console.WriteLine("Ćwiczenia:");
        data.Exercises.ForEach(e => Console.WriteLine($"{e.Id}: {e.Name}"));
        Console.Write("ID ćwiczenia lub 'q' aby zakończyć: ");
        string input = Console.ReadLine()!;
        if (input?.ToLower() == "q") break;

        if (int.TryParse(input, out int exId))
        {
            var ex = data.GetExerciseById(exId);
            if (ex == null)
            {
                Console.WriteLine("Nie znaleziono ćwiczenia.");
                continue;
            }

            Console.Write("Powtórzenia: ");
            int.TryParse(Console.ReadLine(), out int reps);
            Console.Write("Serie: ");
            int.TryParse(Console.ReadLine(), out int sets);
            Console.Write("Ciężar (kg): ");
            double.TryParse(Console.ReadLine(), out double weight);
            workout.Sets.Add(new Set(ex, reps, sets, weight));
        }
    }

    if (workout.Sets.Any())
    {
        data.AddWorkout(workout);
        Console.WriteLine("Trening został pomyślnie zapisany.");
    }
    else
    {
        Console.WriteLine("Nie dodano żadnych ćwiczeń, trening nie został zapisany.");
    }
}

void BookSessionInteractive(DataManagement data, BookingManager booking)
{
    Console.WriteLine("Trenerzy:");
    data.Trainers.ForEach(t => Console.WriteLine($"{t.Id}: {t.FirstName} {t.LastName}"));
    if (!int.TryParse(Console.ReadLine(), out int trainerId)) return;

    var trainer = data.GetTrainerById(trainerId);
    if (trainer == null) 
            {
        Console.WriteLine("Błąd: Nie znaleziono trenera o takim ID.");
        return;
    }

    booking.DisplayTrainerAvailability(trainer);

    Console.Write("Data rezerwacji (YYYY-MM-DD HH:MM): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime slot)) return;

    Console.WriteLine("Klienci:");
    data.Clients.ForEach(c => Console.WriteLine($"{c.Id}: {c.FirstName} {c.LastName}"));
    Console.Write("ID Klienta: ");
    if (!int.TryParse(Console.ReadLine(), out int clientId)) return;

    booking.BookSession(clientId, trainerId, slot);
}
