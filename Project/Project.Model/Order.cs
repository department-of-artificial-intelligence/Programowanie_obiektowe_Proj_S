using System;
using Project.Abstractions;

namespace Project.Model
{
    public class Order
    {
        public int Id { get; private set; }
        public string LoadingDescription { get; private set; }
        public string LoadingAddress { get; private set; }
        public string UnloadingAddress { get; private set; }

        public enum OrderStatus
        {
            Pending,
            InProgress,
            Completed,
            Cancelled
        }
        public OrderStatus OStatus { get; private set; } = OrderStatus.Pending;
        public Driver? AssignedDriver { get; private set; }
        public Vehicle? AssignedVehicle { get; private set; }

        public Order(int id, string loadDesc, string loadingAddress, string unloadingAddress)
        {
            Id = id;
            LoadingDescription = loadDesc;
            LoadingAddress = loadingAddress;
            UnloadingAddress = unloadingAddress; // Poprawione
        }

        public void AssignOrder(Driver driver)
        {
            if (OStatus != OrderStatus.Pending)
            {
                Console.WriteLine($"ERROR - Order {Id} cannot be assigned. Only 'pending' orders can be assigned.");
                return;
            }

            if (!driver.IsAvailable || driver.AssignedVehicle == null)
            {
                Console.WriteLine($"ERROR - Driver: {driver.FirstName} {driver.LastName} is not available now.");
                return;
            }

            AssignedDriver = driver;
            AssignedVehicle = driver.AssignedVehicle;
            OStatus = OrderStatus.InProgress;
            Console.WriteLine($"Order {Id} has been assigned to driver {driver.FirstName} {driver.LastName} and is now in progress.");
        }

        public override string ToString()
        {
            return $"Order with ID: {Id} \n" +
                   $"Loading Address: {LoadingAddress}\n" +
                   $"Unloading Address: {UnloadingAddress}\n" +
                   $"Loading Description: {LoadingDescription}";
        }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}
