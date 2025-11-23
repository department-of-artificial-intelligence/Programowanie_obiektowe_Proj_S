using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Film
    {
        public int ID { get;  set; }
        public string Title { get; private set; }
        public int TimeMin { get; private set; }
        public string Genre { get; private set; }
        public List<Hall> Hall { get; private set; }

        public Film()
        {
            ID = 0;
            Title = string.Empty;
            TimeMin = 0;
            Genre = string.Empty;
            Hall = new List<Hall>();
        }

        public Film(int iD, string title, int time, string genre, List<Hall> hall)
        {
            ID = iD;
            Title = title;
            TimeMin = time;
            Genre = genre;
            Hall = hall;
        }

        public override string ToString()
        {
            return $"Film: {Title} | ID:{ID} | CzasTrwania:{TimeMin} min | Gatunek {Genre} | {Hall}\n";//dopracowac wyswietlanie sie sali w kinie jesli to mozliwe 
        }
    }
}
