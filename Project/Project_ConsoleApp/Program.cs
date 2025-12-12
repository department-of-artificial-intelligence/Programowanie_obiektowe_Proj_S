using System;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.DAL;   
using Project.Model;

namespace Project
{
    public class TutoringSystemApp
    {

        private static SystemManager? manager;

        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Pobranie ConnectionString z appsettings.json
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    // Rejestracja bazy danych
                    services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
                    // Rejestracja SystemManagera
                    services.AddScoped<SystemManager>();
                })
                .Build();

            // 2. Uruchomienie zakresu do pracy z bazą
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Pobieramy kontekst bazy, aby upewnić się, że baza istnieje
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // To stworzy bazę danych, jeśli jej nie ma
                    context.Database.EnsureCreated();

                    // Pobieramy naszego managera, który ma już wstrzykniętą bazę
                    manager = services.GetRequiredService<SystemManager>();

                    //Załaduj dane startowe tylko jeśli baza jest pusta
                    if (!context.Tutors.Any())
                    {
                        SetupInitialData();
                    }

                    // 3. Uruchomienie pętli programu
                    RunMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wystąpił błąd podczas uruchamiania: {ex.Message}");
                }
            }
        }

        private static void RunMenu()
        {
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine("-----------------------------");

                try
                {
                    switch (choice)
                    {
                        case "1": AddTutorMenu(); break;
                        case "2": AddStudentMenu(); break;
                        case "3": AddTimeSlotMenu(); break;
                        case "4": BookLessonMenu(); break;
                        case "5": DisplayAllLessons(); break;
                        case "6": GenerateReportsMenu(); break;
                        case "0":
                            running = false;
                            Console.WriteLine("Zamykanie systemu. Do zobaczenia!");
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd operacji: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("Naciśnij Enter, aby kontynuować");
                    Console.ReadLine();
                }
            }
        }

        // --- Menu----------------

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("============SYSTEM KOREPETYCJI (SQL)============");
            Console.WriteLine("1. Dodaj nowego Korepetytora");
            Console.WriteLine("2. Dodaj nowego Ucznia");
            Console.WriteLine("3. Dodaj wolny termin Korepetytorowi");
            Console.WriteLine("4. Zarezerwuj lekcję");
            Console.WriteLine("5. Wyświetl rezerwacje lekcji");
            Console.WriteLine("6. Wyświetl raport");
            Console.WriteLine("0. Zakończ");
            Console.WriteLine("------------------------------------------");
            Console.Write("Wybierz opcję: ");
        }

        private static bool IsNotEmpty(string? text) => !string.IsNullOrWhiteSpace(text);

        private static void AddTutorMenu()
        {
            Console.Write("Imię: "); string? first = Console.ReadLine();
            Console.Write("Nazwisko: "); string? last = Console.ReadLine();
            Console.Write("Email: "); string? email = Console.ReadLine();
            Console.Write("Stawka za godzinę: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rate)) { Console.WriteLine("Błędna stawka!"); return; }

            if (!IsNotEmpty(first) || !IsNotEmpty(last) || !IsNotEmpty(email)) { Console.WriteLine("Niepoprawne dane!"); return; }

            var tutor = manager!.AddTutor(first!, last!, email!, rate);

            Console.WriteLine("--- Dostępne przedmioty ---");
            foreach (var s in manager.GetSubjects())
            {
                Console.WriteLine(s);
            }

            Console.Write("Podaj ID specjalizacji (lub Enter aby pominąć): ");
            string? subjectInput = Console.ReadLine();
            if (int.TryParse(subjectInput, out int subjectId))
            {
                var subject = manager.GetSubjects().FirstOrDefault(s => s.Id == subjectId);
                if (subject != null)
                {
                    manager.AddSpecialtyToTutor(tutor.Id, subject);
                    Console.WriteLine($"Dodano specjalizację: {subject.Name}");
                }
            }
            Console.WriteLine($"Zapisano korepetytora ID: {tutor.Id}");
        }

        private static void AddStudentMenu()
        {
            Console.Write("Imię: "); string? first = Console.ReadLine();
            Console.Write("Nazwisko: "); string? last = Console.ReadLine();
            Console.Write("Email: "); string? email = Console.ReadLine();
            Console.Write("Poziom edukacji: "); string? level = Console.ReadLine();

            if (!IsNotEmpty(first) || !IsNotEmpty(last) || !IsNotEmpty(level)) { Console.WriteLine("Dane niekompletne."); return; }

            var student = manager!.AddStudent(first!, last!, email!, level!);
            Console.WriteLine($"Dodano studenta ID: {student.Id}");
        }

        private static void AddTimeSlotMenu()
        {
            Console.WriteLine("Dostępni korepetytorzy:");
            foreach (var t in manager!.GetTutors()) Console.WriteLine(t);

            Console.Write("ID korepetytora: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var tutor = manager.GetTutors().FirstOrDefault(t => t.Id == id);
            if (tutor == null) { Console.WriteLine("Nie znaleziono korepetytora!"); return; }

            Console.Write("Start (rrrr-mm-dd hh:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime start)) return;
            Console.Write("Koniec (rrrr-mm-dd hh:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime end)) return;

            var slot = manager.AddTimeSlot(tutor, start, end);
            Console.WriteLine("Dodano termin ID: " + slot.Id);
        }

        private static void BookLessonMenu()
        {

            Console.WriteLine("=== Korepetytorzy ===");
            foreach (var t in manager!.GetTutors()) Console.WriteLine(t);
            Console.Write("ID Korepetytora: ");
            if (!int.TryParse(Console.ReadLine(), out int tId)) return;
            var tutor = manager.GetTutors().FirstOrDefault(t => t.Id == tId);
            if (tutor == null) return;

            Console.WriteLine("=== Uczniowie ===");
            foreach (var s in manager.GetStudents()) Console.WriteLine(s);
            Console.Write("ID Ucznia: ");
            if (!int.TryParse(Console.ReadLine(), out int sId)) return;
            var student = manager.GetStudents().FirstOrDefault(s => s.Id == sId);
            if (student == null) return;

            Console.WriteLine($"=== Przedmioty, których uczy {tutor.LastName} {tutor.FirstName} ===");
            if (!tutor.Specialties.Any()) {
                Console.WriteLine("Ten nauczyciel nie ma przypisanych żadnych przedmiotów!");
                return;
            }
            foreach (var sub in tutor.Specialties) {
                Console.WriteLine(sub);
            }
            Console.Write("ID Przedmiotu: ");
            if (!int.TryParse(Console.ReadLine(), out int subId)) return;
            var subject = manager.GetSubjects().FirstOrDefault(s => s.Id == subId);

            Console.WriteLine("=== Dostępne terminy ===");
            foreach (var slot in tutor.Availability.Where(a => !a.IsBooked)) Console.WriteLine(slot);
            Console.Write("ID Terminu: ");
            if (!int.TryParse(Console.ReadLine(), out int slotId)) return;
            var timeSlot = tutor.Availability.FirstOrDefault(a => a.Id == slotId);

            if (timeSlot == null || subject == null) { Console.WriteLine("Błąd danych."); return; }

            var reservation = manager.BookLesson(tutor, student, subject, timeSlot);
            if (reservation != null)
                Console.WriteLine("Zarezerwowano! ID: " + reservation.Id);
        }

        private static void DisplayAllLessons()
        {
            Console.WriteLine("=== Lekcje ===");
            foreach (var l in manager!.GetLessons()) Console.WriteLine(l);
            Console.WriteLine("\n=== Rezerwacje ===");
            foreach (var r in manager.GetReservations()) Console.WriteLine($"Rezerwacja {r.Id} [Status: {r.Status}]: {r.Lesson}");
        }

        private static void GenerateReportsMenu()
        {
            Raport.ShowTutorsByRate(manager!);
            Console.WriteLine();
            Raport.ShowSubjectPopularity(manager!);
        }

        private static void SetupInitialData()
        {

            var math = manager!.AddSubject("Matematyka", "Algebra, geometria");
            var physics = manager.AddSubject("Fizyka", "Mechanika");
            var english = manager.AddSubject("Angielski", "B2/C1");

            var t1 = manager.AddTutor("Jan", "Kowalski", "jan@test.com", 80);
            manager.AddSpecialtyToTutor(t1.Id, math);

            var s1 = manager.AddStudent("Anna", "Nowak", "anna@test.com", "Liceum");
            var slot = manager.AddTimeSlot(t1, DateTime.Now.AddDays(1).Date.AddHours(10), DateTime.Now.AddDays(1).Date.AddHours(11));

            Console.WriteLine("Dane startowe załadowane!");
        }
    }
}