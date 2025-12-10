namespace Project.Abstractions
{
    public enum DriverStatus // do modelu
    {
        Available,
        Assigned,
        Unavailable
    }

    public interface IDriver
    {
        public DriverStatus Status { get; set; }

        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string LicenseNumber { get; set; }

        bool IsAvailable { get; }

        IVehicle? AssignedVehicle { get; }

        void AssignVehicle(IVehicle vehicle);
        void MarkAsAvailable();
    }
}
