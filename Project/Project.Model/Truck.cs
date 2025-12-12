using Project.Abstractions;

namespace Project.Model
{
    public class Truck : Vehicle
    {
        public int MaxPayLoadKg { get; init; }

        public Truck() { }
        public Truck(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registrationNumber, int maxPayloadKg)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registrationNumber)
        {
            MaxPayLoadKg = maxPayloadKg;
        }

        public override VehicleType VType => VehicleType.Truck;

        public override float CalculateWearRate()
        {
            int vehicleAge = DateTime.Now.Year - ProductionYear;
            float wear = 0.08f + (Mileage * 0.00002f) + (vehicleAge * 0.01f);
            return wear;
        }

        public override string ToString()
        {
            return base.ToString() + $", Max Payload: {MaxPayLoadKg} kg";
        }
    }
}