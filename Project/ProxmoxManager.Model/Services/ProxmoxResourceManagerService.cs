using System.Text.Json;
using ProxmoxManager.Model.Models.DTOs;
using ProxmoxManager.Model.Models.Domain;

namespace ProxmoxManager.Model.Services;

public class ProxmoxResourceManagerService
{
    public List<VirtualMachineTemplateDto> VmTemplates { get; private set; } = [];
    public List<LxcContainerTemplateDto> LxcTemplates { get; private set; } = [];
    public ProxmoxNode Node { get; private set; } = new ProxmoxNode();

    public SnapshotDto? DeserializeSnapshot(string rawJson)
    {
        if (string.IsNullOrWhiteSpace(rawJson))
            return null;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<SnapshotDto>(rawJson,options);
    }
    public bool ImportStatusFromJson(string rawJson)
    {
        var snapshotDto = DeserializeSnapshot(rawJson);
        if (snapshotDto?.NodeStatus == null)
            return false;
        
        var statusDto = snapshotDto.NodeStatus;
        Node.UpdateNodeName(statusDto.Hostname);
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
            Node.UpdateStoragePools(
                statusDto.RootFs.TotalCapacityGb, 
                statusDto.RootFs.UsedCapacityGb
                );
        }
        if(statusDto.SystemInfo != null)
        {
            Node.UpdateSystemInfo(
                statusDto.SystemInfo.KVersion,
                statusDto.SystemInfo.UptimeSeconds
                );
        }
        return true;
    }
    public bool ImportInstancesFromJson(string rawJson)
    {
        var snapshotDto = DeserializeSnapshot(rawJson);
        if (snapshotDto?.BaselineInstances == null)
            return false;
        
        var vmInstance = snapshotDto.BaselineInstances.VirtualMachines;
        var lxcContainer = snapshotDto.BaselineInstances.LxcContainers;
        foreach (var vmDto in vmInstance)
        {
            var vm = new VirtualMachine();

            vm.ConfigureVirtualInstance(
                vmId: vmDto.Vmid,
                name: vmDto.Name,
                ramMb: vmDto.RamMb,
                cores: vmDto.Cores,
                diskSizeGb: (int)(vmDto.MaxDisk / (1024L * 1024 * 1024)),
                isRunning: vmDto.Status == "running"
            );
            vm.ConfigureVirtualMachine(enableQemuAgent: vmDto.EnableQemuAgent,mountedIsoFile: vmDto.MountedIso);
            if (!Node.Instances.Any(i => i.VmId == vm.VmId))
            {
                Node.AddInstance(vm);
            }
        }

        foreach (var vmDto in lxcContainer)
        {
            var lxc = new LxcContainer();

            lxc.ConfigureVirtualInstance(
                vmId: vmDto.Vmid,
                name: vmDto.Name,
                ramMb: vmDto.RamMb,
                cores: vmDto.Cores,
                diskSizeGb: (int)(vmDto.MaxDisk / (1024L * 1024 * 1024)),
                isRunning: vmDto.Status == "running"
            );

            lxc.ConfigureContainer(osTemplate: vmDto.OsTemplate, unprivileged: vmDto.Unprivileged);
            if (!Node.Instances.Any(i => i.VmId == lxc.VmId))
            {
                Node.AddInstance(lxc);
            }
        }
        return true;
    }
    /*public bool ImportStorageFromJson(string rawJson)
    public bool ImportTemplatesFromJson(string rawJson)*/
}