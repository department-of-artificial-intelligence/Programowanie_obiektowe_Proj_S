using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pracodawca : Osoba
    {

        public List<Dzial> ListaDzialow {  get; set; } = new List<Dzial>();

        public Adres Adres { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public string LoadContactInfo()
        {
            return $"Email firmy: {Email} | Telefon: {Telefon} | Adres siedziby: {Adres.Miasto}"
        }

        public void DoEmployeesProjectsCheck()
        {
            Console.WriteLine($"Szef {B_FirstName} sprawdza projekty")
            
            foreach(Pracownik prac in ListaPracownikow)
            {
                Console.WriteLine($"Pracownik {prac.FirstName} {prac.LastName}")

                if(prac.ListaPracownikow.Count == 0)
                {
                    Console.WriteLine("Brak przypisanych projektow")
                }
                else
                {
                    foreach(Projekt proj in prac.ListaProjektow)
                    {
                        Console.WriteLine($"Projekt: '{proj.Name}', Ocena: {proj.Ocena}")
                    }
                }
            }
        }

        public void DoMeeting()
        {
            string MeetingDate = "XX.XX.XXXX";

            if (MeetingDate == "25.XX.20XX")
            {
                Console.WriteLine("Dzisiaj odbędzie sie ważne spotkanie o godz. 16:00")
            }
        }
    }

    


}
