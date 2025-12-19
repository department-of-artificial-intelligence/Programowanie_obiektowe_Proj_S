namespace Project.Abstractions
{
    public interface IDriver
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string LicenseNumber { get; }
        DriverStatus Status { get; }

        bool IsAvailable { get; }

        IVehicle? AssignedVehicle { get; }

        List<IOrder> Orders { get; }

        void AssignVehicle(IVehicle vehicle);
        void MarkAsAvailable();

        void AssignOrder(IOrder order);
        void RemoveOrder(IOrder order);

        public enum DriverStatus
        {
            Available,
            Assigned,
            Unavailable
        }
    }
}
