namespace Project.Model
{
    public class Driver
    {
        public enum DriverStatus
        {
            Available,
            Assigned,
            Unavailable
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;

        public DriverStatus Status { get; set; } = DriverStatus.Available;

        public Vehicle? AssignedVehicle { get; set; }

        public bool IsAvailable => Status == DriverStatus.Available;

        public Driver(int id, string firstName, string lastName, string licenseNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
        }

        public void AssignVehicle(Vehicle vehicle)
        {
            AssignedVehicle = vehicle;
            Status = DriverStatus.Assigned;
        }

        public void MarkAsAvailable()
        {
            AssignedVehicle = null;
            Status = DriverStatus.Available;
        }

        public override string ToString()
        {
            return $"Driver ID: {Id}, Name: {FirstName} {LastName}, " +
                   $"License: {LicenseNumber}, OStatus: {Status}, " +
                   $"Assigned vehicle: {(AssignedVehicle != null ? AssignedVehicle.RegistrationNumber : "None")}";
        }
    }
}
