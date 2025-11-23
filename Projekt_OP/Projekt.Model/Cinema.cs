using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        private int _cinemaID;
        private string _cinemaName;
        private List<Hall> _hall;

        public int CinemaID { get { return _cinemaID; } set { _cinemaID = value; } }

        public string CinemaName { get { return _cinemaName; } set { _cinemaName = value; } }

        public List<Hall> Hall { get { return _hall; } set { _hall = value; } }

        public Cinema() 
        {
            _cinemaID = 0;
            _cinemaName = string.Empty;
            _hall = new List<Hall>();

        }
        

        public Cinema(int CinemaID,string CinemaName,List<Hall> Hall ) 
        {
            _cinemaID = CinemaID;
            _cinemaName = CinemaName;
            _hall = Hall;
        }


        public override string ToString()
        {
            return $"Kino: {CinemaName} - {CinemaID}\n";
        }
    }
}
