using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Dzial
    {
        public string NazwaDzialu {  get; set; }
        public List<Pracownik> ListaPracownikow { get; set; } = new List<Pracownik>();
    }
}