using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public ActualStatus Status { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; } 

        public Reservation() { }
        public Reservation(int customerId, int vehicleId, int departmentId, DateTime startDate, DateTime endDate, double price, ActualStatus status)
        {
            CustomerId = customerId;
            VehicleId = vehicleId;
            DepartmentId = departmentId;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Status = status;
        }
    }
}
