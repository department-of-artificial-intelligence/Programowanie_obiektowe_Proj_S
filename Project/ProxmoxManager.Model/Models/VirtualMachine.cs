namespace ProxmoxManager.Model.Models;

public class VirtualMachine : VirtualInstance
{
    public bool EnableQemuAgent { get; private set; }
    public string MountedIsoFile { get; private set; } = string.Empty;

    public override double CalculateHostOverheadMb()
    {
        //Example overhead calculation for VMs
        return AllocatedRamMb * 0.15;
    }
}