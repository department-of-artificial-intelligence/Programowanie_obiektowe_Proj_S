using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt.Model
{
    public class CinemaMenager
    {
        private List<Cinema> _cinemas;
        
        public List<Cinema> Cinemas { get { return _cinemas; } set { _cinemas = value; } }


        public CinemaMenager(List<Cinema> cinemas)
        {
            _cinemas = cinemas;
        }

        public void DisplayCinemas()
        {
            Console.WriteLine("\n---Lista Kin--- ");
            if (_cinemas.Count == 0)
            { 
                Console.WriteLine("Brak Kin w Systemie!");
            }

            foreach (var c in _cinemas)
            {
                Console.WriteLine(c.ToString());
            }
        }

    }
}
