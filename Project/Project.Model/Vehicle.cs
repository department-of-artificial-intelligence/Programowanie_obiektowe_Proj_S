using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ProdYear { get; set; }
        public int MileAge { get; set; } //przebieg
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegNum { get; set; }
        public string Body { get; set; } //nadwozie
        public string Status { get; set; } //wypozyczony, w naprawie, dostepny

        public override string ToString()
        {
            return $"{Brand} {Model}\nID: {Id}\nRegister Number: {RegNum}\nBody: {Body}\nProduction Year: {ProdYear}\nMileage: {MileAge}\nStatus: {Status}";
        }

    }
}
