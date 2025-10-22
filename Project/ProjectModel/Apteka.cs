using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Apteka
    {
        public int Id_Apteki { get; set; }
        public string Nazwa_Apteki { get; set; }

        public Apteka(int id_Apteki, string nazwa_Apteki)
        {
            Id_Apteki = id_Apteki;
            Nazwa_Apteki = nazwa_Apteki;
        }
        public Apteka() : this(0, string.Empty) { }

        public void wyswietlNazwe()
        {
            Console.WriteLine($"Id: {Id_Apteki}, Nazwa {Nazwa_Apteki}");
        }

    }
}
