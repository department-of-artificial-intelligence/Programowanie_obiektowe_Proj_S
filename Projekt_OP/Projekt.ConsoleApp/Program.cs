using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projekt.Model;
using Projekt.DATABASE;


namespace Projekt
{
    class Program
    {



        static void DisplayMainMenu(ConsoleView view)
        {
            Console.Clear();
            view.DisplayWelcomeMessage();
            Console.WriteLine("==== GŁÓWNE MENU  ====");
            Console.WriteLine("1. Szczegoły Kin");
            Console.WriteLine("2. Sczegoły Pracownikow");
            Console.WriteLine("3. Sczegoły Filmow");
            Console.WriteLine("0. Wyjdź z programu");
            Console.WriteLine("------------------------------------------");
        }

        static void DisplayMainMenu1(ConsoleView view)
        {
            Console.Clear();
            Console.WriteLine("==== Zarzadzaj Kinami  ====");
            Console.WriteLine("1.pokaz wszystkie kina i informacje");
            Console.WriteLine("2.pokaz kino po ID");
            Console.WriteLine("3.Dodaj kino");
            Console.WriteLine("4.Usun kino po ID");
            Console.WriteLine("0.Wroc do glownego menu");
            Console.WriteLine("------------------------------------------");
        }
        static void DisplayMainMenu2(ConsoleView view)
        {
            Console.Clear();
            Console.WriteLine("==== Zarzadzaj Pracownikami  ====");
            Console.WriteLine("1.pokaz wszystkich pracownikow i informacje");
            Console.WriteLine("2.pokaz pracownika po ID");
            Console.WriteLine("3.Dodaj pracownika");
            Console.WriteLine("4.Usun pracownika po ID");
            Console.WriteLine("0.Wroc do glownego menu");
            Console.WriteLine("------------------------------------------");
        }
        static void DisplayMainMenu3(ConsoleView view)
        {
            Console.Clear();
            Console.WriteLine("==== Zarzadzaj Filmami  ====");
            Console.WriteLine("1.pokaz wszystkie filmy i informacje");
            Console.WriteLine("2.pokaz film po ID");
            Console.WriteLine("3.Dodaj film");
            Console.WriteLine("4.Usun film po ID");
            Console.WriteLine("5.Posegreguje filmy");
            Console.WriteLine("6.Pokac statystyki");
            Console.WriteLine("0.Wroc do glownego menu");
            Console.WriteLine("------------------------------------------");
        }
        static void Main(string[] args)
        {

            IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var cns = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
            }).Build();

            
            var _context = _host.Services.GetService<ApplicationDbContext>();
            if (_context == null)
            {
                Console.WriteLine("DbContext jest null!");
            }
            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
                var kino = new Cinema("Bananowe", new CinemaAddress("Czestopchowa", "pudrowa", 54));
                var sala = new Hall(20, kino);
                var film = new Film("Film1", 120, "akcja", sala);
                var pracownik = new Employee("Osoba", "Ukryta", kino);

                sala.Films.Add(film);
                kino.Hall.Add(sala);
                kino.Employees.Add(pracownik);

                _context.Cinemas.Add(kino);
                _context.SaveChanges();

            }
            
        }
    }
}
           // Cinema Kino11 = new Cinema(8, "BANANOWE", new CinemaAddress("Czestochowa", "cukier", 10), new List<Hall>() { new Hall(1, 15, new List<Film>() { new Film(1, "xd", 60, "akcja") }) }, new List<Employee>() { new Employee(100, "Nowak", "ADam") });


//            List<Employee> pracownicyAll = new List<Employee>()
//            {
//                new Employee(1,"Olek","Wyrazik"),
//                new Employee(2,"Mateusz","Szczepanik"),
//                new Employee(3,"Kacper","Marek"),
//                new Employee(4,"Norbert","Cwiklinski"),
//                new Employee(5,"Robert", "Lewandowski"),
//                new Employee(6,"Wojciech", "Szczęsny"),
//                new Employee(7,"Łukasz","Piszczek"),
//                new Employee(8,"Kamil","GOATsicki"),
//                new Employee(9,"Kuba","Błaszczykowski"),


//            };

//                List<Film> filmyAll = new List<Film>()
//            {
//                new Film(1,"Auta",120,"Bajka"),
//                new Film(2,"Szybcy I Wsciekli",180,"Akcja"),
//                new Film(3,"Chuucky",100,"Horror"),
//                new Film(4, "Jak Wytresowac Smoka", 120, "Bajka"),
//                new Film(5, "Szklana pułapka", 180, "Akcja"),
//                new Film(6, "Obecnosc", 100, "Horror"),
//                new Film(7, "MyHeroAcademia", 120, "Anime"),
//                new Film(8, "Spider-Man", 180, "Akcja"),
//                new Film(9, "Zakonnica", 100, "Horror")

//            };

