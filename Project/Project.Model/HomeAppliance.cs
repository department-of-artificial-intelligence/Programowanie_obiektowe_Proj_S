namespace Project.Model
{
    
    public class HomeAppliance : Product
    {
        
        public string EnergyClass { get; set; } = "F"; 
        public double PowerConsumptionWatts { get; set; }
        public double Capacity { get; set; } 

        public HomeAppliance() { }

        public override string GetDescription()
        {
            
            return $"{base.GetDescription()} | Klasa: {EnergyClass} | Moc: {PowerConsumptionWatts}W";
        }
    }
}