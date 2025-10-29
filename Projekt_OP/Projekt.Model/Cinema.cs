using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        public required int _cinemaID;
        public required string _cinemaName;

        public int CinemaID { get { return _cinemaID; } set { _cinemaID = value; } }

        public string CinemaName { get { return _cinemaName; } set { _cinemaName = value; } }

        public Cinema() 
        {
            this._cinemaID = 0;
            this._cinemaName = string.Empty;
        }


        public Cinema(int CinemaID,string CinemaName) 
        {
            _cinemaID = CinemaID;
            _cinemaName = CinemaName;
        }


        public override string ToString()
        {
            return $"Kino: {_cinemaName} - {_cinemaID}\n";
        }



    }
}
