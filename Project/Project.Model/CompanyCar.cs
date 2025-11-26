using Project.Abstractions;

namespace Project.Model
{
    public class CompanyCar : Vehicle
    {
        public bool IsExecutive { get; init; }

        public CompanyCar(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registationNumber, bool isExecutive)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registationNumber)
        {
            IsExecutive = isExecutive;
        }
        public override VehicleType VType => VehicleType.CompanyCar;
        public override float CalculateWearRate() => 0.05f;

        public override string ToString()
        {
            return base.ToString() + $", Executive: {IsExecutive}";
        }
    }
}