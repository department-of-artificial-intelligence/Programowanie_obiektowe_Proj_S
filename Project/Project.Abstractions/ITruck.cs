namespace Project.Abstractions
{
    public interface ITruck : IVehicle
    {
        int MaxPayloadKg { get; }
    }
}