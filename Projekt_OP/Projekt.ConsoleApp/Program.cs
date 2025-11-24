using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Projekt.Model;


namespace Projekt
{
    class Program
    {

        static void DisplayMainMenu(ConsoleView view)
        {
            Console.Clear();
            Console.WriteLine("==== GŁÓWNE MENU ZARZĄDZANIA KINAMI ====");
            Console.WriteLine("1. Wyświetl listę wszystkich kin");
            Console.WriteLine("2. Wyświetl szczegóły kina (wyszukaj po ID)");
            Console.WriteLine("3. Wyświetl listę wszystkich pracowników");
            Console.WriteLine("4. Wyświetl pracownika po ID");
            Console.WriteLine("5. Wyświetl listę wszystkich filmów");
            Console.WriteLine("6. Wyświetl Film po ID");
            Console.WriteLine("0. Wyjdź z programu");
            Console.WriteLine("------------------------------------------");
        }
        static void Main(string[] args)
        {
            List<Employee> pracownicyAll = new List<Employee>()
            {
                new Employee(1,"Olek","Wyrazik"), 
                new Employee(2,"Mateusz","Szczepanik"),
                new Employee(3,"Kacper","Marek"),
                new Employee(4,"Norbert","Cwiklinski"),
                new Employee(5, "Robert", "Lewandowski"),
                new Employee(6, "Wojciech", "Szczęsny"),
                new Employee(7,"Łukasz","Piszczek"),
                new Employee(8,"Kamil","GOATsicki"),
                new Employee(9,"Kuba","Błaszczykowski"),


            };

            List<Film> filmyAll = new List<Film>()
            {
                new Film(1,"Auta",120,"Bajka"), 
                new Film(2,"Szybcy I Wsciekli",180,"Akcja"),
                new Film(3,"Chuucky",100,"Horror"),
                new Film(4, "Jak Wytresowac Smoka", 120, "Bajka"),
                new Film(5, "Szklana pułapka", 180, "Akcja"),
                new Film(6, "Obecnosc", 100, "Horror"),
                new Film(7, "MyHeroAcademia", 120, "Anime"),
                new Film(8, "Spider-Man", 180, "Akcja"),
                new Film(9, "Zakonnica", 100, "Horror")

            };

            List<Hall> sale1 = new List<Hall>()
            {
                new Hall(1,20,filmyAll.GetRange(0,3)),
                new Hall(1,20,filmyAll.GetRange(3,3)),
                new Hall(1,20,filmyAll.GetRange(6,3)),
            };
            List<Hall> sale2 = new List<Hall>()
            {
                new Hall(1,20,filmyAll.GetRange(0,3)),
                new Hall(1,20,filmyAll.GetRange(3,3)),
                new Hall(1,20,filmyAll.GetRange(6,3)),
            };
            List<Hall> sale3 = new List<Hall>()
            {
                new Hall(1,20,filmyAll.GetRange(0,3)),
                new Hall(1,20,filmyAll.GetRange(3,3)),
                new Hall(1,20,filmyAll.GetRange(6,3)),
            };

            List<Cinema> Kina = new List<Cinema>()
            {
                
                new Cinema(1,"Cukierkowe",new CinemaAddress("Czestochowa","Kruszwicka",15),sale1,pracownicyAll.GetRange(0,3)),
                new Cinema(2,"Czekoladowe",new CinemaAddress("Czestochowa","AL.NMP", 9),sale2,pracownicyAll.GetRange(3,3)),
                new Cinema(3,"Smietankowe",new CinemaAddress("Czestochowa","Galeria Jurajska ",23),sale3,pracownicyAll.GetRange(6,3)),

            };


            ICinemaRepository cinemaRepository = new CinemaRepository(Kina);
            IEmployeeRepository employeeRepository = new EmployeeRepository(pracownicyAll);
            IFilmRepository filmRepository = new FilmRepository(filmyAll);
            ConsoleView view = new ConsoleView();


            view.DisplayWelcomeMessage();
            bool isRuning = true;

            while (isRuning) 
            {
                DisplayMainMenu(view);

                int choice = view.GetIDInput("Wybierz Opcje:");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        view.DisplayCinemas(cinemaRepository.GetAll());
                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                            try
                            {
                                int id = view.GetIDInput("Wpisz ID: ");
                                Cinema cinema = cinemaRepository.GetByID(id);
                                view.DisplayCinemaByID(cinema);
                                Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Wystapil blad: {ex.Message}");
                                Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                                break;
                            }
                        break;
                
                    case 3:
                        Console.Clear();
                        view.DisplayEmployees(employeeRepository.GetAll());
                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                            try
                            {
                                int idE = view.GetIDInput("Wpisz ID: ");
                                Employee employeePoId = employeeRepository.GetByID(idE);
                                view.DisplayEmployeeByID(employeePoId);
                                Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Wystapil blad: {ex.Message}");
                                Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                                break;
                            }
                        break;
                    case 5:
                        Console.Clear();
                        view.DisplayFilms(filmRepository.GetAll());
                        Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.Clear();
                            try
                            {
                                int idF = view.GetIDInput("Wpisz ID: ");
                                Film FilmPoID = filmRepository.GetByID(idF);
                                view.DisplayFilmByID(FilmPoID);
                                Console.WriteLine("\nNacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Wystapil blad: {ex.Message}");
                                Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
                                Console.ReadLine();
                                break;
                            }
                        break;

                    case 0:
                        isRuning = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        Console.WriteLine("Nacisnij Enter aby wrocic do menu....");
                        Console.ReadLine();
                        break;


                }
            }
        }
    }
}
    




