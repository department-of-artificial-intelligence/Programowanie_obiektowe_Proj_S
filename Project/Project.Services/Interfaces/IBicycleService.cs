using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Services.Interfaces
{
    public interface IBicycleService
    {
        void AddBicycle(string model, BicycleType type, decimal price, int? batteryLevel = null, int? rangeKm = null);
        List<Bicycle> GetAllBicycles();
        List<Bicycle> GetCheapBicycles();
        void ShowCheapBikes();
    }
}
