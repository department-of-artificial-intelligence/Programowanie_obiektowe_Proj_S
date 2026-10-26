using System.Linq;
using ProxmoxManager.Model.Interfaces;

namespace ProxmoxManager.Model.Models;
public class ProxmoxNode : IResourceAllocatable,IMonitorable
{
    public int Id { get; private set; }
    public string NodeName { get; private set; } = string.Empty;
    public string IpAddress { get; private set; } = string.Empty;
    public bool IsConnected { get; private set; }
    public HardwareSpecs Hardware { get; private set; } = new ();
    public List<StoragePools> StoragePools { get; private set; } = [];
    public List<VirtualInstance> Instances { get; private set; } = [];

    public int GetAllocatedRamMb()
    {
        return Instances.Where(i => i.IsRunning).Sum(i => i.AllocatedRamMb);
    }
    
    public int GetAllocatedCores()
    {
        return Instances.Where(i => i.IsRunning).Sum(i => i.AllocatedCores);
    }

    public bool GetHealthStatus()
    {
        return IsConnected && GetAllocatedRamMb() <= Hardware.TotalRamMb;
    }
}