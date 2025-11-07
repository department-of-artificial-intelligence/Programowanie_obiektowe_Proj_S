using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Model.Vehicle;

namespace Project.Model
{

    public interface IVehicle
    {
        int Id { get; set; }
        string VinNumber { get; set; }
        int ProductionYear { get; set; }
        float EngineSize { get; set; }
        int Mileage { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        string RegistrationNumber { get; set; }

        VehicleStatus VStatus { get; set; }
        VehicleType VType { get; set; }

        Driver? AssignedDriver { get; }
        bool IsAvailable { get; }

        void AssignDriver(Driver driver);
        void MarkAsAvailable();
    }
}