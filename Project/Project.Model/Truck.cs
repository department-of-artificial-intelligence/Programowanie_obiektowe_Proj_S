using Project.Abstractions;


namespace Project.Model
{
    public class Truck : Vehicle
    {
        public int MaxPayloadKg { get; init; }

        public Truck(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registationNumber, int maxPayload)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registationNumber)
        {
            MaxPayloadKg = maxPayload;
        }

        public override VehicleType VType => VehicleType.Truck;

        public override float CalculateWearRate() => 0.25f;

        public override string ToString()
        {
            return base.ToString() + $", Max Payload: {MaxPayloadKg} kg";
        }
    }
}