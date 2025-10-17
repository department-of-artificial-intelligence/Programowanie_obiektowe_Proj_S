using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model
{
    public class Car
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int ProductionYear { get; set; }
        public float EngineCapacity { get; set; }
        public string EngineType { get; set; }
        public int EnginePower { get; set; }
        public string GearboxType { get; set; }
        public string CarType { get; set; }

        public override string ToString()
        {
            return $"Marka: {CarBrand} | Model: {CarModel} | Rok produkcji: {ProductionYear}";
        }
    }
}
