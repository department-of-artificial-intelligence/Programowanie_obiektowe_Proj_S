using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Vehicle : IVehicle
    {
        public int Id { get; set; }
        public int VinNumber { get; set; }
        public int ProductionYear { get; set; }
        public int EngineSize { get; set; }
        public int Mileage { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string RegistrationNumber { get; set; }

        public VehicleStatus VStatus { get; set; } = VehicleStatus.Available;
        public VehicleType VType { get; set; }

        public Driver? AssignedDriver { get; private set; }

        public Vehicle(int id, int vinNumber, int productionYear, int engineSize, int mileage, string brand, string model, string registationNumber)
        {
            this.Id = id;
            this.VinNumber = vinNumber;
            this.ProductionYear = productionYear;
            this.EngineSize = engineSize;
            this.Mileage = mileage;
            this.Brand = brand;
            this.Model = model;
            this.RegistrationNumber = registationNumber;
        }

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
                   $"Production year: {ProductionYear}, Engine: {EngineSize}cc, Mileage: {Mileage} km, " +
                   $"Registration: {RegistrationNumber}, Type: {VType}, Status: {VStatus}";
        }
    }
}
