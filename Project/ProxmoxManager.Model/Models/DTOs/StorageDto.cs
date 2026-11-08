using System.Text.Json.Serialization;

namespace ProxmoxManager.Model.Models.DTOs;

public class StorageDto
{
    [JsonPropertyName("storage")]
    public string? Storage { get; set; } = string.Empty;
    [JsonPropertyName("type")]
    public string? Type { get; set; } = string.Empty;
    [JsonPropertyName("total")]
    public long? TotalBytes { get; set; }
    [JsonPropertyName("used")]
    public long? UsedBytes { get; set; }
    [JsonPropertyName("available")]
    public long? AvailableBytes { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; } = string.Empty;
    
}