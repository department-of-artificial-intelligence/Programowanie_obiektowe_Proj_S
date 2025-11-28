using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class ElectricBicycle : Bicycle
    {
        public int BatteryCharge { get; set; }

        public ElectricBicycle() : base()
        {
            Type = "Electric";
            BatteryCharge = 100;
        }

        public ElectricBicycle(int batteryCharge) : base() {
            BatteryCharge = batteryCharge;
        }

        public void Charge()
        {
            BatteryCharge = 100;
        }

        public int CheckBatteryCharge()
        {
            return BatteryCharge;
        }
        public override string ToString()
        {
            return base.ToString() + $"[Battery: {BatteryCharge}%";
        }
    }
}
