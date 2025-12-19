using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public enum BicycleStatus
    {
        Available,
        Rented,
        Maintenance
    }

    public class Bicycle
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public BicycleType Type { get; set; }
        public decimal Price { get; set; }

        public int? BatteryLevel { get; set; } 
        public int? RangeKm { get; set; }

        public int? CurrentStationId { get; set; }

        
        [ForeignKey("CurrentStationId")]
        public virtual Station? CurrentStation { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer? Renter { get; set; }

        public BicycleStatus Status { get; private set; } = BicycleStatus.Available;

        public void Park(int stationId)
        {
            
            this.Status = BicycleStatus.Available;
            this.CurrentStationId = stationId;
            this.CustomerId = null; 
        }

        public void Rent()
        {
            if (Status != BicycleStatus.Available)
            {
                throw new InvalidOperationException($"Bike {Id} is not available.");
            }

            Status = BicycleStatus.Rented;
            CurrentStation = null;
            CurrentStationId = null; 
        }

        
        public void Return(Station station)
        {
            Status = BicycleStatus.Available;
            CurrentStation = station;
            CurrentStationId = station.Id; 
        }

        public override string ToString()
        {
            return $"ID: {Id}, {Model} ({Type}) - {Price} PLN/hour [{Status}]";
        }
    }
}