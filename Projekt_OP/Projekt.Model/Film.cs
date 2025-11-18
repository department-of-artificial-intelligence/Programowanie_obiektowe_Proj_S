using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Film
    {
        public int _iD;
        public string _title;
        public int _timeMin;
        public string _genre;

        public int ID { get { return _iD; } set { _iD = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public int TimeMin { get { return _timeMin; } set { _timeMin = value; } }
        public string Genre { get { return _genre; } set { _genre = value; } }

        public Film()
        {
            _iD = 0;
            _title = string.Empty;
            _timeMin = 0;
            _genre = string.Empty;
        }

        public Film(int ID, string Title, int Time, string Genre)
        {
            _iD = ID;
            _title = Title;
            _timeMin = Time;
            _genre = Genre;
        }

        public override string ToString()
        {
            return $"Film: {_title} - {_iD} - {_timeMin} min - {_genre}\n";
        }
    }
}
