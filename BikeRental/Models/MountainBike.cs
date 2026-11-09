using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class MountainBike : Bike
    {
        public MountainBike(int id, string model, decimal pricePerHour) : base(id, model, pricePerHour)
        {
        }
        public override decimal GetHourlyPrice()
        {
            return PricePerHour + 2m;
        }
    }
}
