using System.Text.Json.Serialization;

namespace ProxmoxManager.Model.Models.DTOs;

public class VirtualMachineTemplateDto
{
    [JsonPropertyName("template_name")]
    public string TemplateName { get; set; } = string.Empty;
    [JsonPropertyName("default_ram_mb")]
    public int DefaultRamMb { get; set; }
    [JsonPropertyName("default_cores")]
    public int DefaultCores { get; set; }
    [JsonPropertyName("enable_qemu_agent")]
    public bool EnableQemuAgent { get; set; }
    [JsonPropertyName("default_iso")]
    public string DefaultIso { get; set; } = string.Empty;
}

public class LxcContainerTemplateDto
{
    [JsonPropertyName("template_name")]
    public string TemplateName { get; set; } = string.Empty;
    [JsonPropertyName("default_ram_mb")]
    public int DefaultRamMb { get; set; }
    [JsonPropertyName("default_cores")]
    public int DefaultCores { get; set; }
    [JsonPropertyName("unprivilaged")]
    public bool Unprivilaged { get; set; }
    [JsonPropertyName("os_template")]
    public string OsTemplate { get; set; } = string.Empty;
}