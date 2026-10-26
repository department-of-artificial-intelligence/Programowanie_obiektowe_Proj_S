namespace ProxmoxManager.Model.Models;

public class StoragePools
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int TotalCapacityGb { get; private set; }
    public int UsedCapacityGb { get; private set; }
    public int ProxmoxNodeId { get; private set; }
}