using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Order
    {
        public enum OrderStatus { Pending, InProgress, Completed, Cancelled }
        public int Id { get; set; }
        public required string LoadDesc { get; set; }
        public required string LoadingAddress { get; set; }
        public required string UnloadingAdress { get; set; }
        //public 

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Driver? AssigneDriver { get; private set; }
        public Vehicle? AssignedVehicle { get; private set; }

        public void AssignOrder(Driver driver)
        {
            if(Status != OrderStatus.Pending)
            {
                Console.WriteLine("ERROR - This order does not exist.");
                return;
            }
            if(driver.IsAvailable || driver.AssignedVehicle == null)
                {
                Console.WriteLine($"ERROR - Driver: {driver.FirstName} {driver.LastName} is not available now.");
                return;
                }
            {
                this.AssignedDriver = driver;
                this.AssignedVehicle = driver.AssignedVehicle;
                this.Status = OrderStatus.InProgress;
                Console.WriteLine($"Order {Id} has been assigned to driver {driver.FirstName} {driver.LastName} and is now in progress.");
            }
        }
        }
    }
}
