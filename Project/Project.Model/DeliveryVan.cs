using Project.Abstractions;

namespace Project.Model
{
    public class DeliveryVan : Vehicle
    {
        public float MaxVolumeCubicMeters { get; init; }

        public DeliveryVan(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registrationNumber, float maxVolume)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registrationNumber)
        {
            MaxVolumeCubicMeters = maxVolume;
        }

        public override VehicleType VType => VehicleType.DeliveryVan;

        public override float CalculateWearRate()
        {
            int vehicleAge = DateTime.Now.Year - ProductionYear;
            float wear = 0.05f + (Mileage * 0.000015f) + (vehicleAge * 0.007f);
            return wear;
        }

        public override string ToString()
        {
            return base.ToString() + $", Max Volume: {MaxVolumeCubicMeters} m³";
        }
    }
}