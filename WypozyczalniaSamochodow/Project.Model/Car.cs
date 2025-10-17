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
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public float EngineVolume { get; set; }
        public double AvgConsumption { get; set; }
        public int Power { get; set; }
        public string Gearbox { get; set; }
        public string FuelType { get; set; }
        public bool IsAvailable { get; set; }

        public Car() : this(0, string.Empty, string.Empty, 0, 0, 0, 0, string.Empty, string.Empty, false) { }

        public Car(int id, string brand, string model, int productionYear, float engineVolume, double avgConsumption, int power, string gearbox, string fuelType, bool isAvailable)
        {
            Id = id;
            Brand = brand;
            Model = model;
            ProductionYear = productionYear;
            EngineVolume = engineVolume;
            AvgConsumption = avgConsumption;
            Power = power;
            Gearbox = gearbox;
            FuelType = fuelType;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return string.Format
            (
                $"Car {Id} - Dostępność: {IsAvailable}:\n"+
                $"Brand: {Brand} \nModel: {Model} \nProduction Year: {ProductionYear} \nEngine Volume: {EngineVolume} \nAvarage Consumption: {AvgConsumption} " +
                $"\nPower: {Power} \nGearbox: {Gearbox} \nFuel Type: {FuelType}"
            );
        }
    }
}
