using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2_.Project_lab2
{
    internal class Invertar
    {
        private List<Towar> _towars = new();

        public void AddTowar(Towar towar)
        {
            _towars.Add(towar);
        }

        public void ShowAll()
        {
            Console.WriteLine("_________________________________Asortyment_________________________________");
            foreach (var t in _towars)
            {
                Console.WriteLine(t);
            }
        }
    }
}
