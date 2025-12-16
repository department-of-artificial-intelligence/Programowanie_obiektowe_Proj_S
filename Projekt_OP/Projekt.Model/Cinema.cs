using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        //Klucz główny do bazy
        public int CinemaID { get;  set; }
        public string CinemaName { get; set; }
        public List<Hall> Hall { get; set; } = new List<Hall>();

        //Klucze obce
        public CinemaAddress Address { get; set; }
        public int CinemaAddressID { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        //==================================


        public Cinema() 
        {

        }

        public Cinema(string cinemaName, CinemaAddress address)
        {
            CinemaName = cinemaName;
            Address = address;
        }

        public override string ToString()
        {
            return $"Kino: {CinemaName} | IDKina:{CinemaID}  \nAdres: {Address}\n";

        }
    }
}
