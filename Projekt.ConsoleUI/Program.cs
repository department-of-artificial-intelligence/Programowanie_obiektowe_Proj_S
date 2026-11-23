using Projekt.Models;
using Projekt.Models.Enums;
using Projekt.Repositories.InMemory;
using Projekt.Services.Korepetytor;
using Projekt.Services.Lekcja;
using Projekt.Services.Platnosc;
using Projekt.Services.Uczen;

namespace Projekt.ConsoleUI;

public class Program
{
    private static int _passed;
    private static int _failed;

    public static void Main()
    {
        TestModels();
        TestRepositories();
        TestServices();

        Console.WriteLine();
        Console.WriteLine($"Tests passed: {_passed}");
        Console.WriteLine($"Tests failed: {_failed}");

        if (_failed > 0)
        {
            Environment.ExitCode = 1;
        }
    }

    private static void TestModels()
    {
        Run("Ocena stores valid grade", () =>
        {
            var grade = new Ocena(4.5, "Dobry wynik");
            Check(grade.NumericGrade == 4.5);
            Check(grade.Descripiton == "Dobry wynik");
        });

        Run("Ocena normalizes invalid grade", () =>
        {
            var grade = new Ocena(0, "");
            Check(grade.NumericGrade == 2);
            Check(grade.Descripiton == string.Empty);
        });

        Run("Uczen initializes inherited person data", () =>
        {
            var student = CreateStudent();
            Check(!string.IsNullOrWhiteSpace(student.Name));
            Check(student.Name == "Jan");
            Check(student.Surname == "Kowalski");
            Check(student.Email == "jan@example.com");
            Check(student.PoziomNauczania == PoziomNauczania.Podstawowy);
            Check(student.Id > 0);
        });

        Run("Korepetytor initializes availability and rate", () =>
        {
            var tutor = CreateTutor();
            Check(tutor.Name == "Anna");
            Check(tutor.HourlyRate == 80);
            Check(tutor.AvailableDates.Count == 0);
        });

        Run("Przedmiot initializes data", () =>
        {
            var subject = new Przedmiot("Matematyka", PoziomNauczania.Wyzszy);
            Check(subject.Name == "Matematyka");
            Check(subject.Level == PoziomNauczania.Wyzszy);
            Check(subject.Id > 0);
        });

        Run("TerminDostepnosci validates hours", () =>
        {
            var term = new TerminDostepnosci(DayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11));
            Check(term.Day == DayOfWeek.Monday);
            Check(term.From == TimeSpan.FromHours(9));
            Check(term.To == TimeSpan.FromHours(11));

            try
            {
                _ = new TerminDostepnosci(DayOfWeek.Monday, TimeSpan.FromHours(11), TimeSpan.FromHours(9));
                throw new Exception("Invalid availability should throw");
            }
            catch (ArgumentException)
            {
            }
        });

        Run("Lekcja initializes linked data", () =>
        {
            var tutor = CreateTutor();
            var subject = new Przedmiot("Fizyka", PoziomNauczania.PonadPodstawowy);
            var student = CreateStudent();
            var lesson = new Lekcja(
                new DateTime(2026, 12, 1, 10, 0, 0),
                60,
                StatusLekcji.Zaplanowana,
                120,
                tutor,
                subject,
                new List<Uczen> { student });

            Check(lesson.Id > 0);
            Check(lesson.TimeInMinutes == 60);
            Check(lesson.Status == StatusLekcji.Zaplanowana);
            Check(lesson.Cost == 120);
            Check(lesson.Tutor == tutor);
            Check(lesson.Subject == subject);
            Check(lesson.Students.Contains(student));
        });

        Run("Lekcja requires at least one student", () =>
        {
            try
            {
                _ = new Lekcja(
                    DateTime.Now,
                    60,
                    StatusLekcji.Zaplanowana,
                    100,
                    CreateTutor(),
                    new Przedmiot("Matematyka", PoziomNauczania.Podstawowy),
                    new List<Uczen>());
                throw new Exception("Lekcja without students should throw");
            }
            catch (ArgumentException)
            {
            }
        });

        Run("Platnosc initializes and validates amount", () =>
        {
            var lesson = CreateLesson();
            var payment = new Platnosc(TypPlatnosci.BLIK, 50, lesson, "Zaliczka");

            Check(payment.Id > 0);
            Check(payment.Amount == 50);
            Check(payment.Lekcja == lesson);
            Check(payment.LekcjaId == lesson.Id);
            Check(payment.PaymentStatus == StatusPlatnosci.Oczekujaca);
            Check(payment.Opis == "Zaliczka");

            try
            {
                _ = new Platnosc(TypPlatnosci.Gotowka, 0, lesson);
                throw new Exception("Non-positive payment should throw");
            }
            catch (ArgumentException)
            {
            }
        });

