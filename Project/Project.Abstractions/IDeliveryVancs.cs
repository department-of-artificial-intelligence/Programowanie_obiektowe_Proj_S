namespace Project.Abstractions
{
    public interface IDeliveryVan : IVehicle
    {
        float MaxVolumeCubicMeters { get; }
    }
}