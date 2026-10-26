namespace ProxmoxManager.Model.Interfaces;

public interface IResourceAllocatable
{
    int GetAllocatedRamMb();
    int GetAllocatedCores();
}