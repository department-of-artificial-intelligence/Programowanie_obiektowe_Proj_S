using System.Text.Json.Serialization;

namespace ProxmoxManager.Model.Models;

public class ProxmoxNodeStatusDto
{
    [JsonPropertyName("systeminfo")]
    public SystemInfoDto? SystemInfo { get; set; }
    [JsonPropertyName("cpuinfo")]
    public CpuInfoDto? CpuInfo { get; set; }
    [JsonPropertyName("memoryinfo")]
    public MemoryInfoDto? MemoryInfo { get; set; }
    [JsonPropertyName("rootfs")]
    public RootFsInfoDto? RootFs { get; set; }
    //SystemInfo
    public class SystemInfoDto
    {
        [JsonPropertyName("pveversion")]
        public string PveVersion { get; set; } = string.Empty;
        [JsonPropertyName("kversion")]
        public string KVersion { get; set; } = string.Empty;
        [JsonPropertyName("uptime")]
        public long UptimeSeconds { get; set; }
    }
    //Cpu
    public class CpuInfoDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;
        [JsonPropertyName("cores")]
        public int CoreCount { get; set; }
        [JsonPropertyName("cpus")]
        public int Threads { get; set; }
        [JsonPropertyName("mhz")]
        public string Mhz { get; set; } = string.Empty;
    }
    //RamMemory
    public class MemoryInfoDto
    {
        [JsonPropertyName("available")]
        public long AvailableBytes { get; set; }
        [JsonPropertyName("free")]
        public long FreeBytes { get; set; }
        [JsonPropertyName("total")]
        public long TotalBytes { get; set; }
        [JsonPropertyName("used")]
        public long UsedBytes { get; set; }
        
        public int TotalRamMb => (int)(TotalBytes/(1024*1024));
    }
    //DiskMemory
    public class RootFsInfoDto
    {
        [JsonPropertyName("total")]
        public long TotalBytes { get; set; }
        [JsonPropertyName("used")]
        public long UsedBytes { get; set; }
    
        public int TotalCapacityGb => (int)(TotalBytes / (1024 * 1024 * 1024));
        public int UsedCapacityGb => (int)(UsedBytes / (1024 * 1024 * 1024));
    }
}

