using ProxmoxManager.Model.Interfaces;

namespace ProxmoxManager.Model.Models.Domain;

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
    public void ConfigureVirtualInstance(string name,int vmId, int ramMb, int cores,int diskSizeGb, bool isRunning)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        if(vmId < 0) throw new ArgumentOutOfRangeException(nameof(vmId));
        if(cores <= 0) throw new ArgumentOutOfRangeException(nameof(cores));
        if(ramMb <= 0) throw new ArgumentOutOfRangeException(nameof(ramMb));
        if(diskSizeGb < 0) throw new ArgumentOutOfRangeException(nameof(diskSizeGb));
        Name = name;
        VmId = vmId;
        AllocatedRamMb = ramMb;
        AllocatedCores = cores;
        DiskSizeGb = diskSizeGb;
        IsRunning = isRunning;
    }
}