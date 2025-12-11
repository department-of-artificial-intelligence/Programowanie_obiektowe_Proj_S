using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        public int CinemaID { get;  set; }
        public string CinemaName { get; set; }
        public List<Hall> Hall { get; set; } = new List<Hall>();
        public CinemaAddress Address { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int CinemaAddressID { get; set; }

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
            return $"Kino: {CinemaName} | IDKina:{CinemaID}|\n";
        }
    }
}
