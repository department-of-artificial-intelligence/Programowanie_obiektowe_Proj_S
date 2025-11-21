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

        public override string ToString()
        {
            return $"[{Id}] {Brand} {Model} ({ProductionYear}) | {PricePerDay} zł/dzień | {(IsAvailable ? "Dostępny" : "Niedostępny")}";
        }
    }
}