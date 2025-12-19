namespace WypozyczalniaSamochodow.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public double EngineVolume { get; set; }
        public double AvgConsumption { get; set; }
        public int Power { get; set; }
        public string Gearbox { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();

        public Car() { }
        public Car(int id, string brand, string model, int productionYear, double engineVolume, double avgConsumption, int power, string gearbox, string fuelType, decimal pricePerDay, bool isAvailable, int branchId, Branch? branch, List<Rental> rentals)
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
            Branch = branch;
            Rentals = rentals;
        }

        public override string ToString()
        {
            string status = IsAvailable ? "✓ Dostępny" : "✗ Wypożyczony";

            return $"  [{Id}] {Brand} {Model} ({ProductionYear})\n" +
                   $"      Status: {status}\n" +
                   $"      💪 {Power} KM | 🛢️ {EngineVolume}L | ⚙️ {Gearbox} | ⛽ {FuelType}\n" +
                   $"      💰 {PricePerDay:C}/dzień | 📊 Spalanie: {AvgConsumption}L/100km\n";
        }

    }
}