using System.Collections.Generic;

namespace Project.Model
{
    public class Room
    {
        public string Field { get; set; }
        public List<Actuating_Device> ActuatingDevices { get; set; } = new List<Actuating_Device>();
        public List<Sensors> SensorsList { get; set; } = new List<Sensors>();
        public void EvaluateConditions() { /* Logika automatyki */ }
    }
}