using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Model;
using System.Collections.Generic;

namespace Project.Model.Tests
{
    [TestClass]
    public class RentalTests
    {
        private Station _station;
        private Customer _customer;
        private Employee _manager;
        private Bicycle _bike;

        [TestInitialize]
        public void Setup()
        {
            _station = new Station("Central Park Station", 20);

            _bike = new Bicycle { Id = 101, Model = "Giant Escape", Type = "City", Price = 15 };
            _station.AddBicycle(_bike);

            _customer = new Customer("John", "Doe", "5th Avenue, NY");
            _manager = new Employee("Alice", "Smith", 1, EmployeeRole.Manager);
        }

        [TestMethod]
        public void RentBike_SuccessfulRental_UpdatesStatusAndCount()
        {
            _manager.ProcessRental(_customer, _bike);

            Assert.AreEqual(BicycleStatus.Rented, _bike.Status, "Bike status should change to Rented.");
            Assert.AreEqual(1, _customer.BicycleCount, "Customer should have 1 bike.");
        }

        [TestMethod]
        public void RentBike_LimitReached_DeniesRental()
        {
            _customer.AddBicycle(new Bicycle { Id = 1 });
            _customer.AddBicycle(new Bicycle { Id = 2 });
            _customer.AddBicycle(new Bicycle { Id = 3 });

            Bicycle extraBike = new Bicycle { Id = 999 };

            _manager.ProcessRental(_customer, extraBike);

            Assert.AreEqual(3, _customer.BicycleCount, "Customer cannot rent more than 3 bikes.");
            Assert.AreEqual(BicycleStatus.Available, extraBike.Status, "Bike should remain Available.");
        }
    }
}