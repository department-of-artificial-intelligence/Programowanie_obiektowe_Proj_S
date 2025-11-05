using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly ISourceEmployee _source;

        public EmployeeManager(ISourceEmployee source)
        {
            _source = source;
        }
        public void DisplayEmployees()
        {
            var pracownicy = _source.AllEmployees();
            foreach(Employee emp in pracownicy)
            {
                Console.WriteLine(emp);
            }
        }
        public bool AddEmployee()
        {
            var pracownicy = _source.AllEmployees();
            int new_id = 0;
            while(pracownicy.Any(x => x.Id == new_id)) {
                new_id++;
            }
            Console.Write("Podaj imie nowego pracownika: ");
            string? imie = Console.ReadLine();
            Console.Write("Podaj nazwisko nowego pracownika: ");
            string? nazw = Console.ReadLine();
            Console.Write("Podaj stanowisko nowego pracownika: ");
            string? stanowisko = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazw) || string.IsNullOrWhiteSpace(stanowisko))
            {
                Console.WriteLine("Ktoras dana nie zostala podana");
                return false;
            }
            Employee nowy = new(new_id, imie, nazw, stanowisko);
            if (_source.AddEmployee(nowy))
            {
                Console.WriteLine("Pomyślnie dodano pracownika");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteEmployee()
        {
            Console.Write("Podaj Id pracownika do usunięcia: ");
            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Nie podales liczby!!");
            }
            if (_source.DeleteEmployee(id))
            {
                Console.WriteLine("Pomyślnie usunięto pracownika");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
