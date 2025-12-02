using Project.Abstractions;
using Project.Model;

namespace Project.Model
{
    public class Driver : IDriver
    {
        public DriverStatus Status { get; set; } = DriverStatus.Available;

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;


        public Vehicle? AssignedVehicle { get; set; }

        public bool IsAvailable => Status == DriverStatus.Available;

        public Driver(int id, string firstName, string lastName, string licenseNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
        }

        IVehicle? IDriver.AssignedVehicle => AssignedVehicle;

        public void AssignVehicle(IVehicle vehicle)
        {
            if (vehicle is Vehicle concreteVehicle)
            {
                AssignedVehicle = concreteVehicle;
                Status = DriverStatus.Assigned;
            }
            else
            {
                throw new ArgumentException("ERROR - Provided vehicle is not a concrete Vehicle type.");
            }
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
