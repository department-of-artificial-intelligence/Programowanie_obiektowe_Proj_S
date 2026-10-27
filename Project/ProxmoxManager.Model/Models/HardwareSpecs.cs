namespace ProxmoxManager.Model.Models;

public class HardwareSpecs
{
    public int Id { get; private set; }
    public string CpuModel { get; private set; } = string.Empty;
    public int TotalCores { get; private set; }
    public int TotalThreads { get; private set; }
    public int TotalRamMb { get; private set; }
    public string KernelVersion { get; private set; } = string.Empty;
    public long UptimeSeconds { get; private set; }

    public HardwareSpecs()
    {
        CpuModel = string.Empty;
        KernelVersion = string.Empty;
    }
    
    public void UpdateHardwareSpecs(string cpuModel, int cores,int threads, int ramMb)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cpuModel,nameof(cpuModel));
        if(cores <= 0 ) throw new ArgumentOutOfRangeException(nameof(cores));
        if(threads < cores) throw new ArgumentOutOfRangeException(nameof(threads));
        if(ramMb <= 0) throw new ArgumentOutOfRangeException(nameof(ramMb));
        
        this.CpuModel = cpuModel;
        this.TotalCores = cores;
        this.TotalThreads = threads;
        this.TotalRamMb = ramMb;
    }
    public void UpdateSystemInfo(string kernelVersion,long uptimeSeconds)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(kernelVersion,nameof(kernelVersion));
        this.KernelVersion = kernelVersion;
        this.UptimeSeconds = uptimeSeconds;
    }
}