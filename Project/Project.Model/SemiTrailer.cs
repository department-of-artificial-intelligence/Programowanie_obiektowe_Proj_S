using Project.Abstractions;


namespace Project.Model
{
    public class SemiTrailer : Vehicle
    {
        public float MaxGrossWeightTons { get; init; }

        public SemiTrailer(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registrationNumber, float maxGrossWeightTons)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registrationNumber)
        {
            MaxGrossWeightTons = maxGrossWeightTons;
        }

        public override VehicleType VType => VehicleType.SemiTrailer;


        public override float CalculateWearRate()
        {
            int vehicleAge = DateTime.Now.Year - ProductionYear;
            float wear = 0.10f + (Mileage * 0.000018f) + (vehicleAge * 0.012f);
            return wear;
        }


        public override string ToString()
        {
            return base.ToString() + $", Max Gross Weight: {MaxGrossWeightTons} t";
        }
    }
}