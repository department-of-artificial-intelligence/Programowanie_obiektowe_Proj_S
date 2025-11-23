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
            Console.WriteLine("\n==== GŁÓWNE MENU ZARZĄDZANIA KINAMI ====");
            Console.WriteLine("1. Wyświetl listę wszystkich kin");
            Console.WriteLine("2. Wyświetl szczegóły kina (wyszukaj po ID)");
            Console.WriteLine("3. Wyświetl listę wszystkich pracowników");
            Console.WriteLine("4. Wyświetl listę wszystkich filmów");
            Console.WriteLine("0. Wyjdź z programu");
            Console.WriteLine("------------------------------------------");
        }
        static void Main(string[] args)
        {
            List<Employee> pracownicyALL = new List<Employee>()
            {
                new Employee(1,"Olek","Wyrazik"), new Employee(2,"Mateusz","Szczepanik"),
                new Employee(3,"Kacper","Marek"), new Employee(4,"Norbert","Cwiklinski"),
                new Employee(5, "Robert", "Lewandowski"), new Employee(6, "Wojciech", "Szczęsny"),
            };

            List<Film> filmyALL = new List<Film>()
            {
                new Film (1,"Auta",120,"Bajka"), new Film (2,"Szybcy I Wsciekli",180,"Akcja"),
                new Film (3,"Chuucky",100,"Horror"), new Film (4,"Jak Wytresowac Smoka",120,"Bajka"),
                new Film (5,"Szklana pułapka",180,"Akcja"), new Film (6,"Obecnosc",100,"Horror"),
            };
            List<Hall> saleInit = new List<Hall>()
            {
                // Zakładamy, że klasy Hall i Film zostały ulepszone i nie mają problemów z relacjami.
                new Hall(1, 20, filmyALL.Take(3).ToList()),
                new Hall(2, 20, filmyALL.Skip(3).ToList()),
            };





        }
    }
}
    