//                List<Hall> sale1 = new List<Hall>()
//            {
//                new Hall(1,20,filmyAll.GetRange(0,3)),
//                new Hall(2,20,filmyAll.GetRange(3,3)),
//                new Hall(3,20,filmyAll.GetRange(6,3)),
//            };
//                List<Hall> sale2 = new List<Hall>()
//            {
//                new Hall(4,20,filmyAll.GetRange(0,3)),
//                new Hall(5,20,filmyAll.GetRange(3,3)),
//                new Hall(6,20,filmyAll.GetRange(6,3)),
//            };
//                List<Hall> sale3 = new List<Hall>()
//            {
//                new Hall(7,20,filmyAll.GetRange(0,3)),
//                new Hall(8,20,filmyAll.GetRange(3,3)),
//                new Hall(9,20,filmyAll.GetRange(6,3)),
//            };

//                List<Cinema> Kina = new List<Cinema>()
//            {

//                new Cinema(1,"Cukierkowe",new CinemaAddress("Czestochowa","Kruszwicka",15),sale1,pracownicyAll.GetRange(0,3)),
//                new Cinema(2,"Czekoladowe",new CinemaAddress("Czestochowa","AL.NMP", 9),sale2,pracownicyAll.GetRange(3,3)),
//                new Cinema(3,"Smietankowe",new CinemaAddress("Czestochowa","Galeria Jurajska ",23),sale3,pracownicyAll.GetRange(6,3)),

//            };

//            ICinemaRepository cinemaRepository = new CinemaRepository(Kina);
//            IEmployeeRepository employeeRepository = new EmployeeRepository(pracownicyAll);
//            IFilmRepository filmRepository = new FilmRepository(filmyAll);
//            ConsoleView view = new ConsoleView();


//            bool isRuning = true;

//                while (isRuning)
//                {
//                    DisplayMainMenu(view);
//                    int choice = view.GetIDInput("Wybierz Opcje:");


//                    switch (choice)
//                    {
//                        case 1:
//                            DisplayMainMenu1(view);
//                            int choice1 = view.GetIDInput("Wybierz Opcje:");
//                            switch (choice1)
//                            {
//                                case 1:
//                                    Console.Clear();
//                                    view.DisplayCinemas(cinemaRepository.GetAll());
//                                    Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                    Console.ReadLine();
//                                    break;
//                                case 2:
//                                    Console.Clear();
//                                    try
//                                    {
//                                        int id = view.GetIDInput("Wpisz ID: ");
//                                        Cinema cinema = cinemaRepository.GetByID(id);
//                                        view.DisplayCinemaByID(cinema);
//                                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        Console.WriteLine($"Wystapil blad: {ex.Message}");
//                                        Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                        break;
//                                    }
//                                    break;
//                                case 3:
//                                    Console.Clear();
//                                    Console.WriteLine("Dodaj nowe kino:");
//                                    string cinemaName1 = view.GetInput("Nazwa Kina: ");
//                                    string city1 = view.GetInput("Miasto: ");
//                                    string street1 = view.GetInput("Ulica: ");
//                                    int streetNumber1 = view.GetIDInput("Numer Ulicy: ");
//                                    CinemaAddress address1 = new CinemaAddress(city1, street1, streetNumber1);
//                                    var cinema1 = new Cinema(0, cinemaName1, address1, sale3, pracownicyAll.GetRange(0, 3));
//                                    cinemaRepository.Add(cinema1);
//                                    break;

//                            }

//                            break;

//                        case 2:
//                            DisplayMainMenu2(view);
//                            int choice2 = view.GetIDInput("Wybierz Opcje:");
//                            switch (choice2)
//                            {
//                                case 1:
//                                    Console.Clear();
//                                    view.DisplayEmployees(employeeRepository.GetAll());
//                                    Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                    Console.ReadLine();
//                                    break;
//                                case 2:
//                                    Console.Clear();
//                                    try
//                                    {
//                                        int idE = view.GetIDInput("Wpisz ID: ");
//                                        Employee employeePoId = employeeRepository.GetByID(idE);
//                                        view.DisplayEmployeeByID(employeePoId);
//                                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        Console.WriteLine($"Wystapil blad: {ex.Message}");
//                                        Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                        break;
//                                    }
//                                    break;
//                                case 0:
//                                    Console.WriteLine("Wroc do glownego menu");
//                                    break;

//                            }
//                            break;

//                        case 3:
//                            DisplayMainMenu3(view);
//                            int choice3 = view.GetIDInput("Wybierz Opcje:");
//                            switch (choice3)
//                            {
//                                case 1:
//                                    Console.Clear();
//                                    view.DisplayFilms(filmRepository.GetAll());
//                                    Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                    Console.ReadLine();
//                                    break;
//                                case 2:
//                                    Console.Clear();
//                                    try
//                                    {
//                                        int idF = view.GetIDInput("Wpisz ID: ");
//                                        Film FilmPoID = filmRepository.GetByID(idF);
//                                        view.DisplayFilmByID(FilmPoID);
//                                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        Console.WriteLine($"Wystapil blad: {ex.Message}");
//                                        Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
//                                        Console.ReadLine();
//                                        break;
//                                    }
//                                    break;

//                            }

//                            break;
//                        case 0:
//                            isRuning = false;
//                            break;
//                        default:
//                            Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
//                            Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
//                            Console.ReadLine();
//                            break;


//                    }

//                }
            
//        }
//    }
//}
    




