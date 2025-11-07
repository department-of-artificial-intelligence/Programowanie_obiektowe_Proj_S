using Project.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Driver : IClassWithIEnum
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Vehicle? AssignedVehicle { get; private set; }

        public Driver(int id, string firstName, string lastName)
        {
            Id = id ;
            FirstName = firstName ;
            LastName = lastName ;
        }

        public void AssignVehicle(Vehicle vehicle)
        {
            if(IsAvailable && vehicle.IsAvailable)
            {
                AssignedVehicle = vehicle;
                IsAvailable = false;
                vehicle.AssignDriver(this);
                Console.WriteLine($"Vehicle with ID: {vehicle.Id} ({vehicle.RegistrationNumber}) assigned to driver {FirstName} {LastName}.");
            }
        }

        public void CompleteOrder()
        {
            if (AssignedVehicle != null) {
                Console.WriteLine($"Driver {FirstName} {LastName} has completed the order with vehicle with ID: {AssignedVehicle.Id} ({AssignedVehicle.RegistrationNumber}");
                IsAvailable = true;
                AssignedVehicle.MarkAsAvailable();
                AssignedVehicle = null;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, ID: {Id}. Is available? - {IsAvailable}";
        }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}
