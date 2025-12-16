using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Employee
    {
        //Klucz główny do bazy
        public int ID { get; set;}
        public string Name { get; set; }
        public string LastName { get; set; }

        //klucze obce 
        public Cinema Cinema { get; set; }
        public int CinemaID { get; set; }
        //================================
        


        public Employee() { }

        public Employee(string Name, string LastName, Cinema Cinema)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Cinema = Cinema;
        }

        public override string ToString()
        {
             return $"Pracownik: {Name} {LastName} | ID: {ID} | Nazwa Kina: {Cinema.CinemaName} | ID Kina: {Cinema.CinemaID}\n";
        }

    }
}
