namespace Project.Model.Entities
{
    internal class VirtualMachine
    {
        public bool EnableQemuAgent { get; private set; }=false;
        public bool HasGPUOverhead { get; private set; }=true;

        //public double CalculateHostOverheadMb();
    }
}
