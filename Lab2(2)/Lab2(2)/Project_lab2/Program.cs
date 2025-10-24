using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2_.Project_lab2
{
    internal class Program
    {
        static void Main()
        {
            var towars = new Invertar();

            towars.AddTowar(new Towar(20, "Paracetamol", 18, new DateTime(2026,12,20)));
            towars.AddTowar(new Towar(20, "Paracetamol", 18, new DateTime(2026, 9, 10)));
            towars.AddTowar(new Towar(20, "Paracetamol", 18, new DateTime(2027, 1, 30)));

            towars.ShowAll();   
        }

    }
}
