using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Vehicle
    {
        public enum VehicleStatus { Avaliable, InTransit, UnderMaintenance }
        public enum VehicleType { CompanyCar, DeliveryVan, Truck, SemiTrailer /*naczepa*/ }
        public int Id { get; set; }
        public int VinNumber { get; set; }
        public int ProductionYear { get; set; }
        public int EngineSize { get; set; }
        public int MileAge { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegNum { get; set; }

        public VehicleStatus Status { get; set; }
        public VehicleType Type { get; set; }

        public override string ToString()
        {
            return $"Vehicle ID: {Id}, VIN: {VinNumber}, Brand: {Brand}, Model: {Model}, " +
                   $"Production year: {ProductionYear}, Engine: {EngineSize}cc, Mileage: {MileAge} km, " +
                   $"Registration: {RegNum}, Type: {Type}, Status: {Status}";
        }
    }
}
