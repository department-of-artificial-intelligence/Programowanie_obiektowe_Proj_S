namespace ProxmoxManager.Model.Models;

public class HardwareSpecs
{
    public int Id { get; private set; }
    public string CpuModel { get; private set; } = string.Empty;
    public int TotalCores { get; private set; }
    public int TotalRamMb { get; private set; }
}