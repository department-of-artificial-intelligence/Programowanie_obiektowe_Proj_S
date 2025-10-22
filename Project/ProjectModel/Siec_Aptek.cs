using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Siec_Aptek
    {
        public List<Apteka> _apteki { get; set; }

        public Siec_Aptek(List<Apteka> apteki)
        {
            _apteki = new List<Apteka>();
            if(apteki != null)
            {
                foreach(Apteka apteka in apteki)
                {
                    _apteki.Add(apteka);
                }
            }
        }
        public void wyswietlSiecAptek()
        {
            foreach(Apteka a in _apteki)
            {
                Console.WriteLine($"{a},\n");
            }
        }
    }
}
