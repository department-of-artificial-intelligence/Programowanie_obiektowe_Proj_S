using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.Model.Service
{
    public interface IVehicleService
    {
        Task AddVehicle(Vehicle vehicle); 
        Task<bool> DeleteVehicle(int vehicleId); 
        Task<Vehicle?> GetVehicleById(int vehicleId); 
        Task<List<Vehicle>> GetAllVehicles(); 
        Task<List<Vehicle>> SearchVehicles(string searchTerm);
        Task<bool> ServiceVehicle(int vehicleId);
        Task<int> GetVehicleCount();
        Task<int> GetRentedVehicleCount();
        Task<int> GetAvailableVehicleCount();
    }
}
