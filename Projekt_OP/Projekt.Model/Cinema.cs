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
        public string CinemaName { get; private set; }
        public List<Hall> Hall { get; private set; }
        public CinemaAddress Address { get; private set; }
        public List<Employee> Employees { get; private set; }

        public Cinema() 
        {
            CinemaID = 0;
            CinemaName = string.Empty;
            Address = new CinemaAddress();
            Hall = new List<Hall>();
            Employees = new List<Employee>();

        }
        

        public Cinema(int cinemaID,string cinemaName,CinemaAddress address,List<Hall> hall,List<Employee> employees) 
        {
            CinemaID = cinemaID;
            CinemaName = cinemaName;
            Address = address;
            Hall = hall;
            Employees  = employees;
        }


        public override string ToString()
        {
            return $"Kino: {CinemaName} | IDKina:{CinemaID}|\n";
        }
    }
}
