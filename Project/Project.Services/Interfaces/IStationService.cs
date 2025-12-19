using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Services.Interfaces
{
    public interface IStationService
    {
        void AddStation(string name, string city, string address, int capacity);

        List<Station> GetAllStations();
        string ParkBicycle(int stationId, int bicycleId);
    }
}
