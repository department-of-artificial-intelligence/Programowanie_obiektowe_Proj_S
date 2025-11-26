using Project.Abstractions;

namespace Project.Model
{
    public class DeliveryVan : Vehicle
    {
        public float MaxVolumeCubicMeters { get; init; }

        public DeliveryVan(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registationNumber, float maxVolume)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registationNumber)
        {
            MaxVolumeCubicMeters = maxVolume;
        }

        public override VehicleType VType => VehicleType.DeliveryVan;

        public override float CalculateWearRate() => 0.15f;

        public override string ToString()
        {
            return base.ToString() + $", Max Volume: {MaxVolumeCubicMeters} m³";
        }
    }
}