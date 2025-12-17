using Project.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Driver : IDriver
    {
        public DriverStatus Status { get; set; } = DriverStatus.Available;

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;

        [NotMapped]
        public List<IOrder> Orders { get; private set; } = new List<IOrder>();

        public Vehicle? AssignedVehicle { get; set; }
        
        public bool IsAvailable => Status == DriverStatus.Available || Status == DriverStatus.Assigned;

        public Driver() { }
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

        public void AssignOrder(IOrder order)
        {
            if (!Orders.Contains(order))
            {
                Orders.Add(order);
                Status = DriverStatus.Assigned;
            }
        }

        public void RemoveOrder(IOrder order)
        {
            if (Orders.Contains(order))
            {
                Orders.Remove(order);
                if (Orders.Count == 0)
                    Status = DriverStatus.Available;
            }
        }

        public override string ToString()
        {
            return $"Driver ID: {Id}, Name: {FirstName} {LastName}, " +
                   $"License: {LicenseNumber}, Status: {Status}, " +
                   $"Assigned vehicle: {(AssignedVehicle != null ? AssignedVehicle.RegistrationNumber : "None")}";
        }
    }
}
