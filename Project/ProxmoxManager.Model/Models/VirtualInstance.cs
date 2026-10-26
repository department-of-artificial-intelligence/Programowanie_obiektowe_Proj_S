using ProxmoxManager.Model.Interfaces;

namespace ProxmoxManager.Model.Models;

public abstract class VirtualInstance : IResourceAllocatable
{
    public int Id { get; private set; }
    public int VmId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int AllocatedCores { get; private set; }
    public int AllocatedRamMb { get; private set; }
    public int DiskSizeGb { get; private set; }
    public bool IsRunning { get; private set; } = true;
    public int? ProxmoxNodeId { get; private set; }

    public abstract double CalculateHostOverheadMb();
    
    public int GetAllocatedRamMb() => AllocatedRamMb;
    public int GetAllocatedCores() => AllocatedCores;
}