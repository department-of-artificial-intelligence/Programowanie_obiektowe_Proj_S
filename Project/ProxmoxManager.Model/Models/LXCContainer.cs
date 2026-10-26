namespace ProxmoxManager.Model.Models;

public class LXCContainer : VirtualInstance
{
    public string OsTemplate { get; private set; } = string.Empty;
    public bool Unprivileged { get; private set; }

    public override double CalculateHostOverheadMb()
    {
        //Example overhead calculation for LXC containers
        return AllocatedRamMb * 0.05;
    }
}