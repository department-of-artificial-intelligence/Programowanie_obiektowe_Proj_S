using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public enum BicycleStatus
    {
        Available,
        Rented,
        Maintenance
    }
    internal class Bicycle
    {
        public int Id {  get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public Station? CurrentStation { get; internal set; }
        public BicycleStatus Status { get; private set; } = BicycleStatus.Available;
        

        public void Rent()
        {
            if (Status != BicycleStatus.Available)
            {
                throw new InvalidOperationException($"Bicycle ID {Id} is not available. Current status: {Status}");
            }

            Status = BicycleStatus.Rented;
            CurrentStation = null;
        }

        public void Return(Station station)
        {
            Status = BicycleStatus.Available;
            CurrentStation = station; 
        }

        public override string ToString()
        {
            
            return $"ID: {Id}, {Model} ({Type}) - {Price} EUR/hour";
        }
    }
}
