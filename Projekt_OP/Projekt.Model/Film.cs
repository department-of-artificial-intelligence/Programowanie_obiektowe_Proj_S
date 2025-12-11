using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Film
    {
        //ID Do bazy (Klucz głowny)
        public int ID { get;  set; }
        public string Title { get; private set; }
        public int TimeMin { get; private set; }
        public string Genre { get; private set; }
        //Nwigacja bazy
        public Hall Hall { get; set; }
        public int HallID { get; set; }

        public Film()
        {
        }

        public Film( string title, int time, string genre,Hall hall)
        {
            Title = title;
            TimeMin = time;
            Genre = genre;
            Hall = hall;
        }

        public override string ToString()
        {
            return $"Film: {Title} | ID:{ID} | CzasTrwania:{TimeMin} min | Gatunek:{Genre}\n";//dopracowac wyswietlanie sie sali w kinie jesli to mozliwe 
        }
    }
}
