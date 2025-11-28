using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public enum EmployeeRole
    {
        Manager, Mechanic
    }

    public class Employee : Person
    {
        public int EmployeeId { get; set; }
        public EmployeeRole Role { get; set; }

        
        public Employee(string firstName, string lastName, int id, EmployeeRole role)
            : base(firstName, lastName)
        {
            EmployeeId = id;
            Role = role;
        }

        
        public void RepairBike(Bicycle bike)
        {
            if (Role == EmployeeRole.Mechanic)
            {
                Console.WriteLine($"Mechanic {FirstName} is repairing bike {bike.Id}...");

                
                if (bike.CurrentStation != null)
                {
                    bike.Return(bike.CurrentStation);
                    Console.WriteLine("Bike fixed and available at the station!");
                }
                else
                {
                    Console.WriteLine("Error: Bike belongs to no station. Assign a station first.");
                }
            }
            else
            {
                Console.WriteLine("Error: Only mechanics can repair bikes.");
            }
        }

        
        public void ProcessRental(Customer customer, Bicycle bike)
        {
            if (Role == EmployeeRole.Manager)
            {
                
                bool addedToCustomer = customer.AddBicycle(bike);

                if (addedToCustomer)
                {
                    
                    try
                    {
                        bike.Rent(); 
                        Console.WriteLine($"Manager {FirstName} approved rental for {customer.LastName}. Bike {bike.Id} rented.");
                    }
                    catch (Exception ex)
                    {
                        
                        customer.RemoveBicycle(bike);
                        Console.WriteLine($"Rental failed: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Rental denied. Customer {customer.LastName} has reached the limit or bike is invalid.");
                }
            }
            else
            {
                Console.WriteLine("Error: Only managers can process rentals.");
            }
        }

        
        public override string ToString()
        {
            return $"[Staff] {Role}: {FirstName} {LastName} (ID: {EmployeeId})";
        }

        
        public void ShowInfo()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