        Run("All enum values are defined", () =>
        {
            foreach (PoziomNauczania level in Enum.GetValues<PoziomNauczania>())
                Check(Enum.IsDefined(level));
            foreach (StatusLekcji status in Enum.GetValues<StatusLekcji>())
                Check(Enum.IsDefined(status));
            foreach (StatusPlatnosci status in Enum.GetValues<StatusPlatnosci>())
                Check(Enum.IsDefined(status));
            foreach (TypPlatnosci method in Enum.GetValues<TypPlatnosci>())
                Check(Enum.IsDefined(method));
        });
    }

    private static void TestRepositories()
    {
        Run("UczenRepository add, find and delete", () =>
        {
            var repository = new UczenRepository();
            var student = CreateStudent();

            Check(repository.Add(student));
            Check(repository.GetById(student.Id) == student);
            Check(repository.Get("Jan", "Kowalski") == student);
            Check(repository.GetAll().Count == 1);
            Check(repository.GetByStudyLevel(PoziomNauczania.Podstawowy).Contains(student));
            Check(repository.Delete(student.Id));
            Check(repository.GetById(student.Id) == null);
        });

        Run("KorepetytorRepository add, find and delete", () =>
        {
            var repository = new KorepetytorRepository();
            var tutor = CreateTutor();

            Check(repository.Add(tutor));
            Check(repository.GetById(tutor.Id) == tutor);
            Check(repository.GetAll().Contains(tutor));
            Check(repository.Delete(tutor));
            Check(repository.GetById(tutor.Id) == null);
        });

        Run("LekcjaRepository add, find and filter", () =>
        {
            var repository = new LekcjaRepository();
            var lesson = CreateLesson();

            Check(repository.Add(lesson));
            Check(repository.GetById(lesson.Id) == lesson);
            Check(repository.GetByDate(lesson.StartDate) == lesson);
            Check(repository.GetAll().Contains(lesson));
            Check(repository.GetWithStatus(StatusLekcji.Zaplanowana).Contains(lesson));
            Check(repository.Delete(lesson));
            Check(repository.GetById(lesson.Id) == null);
        });

        Run("PlatnoscRepository add, find and update", () =>
        {
            var repository = new PlatnoscRepository();
            var lesson = CreateLesson();
            var payment = new Platnosc(TypPlatnosci.Karta, 60, lesson);

            repository.AddPayment(payment);
            Check(repository.GetPaymentById(payment.Id) == payment);
            Check(repository.GetAllPayments().Contains(payment));
            Check(repository.GetPaymentsByLessonId(lesson.Id).Contains(payment));
            payment.PaymentStatus = StatusPlatnosci.Zrealizowana;
            Check(repository.UpdatePayment(payment));
        });
    }

    private static void TestServices()
    {
        Run("UczenService grades and average", () =>
        {
            var student = CreateStudent();
            var service = new UczenService();
            var grade = new Ocena(5, "Swietnie");

            service.AddGrade(student, grade);
            Check(student.Oceny.Contains(grade));
            service.RemoveGrade(student, grade);
            Check(!student.Oceny.Contains(grade));
        });

        Run("UczenMapper maps students and grades", () =>
        {
            var student = CreateStudent();
            var dto = UczenMapper.ToDto(student);

            Check(dto.Id == student.Id);
            Check(dto.Name == student.Name);
            Check(dto.Surname == student.Surname);
            Check(dto.Email == student.Email);
            Check(dto.PoziomNauczania == student.PoziomNauczania);
            Check(dto.Oceny.Count == 1);
            Check(dto.Oceny[0].NumericGrade == student.Oceny[0].NumericGrade);
            Check(dto.Oceny[0].Description == student.Oceny[0].Descripiton);

            var createDto = new CreateUczenDto(
                "Maria",
                "Nowak",
                "maria@example.com",
                PoziomNauczania.Wyzszy,
                new List<OcenaDto> { new(5, "Swietnie") });
            var mappedStudent = UczenMapper.ToEntity(createDto);

            Check(mappedStudent.Name == createDto.Name);
            Check(mappedStudent.Surname == createDto.Surname);
            Check(mappedStudent.Email == createDto.Email);
            Check(mappedStudent.PoziomNauczania == createDto.PoziomNauczania);
            Check(mappedStudent.Oceny.Count == 1);
            Check(mappedStudent.Oceny[0].NumericGrade == createDto.Oceny[0].NumericGrade);
            Check(mappedStudent.Oceny[0].Descripiton == createDto.Oceny[0].Description);
        });

        Run("KorepetytorService availability", () =>
        {
            var tutor = CreateTutor();
            var service = new KorepetytorService();
            var term = new TerminDostepnosci(DayOfWeek.Tuesday, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

            Check(service.AddDate(tutor, term));
            Check(tutor.AvailableDates.Contains(term));
            Check(service.RemoveDate(tutor, term));
            Check(!tutor.AvailableDates.Contains(term));
        });

        Run("LekcjaService updates lesson", () =>
        {
            var lesson = CreateLesson();
            var service = new LekcjaService();
            var extraStudent = new Uczen("Maria", "Nowak", "maria@example.com", PoziomNauczania.Wyzszy, new List<Ocena>());

            Check(service.AddStudent(lesson, extraStudent));
            Check(lesson.Students.Contains(extraStudent));
            Check(service.ChangeCost(lesson, 150));
            Check(lesson.Cost == 150);
            Check(service.ChangeStatus(lesson, StatusLekcji.Zakonczona));
            Check(lesson.Status == StatusLekcji.Zakonczona);
            Check(service.RemoveStudent(lesson, extraStudent));
            Check(!lesson.Students.Contains(extraStudent));
        });

        Run("PlatnoscService creates and calculates payment", () =>
        {
            var lesson = CreateLesson();
            var lessonRepository = new LekcjaRepository();
            lessonRepository.Add(lesson);
            var paymentRepository = new PlatnoscRepository();
            var service = new PlatnoscService(paymentRepository, lessonRepository);
            var payment = new Platnosc(TypPlatnosci.Przelew, 40, lesson);

            Check(service.CreatePayment(payment));
            Check(service.GetPaymentById(payment.Id) == payment);
            Check(service.GetPaymentsByLessonId(lesson.Id).Contains(payment));
            Check(service.GetRemainingAmount(lesson.Id) == 120);
            Check(!service.IsLessonPaid(lesson.Id));

            Check(service.EditPaymentMethod(payment, TypPlatnosci.BLIK));
            Check(payment.PaymentMethod == TypPlatnosci.BLIK);
            Check(service.EditPaymentStatus(payment, StatusPlatnosci.Zrealizowana));
            Check(service.GetTotalPaid(lesson.Id) == 40);
            Check(service.GetRemainingAmount(lesson.Id) == 80);
            Check(service.EditText(payment, "Zaplacono"));
            Check(payment.Opis == "Zaplacono");
            

            Check(!service.CreatePayment(new Platnosc(TypPlatnosci.Gotowka, 81, lesson)));
        });

        Run("PlatnoscService rejects overpayment and completed payment changes", () =>
        {
            var lesson = CreateLesson();
            var lessonRepository = new LekcjaRepository();
            lessonRepository.Add(lesson);
            var paymentRepository = new PlatnoscRepository();
            var service = new PlatnoscService(paymentRepository, lessonRepository);
            var payment = new Platnosc(TypPlatnosci.Przelew, 120, lesson);

            payment.PaymentStatus = StatusPlatnosci.Zrealizowana;
            paymentRepository.AddPayment(payment);
            Check(!service.EditPaymentStatus(payment, StatusPlatnosci.Anulowana));
            Check(!service.EditPaymentMethod(payment, TypPlatnosci.Karta));
            Check(!service.CreatePayment(new Platnosc(TypPlatnosci.Gotowka, 1, lesson)));
        });
    }

    private static Uczen CreateStudent()
    {
        return new Uczen(
            "Jan",
            "Kowalski",
            "jan@example.com",
            PoziomNauczania.Podstawowy,
            new List<Ocena> { new(4, "Dobry wynik") });
    }

    private static Korepetytor CreateTutor()
    {
        return new Korepetytor("Anna", "Lewandowska", "anna@example.com", 80);
    }

    private static Lekcja CreateLesson()
    {
        return new Lekcja(
            new DateTime(2026, 12, 1, 10, 0, 0),
            60,
            StatusLekcji.Zaplanowana,
            120,
            CreateTutor(),
            new Przedmiot("Matematyka", PoziomNauczania.Podstawowy),
            new List<Uczen> { CreateStudent() });
    }

    private static void Run(string name, Action test)
    {
        try
        {
            test();
            _passed++;
            Console.WriteLine($"PASS: {name}");
        }
        catch (Exception exception)
        {
            _failed++;
            Console.WriteLine($"FAIL: {name}");
            Console.WriteLine($"      {exception.Message}");
        }
    }

    private static void Check(bool condition)
    {
        if (!condition)
        {
            throw new Exception("Check failed.");
        }
    }
}

