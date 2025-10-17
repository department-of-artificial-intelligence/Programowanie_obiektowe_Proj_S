using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Vehicle
    {
        public enum VehicleStatus { Available, InTransit, UnderMaintenance }
        public enum VehicleType { CompanyCar, DeliveryVan, Truck, SemiTrailer /*naczepa*/ }
        public int Id { get; set; }
        public int VinNumber { get; set; }
        public int ProductionYear { get; set; }
        public int EngineSize { get; set; }
        public int MileAge { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string RegNum { get; set; }

        public VehicleStatus VStatus { get; set; } = VehicleStatus.Available;
        public VehicleType VType { get; set; }

        public Driver? AssignedDriver { get; private set; }

        public bool IsAvailable => VStatus == VehicleStatus.Available;

        public void AssignDriver(Driver driver)
        {
            if(IsAvailable)
            {
                AssignedDriver = driver;
                VStatus = VehicleStatus.InTransit;
            }
        }

        public void MarkAsAvailable()
        {
            VStatus = VehicleStatus.Available;
            AssignedDriver = null;
        }

        public override string ToString()
        {
            return $"Vehicle ID: {Id}, VIN: {VinNumber}, Brand: {Brand}, Model: {Model}, " +
                   $"Production year: {ProductionYear}, Engine: {EngineSize}cc, Mileage: {MileAge} km, " +
                   $"Registration: {RegNum}, Type: {VType}, Status: {VStatus}";
        }
    }
}
