namespace Project.Model
{
    public interface IDeliveryMethod
    {
        decimal Cost { get; }
        string Name { get; }
        int EstimatedDays { get; }
        string TrackingId { get; }
        string GetDeliveryDetails();
    }
}