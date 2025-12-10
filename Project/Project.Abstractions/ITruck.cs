namespace Project.Abstractions
{
    public interface ITruck : IVehicle
    {
        int MaxPayLoadKg { get; }
    }
}