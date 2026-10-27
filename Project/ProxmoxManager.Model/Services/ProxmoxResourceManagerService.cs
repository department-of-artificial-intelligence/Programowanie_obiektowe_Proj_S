using System.Text.Json;
using ProxmoxManager.Model.Models;

namespace ProxmoxManager.Model.Services;

public class ProxmoxResourceManagerService
{
    public ProxmoxNode Node { get; private set; } = new ProxmoxNode();

    public bool ImportStatusFromJson(string rawJson)
    {
        if(string.IsNullOrWhiteSpace(rawJson))
            return false;
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var statusDto = JsonSerializer.Deserialize<ProxmoxNodeStatusDto>(rawJson, options);
        
        if (statusDto == null)
            return false;
        
        Node.UpdateNodeName("pve-real-node");
        Node.UpdateIsConnected(true);

        if (statusDto.CpuInfo != null && statusDto.MemoryInfo != null)
        {
            Node.UpdateHardwareSpecs(
                cpuModel:statusDto.CpuInfo.Model,
                coreCount:statusDto.CpuInfo.CoreCount,
                threads:statusDto.CpuInfo.Threads,
                ramMb:statusDto.MemoryInfo.TotalRamMb
                );
        }
        if(statusDto.RootFs != null)
        {
            Node.UpdateStoragePools(statusDto.RootFs.TotalCapacityGb, statusDto.RootFs.UsedCapacityGb);
        }
        if(statusDto.SystemInfo != null)
        {
            Node.UpdateSystemInfo(statusDto.SystemInfo.KVersion,statusDto.SystemInfo.UptimeSeconds);
        }
        return true;
    }
}