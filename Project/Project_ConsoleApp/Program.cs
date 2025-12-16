using System;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.DAL;   
using Project.Model;
using Microsoft.Extensions.Logging;

namespace Project
{
    public class TutoringSystemApp
    {

        private static IUserService? _userService;
        private static ICatalogService? _catalogService;
        private static IBookingService? _bookingService;

        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging=>
                { //Wyłączenie logów w konsoli, tylko błędy
                    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Error);
                } )

                .ConfigureServices((context, services) =>
                {
                    // pobranie ConnectionString z appsettings.json
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    // Rejestracja bazy danych
                    services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<ICatalogService, CatalogService>();
                    services.AddScoped<IBookingService, BookingService>();
                })
                .Build();

            //Uruchomienie zakresu do pracy z bazą
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //pobranie kontekstu bazy
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    // migracja danych
                    context.Database.Migrate();

                    _userService = services.GetRequiredService<IUserService>();
                    _catalogService= services.GetRequiredService<ICatalogService>();
                    _bookingService= services.GetRequiredService<IBookingService>();

                    if (!context.Tutors.Any())
                    {
                        SetupInitialData();
                    }
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
                            Console.WriteLine("Zamykanie systemu.");
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
            Console.WriteLine("============ SYSTEM KOREPETYCJI ============");
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
        private static bool IsEmpty(string? text){
            return string.IsNullOrWhiteSpace(text);
        }
        private static void AddTutorMenu()
        {
            Console.Write("Imię: "); 
            string? first = Console.ReadLine();
            Console.Write("Nazwisko: "); 
            string? last = Console.ReadLine();
            Console.Write("Email: "); 
            string? email = Console.ReadLine();
            Console.Write("Stawka za godzinę: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rate)) { 
                Console.WriteLine("Błędna stawka!"); 
                return; }
            if (IsEmpty(first) || IsEmpty(last) || IsEmpty(email)) { 
                Console.WriteLine("Niepoprawne dane!"); return; }

            var tutor = _userService!.AddTutor(first!, last!, email!, rate); //tworzenie korepetytora

            Console.WriteLine("--- Dostępne przedmioty ---");
            foreach (var s in _catalogService!.GetSubjects())
            {
                Console.WriteLine(s);
            }

            while (true)  //dodawanie specjalizacji
            {
                Console.Write("Podaj ID specjalizacji: ");
                string? subjectInput = Console.ReadLine();
                if (int.TryParse(subjectInput, out int subjectId))
                {
                    var subject = _catalogService.GetSubjects().FirstOrDefault(s => s.Id == subjectId);
                    if (subject != null)
                    {
                        _userService.AddSpecialtyToTutor(tutor.Id, subject);
                        Console.WriteLine($"Dodano specjalizację: {subject.Name}");
                        break;
                    }
                    else{
                        Console.WriteLine("Błąd: Nie znaleziono przedmiotu o takim ID.");
                    }
                }
                else{
                    Console.WriteLine("Błąd: To nie jest poprawna liczba.");
                }
            }
            Console.WriteLine($"Zapisano korepetytora ID: {tutor.Id}");

        }
        private static void AddStudentMenu()
        {
            Console.Write("Imię: "); 
            string? first = Console.ReadLine();
            Console.Write("Nazwisko: "); 
            string? last = Console.ReadLine();
            Console.Write("Email: "); 
            string? email = Console.ReadLine();
            Console.Write("Poziom edukacji: "); 
            string? level = Console.ReadLine();

            if (IsEmpty(first) || IsEmpty(last) || IsEmpty(level) || IsEmpty(email)) { 
                Console.WriteLine("Dane niekompletne."); return; 
            }
            var student = _userService!.AddStudent(first!, last!, email!, level!);
            Console.WriteLine($"Dodano studenta ID: {student.Id}");
        }
        private static void AddTimeSlotMenu()
        {
            Console.WriteLine("Dostępni korepetytorzy:");
            foreach (var t in _userService!.GetTutors()) { 
                Console.WriteLine(t); }

            Console.Write("ID korepetytora: ");
            if (!int.TryParse(Console.ReadLine(), out int id)){
                Console.WriteLine("Błędne ID");
                return;}

            var tutor = _userService!.GetTutors().FirstOrDefault(t => t.Id == id);
            if (tutor == null) { 
                Console.WriteLine("Nie znaleziono korepetytora"); 
                return; }

            Console.Write("Start (rrrr-mm-dd hh:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime start)){
                Console.WriteLine("Zły format daty");
                return;
            }
            Console.Write("Koniec (rrrr-mm-dd hh:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime end)){
                Console.WriteLine("Zły format daty");
                return;
            }
            var slot = _bookingService!.AddTimeSlot(tutor, start, end);
            Console.WriteLine("Dodano termin ID: " + slot.Id);
        }

        private static void BookLessonMenu()
        {

            Console.WriteLine("=== Korepetytorzy ===");
            foreach (var t in _userService!.GetTutors()) {
                string przedmioty; //łączenie w stringa przedmiotów których uczy korepetytor
                if (t.Specialties.Any()) {
                    przedmioty = string.Join(", ", t.Specialties.Select(s => s.Name));
                }
                else { przedmioty = "Brak przypisanych przedmiotów"; }
                Console.WriteLine($"{t.Id}: {t.FirstName} {t.LastName} ({t.HourlyRate:C}/h)  Specjalizacje: {przedmioty}");
            }

            Console.Write("ID Korepetytora: ");
            if (!int.TryParse(Console.ReadLine(), out int tId))
                return;
            var tutor = _userService.GetTutors().FirstOrDefault(t => t.Id == tId);//szukanie
            if (tutor == null) return; //nie znaleziono

            Console.WriteLine("=== Uczniowie ===");
            foreach (var s in _userService.GetStudents()) {
                Console.WriteLine(s);
            }
            Console.Write("ID Ucznia: ");
            if (!int.TryParse(Console.ReadLine(), out int sId))
                return;
            var student = _userService.GetStudents().FirstOrDefault(s => s.Id == sId);
            if (student == null) return;//nie znaleziono

            Console.WriteLine($"=== Przedmioty, których uczy {tutor.LastName} {tutor.FirstName} ===");
            if (!tutor.Specialties.Any()) {
                Console.WriteLine("Ten nauczyciel nie ma przypisanych żadnych przedmiotów");
                return;
            }
            foreach (var sub in tutor.Specialties) {
                Console.WriteLine(sub);
            }

            Console.Write("ID Przedmiotu: ");
            if (!int.TryParse(Console.ReadLine(), out int subId))
                return;
            var subject = _catalogService!.GetSubjects().FirstOrDefault(s => s.Id == subId);

            Console.WriteLine("=== Dostępne terminy ===");
            foreach (var slot in tutor.Availability.Where(a => !a.IsBooked)) {
                Console.WriteLine(slot);
            }
            Console.Write("ID Terminu: ");
            if (!int.TryParse(Console.ReadLine(), out int slotId)) {
                Console.WriteLine("Błędne ID");
                return; }
            var timeSlot = tutor.Availability.FirstOrDefault(a => a.Id == slotId);

            if (timeSlot == null || subject == null) { 
                Console.WriteLine("Błąd danych."); 
                return; }

            Lesson? lesson = _bookingService!.BookLesson(tutor, student, subject, timeSlot);
            if (lesson != null)
                Console.WriteLine("Zarezerwowano! ID: " + lesson.Id);
            else
                Console.WriteLine("Nieudana rezerwacja ");
        }

        private static void DisplayAllLessons()
        {
            Console.WriteLine("=== Lekcje ===");
            foreach (var l in _bookingService!.GetLessons())
            {
                Console.WriteLine(l);
            }
        }
        private static void GenerateReportsMenu()
        {
            Raport.ShowTutorsByRate(_userService!);
            Console.WriteLine();
        }
        private static void SetupInitialData()
        {

            // Dodawanie przedmiotów
            var mat = _catalogService!.AddSubject("Matematyka", "Algebra, Geometria");
            var pol = _catalogService.AddSubject("Język Polski", "Literatura, Gramatyka");
            var fiz = _catalogService.AddSubject("Fizyka", "Mechanika, Twierdzenia fizycne");
            var ang = _catalogService.AddSubject("Język Angielski", "Konwersacje, Czasy angielskie");
            var geo = _catalogService.AddSubject("Geografia", "Mapy, Pogoda");
            //Dodawanie nauczycieli 
            var t1 = _userService!.AddTutor("Adam", "Nowak", "adam.nowak@test.com", 100);
            var t2 = _userService.AddTutor("Barbara", "Kowalska", "basia@test.com", 80);
            var t3 = _userService.AddTutor("Cezary", "Wiśniewski", "czarek@test.com", 70);
            var t4 = _userService.AddTutor("Dorota", "Wójcik", "dorota@test.com", 120);
            //Dodawanie specjalności
            _userService.AddSpecialtyToTutor(t1.Id, mat);
            _userService.AddSpecialtyToTutor(t1.Id, fiz);
            _userService.AddSpecialtyToTutor(t2.Id, pol);
            _userService.AddSpecialtyToTutor(t3.Id, geo);
            _userService.AddSpecialtyToTutor(t4.Id, ang);
            //Dodawanie studentow
            _userService.AddStudent("Marcin", "Lis", "marcinlis@student.com", "Liceum");
            _userService.AddStudent("Filip", "Najman", "filip@student.com", "Technikum");
            _userService.AddStudent("Kamil", "Piotrowski", "kamil@student.com", "Podstawówka");
            _userService.AddStudent("Jan", "Mazur", "jasiu@student.com", "Technikum");
            //Dodawanie wolnych terminów nauczycielom
            _bookingService!.AddTimeSlot(t1, new DateTime(2026, 1, 15, 16, 0, 0), new DateTime(2026, 1, 15, 17, 0, 0));
            _bookingService.AddTimeSlot(t1, new DateTime(2026, 2, 10, 16, 0, 0), new DateTime(2026, 2, 10, 17, 0, 0));

            _bookingService.AddTimeSlot(t2, new DateTime(2026, 1, 20, 10, 0, 0), new DateTime(2026, 1, 20, 11, 30, 0));
            _bookingService.AddTimeSlot(t2, new DateTime(2026, 2, 12, 12, 0, 0), new DateTime(2026, 2, 12, 13, 30, 0));

            _bookingService.AddTimeSlot(t3, new DateTime(2026, 1, 5, 18, 0, 0), new DateTime(2026, 1, 5, 19, 0, 0));
            _bookingService.AddTimeSlot(t3, new DateTime(2026, 1, 5, 19, 0, 0), new DateTime(2026, 1, 5, 20, 0, 0));

            _bookingService.AddTimeSlot(t4, new DateTime(2026, 1, 25, 8, 0, 0), new DateTime(2026, 1, 25, 9, 0, 0));
            _bookingService.AddTimeSlot(t4, new DateTime(2026, 2, 2, 9, 0, 0), new DateTime(2026, 2, 2, 10, 0, 0));
            Console.WriteLine("Dane załadowane");
        }
    }
}