using Project.Abstractions;

namespace Project.Model
{
    public class CompanyCar : Vehicle
    {
        public int NumberOfSeats { get; init; }

        public CompanyCar(int id, string vinNumber, int productionYear, float engineSize, int mileage, string brand, string model, string registrationNumber, int numberOfSeats)
            : base(id, vinNumber, productionYear, engineSize, mileage, brand, model, registrationNumber)
        {
            NumberOfSeats = numberOfSeats;
        }

        public override VehicleType VType => VehicleType.CompanyCar;

        public override float CalculateWearRate()
        {
            int vehicleAge = DateTime.Now.Year - ProductionYear;
            float wear = 0.02f + (Mileage * 0.00001f) + (vehicleAge * 0.005f);

            if (NumberOfSeats > 5)
                wear *= 1.05f;

            return wear;
        }

        public override string ToString()
        {
            return base.ToString() + $", Seats: {NumberOfSeats}";
        }
    }
}
