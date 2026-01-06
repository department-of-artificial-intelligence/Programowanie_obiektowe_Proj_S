namespace Project.Model
{
    
    public class ElectronicDevice : Product
    {
        
        public string Processor { get; set; } = "N/A";
        public int RamSizeGB { get; set; }
        public string ScreenSize { get; set; } = "N/A"; 

        public ElectronicDevice() { }

        public override string GetDescription()
        {
            
            return $"{base.GetDescription()} | CPU: {Processor}, RAM: {RamSizeGB}GB, Screen: {ScreenSize}";
        }
    }
}
