using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pracownik : public Pracodawca : public Praca
    {
        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }

        public required int Id { get; set; }

        public required int Age { get; set; }


        public void Project(int start, int end, string status, int grade)
        {
            int start = Console.ReadLine();
            int end = Console.ReadLine();
            string status = Console.ReadLine();
            int grade = Console.ReadLine();
        }

        public void PassedProjects(Project(int start,int end,string status ,int grade),int amountofpassed)
        {
            amountofpassed = 0;

            if(grade > 60)
            {
                Console.WriteLine("Projekty zaliczone: ", Project(start, end, status, grade));
                amountofpassed++;
            }

            Console.WriteLine("Liczba zaliczonych projektów: ", amountofpassed)
        }

        public void NotPassedProjects(Project(int start, int end, string status, int grade), int amountofnotpassed)
        {
            amountofnotpassed = 0;

            if (grade < 60)
            {
                Console.WriteLine("Projekty zaliczone: ", Project(start, end, status, grade));
                amountofnotpassed++;
            }

            Console.WriteLine("Liczba niezaliczonych projektów: ", amountofnotpassed)
        }


    }

    
}
