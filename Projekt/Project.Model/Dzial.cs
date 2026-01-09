using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Dzial : IWyszukiwaniePracownikow
    {
        public int Id { get; set; }
        public string NazwaDzialu {  get; set; }
        public List<Pracownik> ListaPracownikow { get; set; } = new List<Pracownik>();

        public void DoEmployeesProjectsCheck()
        {
            Console.WriteLine($"Sprawdzanie projektow w dziale: {NazwaDzialu}");
            if (ListaPracownikow != null)
            {
                foreach (var prac in ListaPracownikow)
                {
                    Console.WriteLine($"- {prac.FirstName} {prac.LastName}");
                }
            }
        }
        public Pracownik FindBestEmployeeByProjectGrade()
        {
            if (ListaPracownikow == null || !ListaPracownikow.Any())
            {
                return null;
            }
            var bestEmployee = ListaPracownikow
                .Where(prac => prac.ListaProjektow != null && prac.ListaProjektow.Any())
                .OrderByDescending(prac => prac.ListaProjektow.Average(proj => proj.Ocena))
                .FirstOrDefault();

            return bestEmployee;
        }
    }
}