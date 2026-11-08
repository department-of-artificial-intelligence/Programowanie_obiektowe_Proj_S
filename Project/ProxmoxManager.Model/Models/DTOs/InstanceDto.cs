using System.Text.Json.Serialization;

namespace ProxmoxManager.Model.Models.DTOs;

public class VirtualMachineDto
{
    [JsonPropertyName("vmid")]
    public int Vmid { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("ram_mb")]
    public int RamMb { get; set; }
    [JsonPropertyName("cores")]
    public int Cores { get; set; }
    [JsonPropertyName("maxdisk")]
    public long MaxDisk { get; set; }
    [JsonPropertyName("enable_qemu_agent")]
    public bool EnableQemuAgent { get; set; }
    [JsonPropertyName("mounted_iso")]
    public string MountedIso { get; set; } = string.Empty;
}

public class LxcContainerDto
{
    [JsonPropertyName("vmid")]
    public int Vmid { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("ram_mb")]
    public int RamMb { get; set; }
    [JsonPropertyName("cores")]
    public int Cores { get; set; }
    [JsonPropertyName("maxdisk")]
    public long MaxDisk { get; set; }
    [JsonPropertyName("os_template")]
    public string OsTemplate { get; set; } = string.Empty;
    [JsonPropertyName("unprivileged")]
    public bool Unprivileged { get; set; }
}