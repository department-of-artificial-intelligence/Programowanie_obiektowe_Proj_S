using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Siłownia
    {
        public required string Name { get; set; }
        public required string Address {  get; set; }
        public required int Type_Of_Gym { get; set; }
        public required int Liczba_Trenerow { get; set; }

        
    }
}
