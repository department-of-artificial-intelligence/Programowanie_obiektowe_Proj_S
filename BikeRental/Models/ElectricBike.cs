using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class ElectricBike : Bike
    {
        public ElectricBike(int id, string model, decimal pricePerHour) : base(id, model, pricePerHour)
        {
        }
        public override decimal GetHourlyPrice()
        {
            return PricePerHour + 4m;
        }
    }
}
