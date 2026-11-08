using System.Text.Json.Serialization;

namespace ProxmoxManager.Model.Models.DTOs;

public class SnapshotDto
{
    [JsonPropertyName("node_status")] 
    public StatusDto? NodeStatus { get; set; } = new();
    [JsonPropertyName("baseline_instances")]
    public BaselineInstancesDto? BaselineInstances { get; set; } = new();
    [JsonPropertyName("templates")]
    public TemplateCollectionDto? Templates { get; set; } = new();
    [JsonPropertyName("storage")]
    public List<StorageDto> Storage { get; set; } = new();
}

public class BaselineInstancesDto
{
    [JsonPropertyName("virtual_machines")]
    public List<VirtualMachineDto> VirtualMachines { get; set; } = new();
    [JsonPropertyName("lxc_containers")]
    public List<LxcContainerDto> LxcContainers { get; set; } = new();
}

public class TemplateCollectionDto
{
    [JsonPropertyName("virtual_machines")]
    public List<VirtualMachineTemplateDto> VirtualMachines { get; set; } = new();
    [JsonPropertyName("lxc_containers")]
    public List<LxcContainerTemplateDto> LxcContainers { get; set; } = new();
}