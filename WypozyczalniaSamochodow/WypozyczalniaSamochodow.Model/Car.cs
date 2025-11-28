namespace WypozyczalniaSamochodow.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public float EngineVolume { get; set; }
        public double AvgConsumption { get; set; }
        public int Power { get; set; }
        public string Gearbox { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;

        public double PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        public int BranchId { get; set; }
        //public Branch Branch { get; set; }

        public Car() { }
        public Car(int id, string brand, string model, int productionYear, float engineVolume, double avgConsumption, int power, string gearbox, string fuelType, double pricePerDay, bool isAvailable, int branchId)
        {
            Id = id;
            Brand = brand;
            Model = model;
            ProductionYear = productionYear;
            EngineVolume = engineVolume;
            AvgConsumption = avgConsumption;
            Power = power;
            Gearbox = gearbox;
            FuelType = fuelType;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;
            BranchId = branchId;
        }

        public override string ToString()
        {
            return $"[{Id}] {Brand} {Model} ({ProductionYear}) | {Power}KM | {PricePerDay} zł/dzień | {(IsAvailable ? "Dostępny" : "Niedostępny")}";
        }
    }
}