using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class CityBike : Bike
    {
        public CityBike(int id, string model, decimal pricePerHour) : base(id, model, pricePerHour)
        {
        }
    }
}
