using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Model
{
    public class TutoringSystemApp
    {
        private static SystemManager manager = new();
        static void Main()
        {
            SetupInitialData();
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine("-----------------------------");

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
                        Console.WriteLine("Zamykanie systemu Do zobaczenia!");
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
                Console.WriteLine("Naciśnij Enter, aby kontynuować");
                Console.ReadLine();
            }
        }
        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("============SYSTEM KOREPETYCJI============");
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
        private static bool IsNotEmpty(string? text) =>
        !string.IsNullOrWhiteSpace(text);
        //menu------------------
        private static void AddTutorMenu()
        {
            Console.Write("Imię: ");
            string? first = Console.ReadLine();
            Console.Write("Nazwisko: ");
            string? last = Console.ReadLine();
            Console.Write("Email: ");
            string? email = Console.ReadLine();

            Console.Write("Stawka za godzinę: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rate))
            {
                Console.WriteLine("Błędna stawka!");
                return;
            }
            if (!IsNotEmpty(first) || !IsNotEmpty(last) || !IsNotEmpty(email))
            {
                Console.WriteLine("Niepoprawne dane!");
                return;
            }
            var tutor = manager.AddTutor(first!, last!, email!, rate);
            Console.WriteLine("--- Dostępne przedmioty ---");
            foreach (var s in manager.Subjects)
            {
                Console.WriteLine(s);
            }

            Console.Write("Podaj ID specjalizacji, aby dodać");
            string? subjectInput = Console.ReadLine();
            if (int.TryParse(subjectInput, out int subjectId))
            {
                var subject = manager.Subjects.FirstOrDefault(s => s.Id == subjectId);
                if (subject != null)
                {
                    tutor.AddSpecialty(subject);
                    Console.WriteLine($"Dodano specjalizację: {subject.Name}");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono przedmiotu o takim ID.");
                }
            }
            Console.WriteLine($"Dodano: {tutor}");
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

            if (!IsNotEmpty(first) || !IsNotEmpty(last) || !IsNotEmpty(level) || !IsNotEmpty(email))
            {
                Console.WriteLine("Niepoprawne dane!");
                return;
            }
            var student = manager.AddStudent(first!, last!, email!, level!);
            Console.WriteLine($"Dodano: {student}");
        }

        private static void AddTimeSlotMenu()
        {
            Console.WriteLine("Dostępni korepetytorzy:");
            foreach (var t in manager.Tutors)
                Console.WriteLine(t);

            Console.Write("ID korepetytora: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Niepoprawne ID!");
                return;
            }

            var tutor = manager.Tutors.FirstOrDefault(t => t.Id == id);
            if (tutor == null)
            {
                Console.WriteLine("Nie znaleziono korepetytora!");
                return;
            }

            Console.Write("Data i godzina rozpoczęcia (rrrr-mm-dd hh:mm): ");
            DateTime start;
            if (!DateTime.TryParse(Console.ReadLine(), out start))
            {
                Console.WriteLine("Błędna data!");
                return;
            }

            Console.Write("Data i godzina zakończenia (rrrr-mm-dd hh:mm): ");
            DateTime end;
            if (!DateTime.TryParse(Console.ReadLine(), out end))
            {
                Console.WriteLine("Błędna data!");
                return;
            }
            var slot = manager.AddTimeSlot(tutor, start, end);
            Console.WriteLine("Dodano termin: " + slot);
        }
        private static void BookLessonMenu()
        {
            Console.WriteLine("=== Korepetytorzy ===");
            foreach (var t in manager.Tutors)
                Console.WriteLine(t);
            Console.Write("ID Korepetytora: ");
            int tutorId = int.Parse(Console.ReadLine()!);
            var tutor = manager.Tutors.FirstOrDefault(t => t.Id == tutorId);
            if (tutor == null)
            {
                Console.WriteLine("Nie znaleziono korepetytora!");
                return;
            }

            Console.WriteLine("=== Uczniowie ===");
            foreach (var s in manager.Students)
                Console.WriteLine(s);
            Console.Write("ID Ucznia: ");
            int studentId = int.Parse(Console.ReadLine()!);
            var student = manager.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Nie znaleziono ucznia!");
                return;
            }

            Console.WriteLine("=== Przedmioty ===");
            foreach (var subj in manager.Subjects)
                Console.WriteLine(subj);
            Console.Write("ID Przedmiotu: ");
            int subjectId = int.Parse(Console.ReadLine()!);
            var subject = manager.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null)
            {
                Console.WriteLine("Nie znaleziono przedmiotu!");
                return;
            }

            Console.WriteLine("=== Dostępne terminy ===");
            foreach (var slot in tutor.Availability.Where(a => !a.IsBooked))
            {
                Console.WriteLine(slot);
            }
            Console.Write("ID Terminu: ");
            int slotId = int.Parse(Console.ReadLine()!);
            var timeSlot = tutor.Availability.FirstOrDefault(a => a.Id == slotId);
            if (timeSlot == null)
            {
                Console.WriteLine("Nie znaleziono terminu!");
                return;
            }

            var reservation = manager.BookLesson(tutor, student, subject, timeSlot);
            if (reservation != null)
            {
                Console.WriteLine("Zarezerwowano lekcję! ID: " + reservation.Id);
            }
        }
        private static void DisplayAllLessons()
        {
            Console.WriteLine("=== Lekcje ===");
            foreach (var l in manager.Lessons)
                Console.WriteLine(l);

            Console.WriteLine("\n=== Rezerwacje ===");
            foreach (var r in manager.Reservations)
                Console.WriteLine($"Rezerwacja {r.Id}: {r.Lesson}");
        }
        private static void GenerateReportsMenu()
        {
            Raport.ShowTutorsByRate(manager);
            Console.WriteLine();
            Raport.ShowSubjectPopularity(manager);
        }
        private static void SetupInitialData()
        {
            //domyślni korepetytorzy i studenci
            var math = manager.AddSubject("Matematyka", "Algebra, geometria, analiza");
            var physics = manager.AddSubject("Fizyka", "Mechanika, termodynamika");
            var english = manager.AddSubject("Język Angielski", "Gramatyka, słownictwo, konwersacje");
            manager.AddSubject("Chemia", "Chemia organiczna i nieorganiczna");
            manager.AddSubject("Biologia", "Anatomia, genetyka, ekologia");
            manager.AddSubject("Historia", "Historia Polski i powszechna");
            manager.AddSubject("Informatyka", "Programowanie w C#, bazy danych");
            var tutor = manager.AddTutor("Jan", "Kowalski", "jan@example.com", 80m);
            var tutor2 = manager.AddTutor("Katarzyna", "Lewandowska", "katarzyna@example.com", 90m);
            var tutor3 = manager.AddTutor("Ewa", "Anglistka", "ewa@mail.com", 85m);
            tutor.AddSpecialty(math);
            tutor2.AddSpecialty(physics);
            tutor3.AddSpecialty(english);
            var student = manager.AddStudent("Anna", "Nowak", "anna@example.com", "Liceum");
            var student2 = manager.AddStudent("Mariusz", "Malinowski", "mariusz@example.com", "Liceum");
            var slot = manager.AddTimeSlot(tutor,
                new DateTime(2025, 1, 15, 10, 0, 0),
                new DateTime(2025, 1, 15, 11, 0, 0));
            manager.BookLesson(tutor, student, math, slot);
            manager.AddTimeSlot(tutor,
                new DateTime(2025, 1, 16, 12, 0, 0),
                new DateTime(2025, 1, 16, 13, 0, 0));
            manager.AddTimeSlot(tutor2,
                new DateTime(2025, 1, 16, 14, 0, 0),
                new DateTime(2025, 1, 16, 15, 0, 0));

        }
    }
}
