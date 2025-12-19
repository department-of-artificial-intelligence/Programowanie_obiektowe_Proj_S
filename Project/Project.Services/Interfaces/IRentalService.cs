using Project.DAL;
using Project.Model;
using Project.Services.Interfaces;
using System.Collections.Generic;

namespace Project.Services.Interfaces
{
    public interface IRentalService
    {
        string RentBicycle(int bicycleId, int customerId);
        string ReturnBicycle(int bicycleId, int stationId);
        List<RentalRecord> GetHistory();
    }
}

