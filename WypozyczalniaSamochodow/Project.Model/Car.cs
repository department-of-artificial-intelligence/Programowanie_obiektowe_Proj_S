using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaSamochodow.Model
{
    public class Car
    {
        public int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string ProductionYear { get; set; }
        public int Power { get; set; }     
        public required string Gearbox { get; set; }
        public required string FuelType { get; set; }


    }
}
