namespace ProxmoxManager.Model.Models.Domain;

public class LxcContainer : VirtualInstance
{
    public string OsTemplate { get; private set; } = string.Empty;
    public bool Unprivileged { get; private set; }

    public override double CalculateHostOverheadMb()
    {
        //Example overhead calculation for LXC containers
        return AllocatedRamMb * 0.05;
    }
    public void ConfigureContainer(string osTemplate, bool unprivileged)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(osTemplate, nameof(osTemplate));
        OsTemplate = osTemplate;
        Unprivileged = unprivileged;
    }
}