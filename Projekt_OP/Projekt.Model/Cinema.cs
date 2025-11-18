using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        public int _cinemaID;
        public string _cinemaName;
        public List<Hall> _hall;

        public int CinemaID { get { return _cinemaID; } set { _cinemaID = value; } }

        public string CinemaName { get { return _cinemaName; } set { _cinemaName = value; } }

        public List<Hall> Hall { get { return _hall; } set { _hall = value; } }

        public Cinema() 
        {
            this._cinemaID = 0;
            this._cinemaName = string.Empty;
            this._hall = new List<Hall>();

        }
        

        public Cinema(int CinemaID,string CinemaName) 
        {
            _cinemaID = CinemaID;
            _cinemaName = CinemaName;
            _hall = new List<Hall>();
        }


        public override string ToString()
        {
            return $"Kino: {_cinemaName} - {_cinemaID}\n";
        }



    }
}
