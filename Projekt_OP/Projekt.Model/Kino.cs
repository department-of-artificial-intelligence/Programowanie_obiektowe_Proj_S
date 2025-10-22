using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Kino
    {
        public required int _cinemaID;
        public required string _cinemaName;
        public required int _cinemaRoom;

        public int CinemaID { get { return _cinemaID; } set { _cinemaID = value; } }

        public string CinemaName { get { return _cinemaName; } set { _cinemaName = value; } }

        public int CinemaRoom { get { return _cinemaRoom; } set { _cinemaRoom = value; } }

        public Kino() 
        {
            this._cinemaID = 0;
            this._cinemaName = null;
            this._cinemaRoom = 0;
        }


        public Kino(int CinemaID,string CinemaName, int CinemaRoom) 
        {
            _cinemaID = CinemaID;
            _cinemaName = CinemaName;
            _cinemaRoom = CinemaRoom;
        }

        public string showName() { }

        public int showID() { }

        public string showRooms() { }


    }
}
