using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pracownik : Osoba : IPracujacy : IKontakt
    {
        public Adres AdresZamieszkania { get; set; }
        public Stanowisko StanowiskoPracy { get; set; }
       
        public List<Projekt> ListaProjektow { get; set; } = new List<Projekt>();
       
        public void PassedProjects()
        {
            int amountofpassed = 0;
       
            foreach (Projekt p in ListaProjektow)
            {
                if (p.Status == "Zakończony" && p.Ocena > 60)
                {
                    amountofpassed = amountofpassed + 1;
                }
            }
            return amountofpassed;
        }
       
        public void NotPassedProjects()
        {
            int amountofnotpassed = 0;
       
            foreach (Projekt p in ListaProjektow)
            {
                if (p.Status == "Zakończony" && p.Ocena < 60)
                {
                    amountofnotpassed = amountofnotpassed + 1;
                }
            }
            return amountofnotpassed;
        }
       
        public Adres Adres { get; set; }
        public string Email { get; set;}
        public string Telefon {  get; set; }
       
        public string LoadContactInfo()
        {
            return $"Email: {Email} | Telefon: {Telefon} | Adres: {Adres.Miasto}, {Adres.Ulica}"
        }
       
   }    
}
