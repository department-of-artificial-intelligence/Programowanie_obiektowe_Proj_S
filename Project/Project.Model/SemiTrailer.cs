using Project.Abstractions;


namespace Project.Model
{
    public class SemiTrailer : Vehicle
    {
        public float MaxGrossWeightTons { get; init; }

        public SemiTrailer(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registationNumber, float maxGrossWeightTons)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registationNumber)
        {
            MaxGrossWeightTons = maxGrossWeightTons;
        }

        public override VehicleType VType => VehicleType.SemiTrailer;


        public override float CalculateWearRate() => 0.35f;

        public override string ToString()
        {
            return base.ToString() + $", Max Gross Weight: {MaxGrossWeightTons} t";
        }
    }
}