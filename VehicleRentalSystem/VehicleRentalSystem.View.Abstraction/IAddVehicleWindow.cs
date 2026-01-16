using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Common;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.View.Abstractions
{
    public interface IAddVehicleWindow : IWindow
    {
        public Vehicle Vehicle { get; set; }
    }
}
