using NUnit.Framework;
using Project.Model;
using System.Collections.Generic;

namespace Project.Model.Tests
{
    [TestFixture]
    public class RentalTests
    {
        private Station _station;
        private Customer _customer;
        private Employee _manager;
        private Bicycle _bike;

        [SetUp]
        public void Setup()
        {
            _station = new Station("Central Station", 20);

            _bike = new Bicycle { Id = 101, Model = "Giant Escape", Type = "City", Price = 15 };
            _station.AddBicycle(_bike);

            _customer = new Customer("John", "Doe", "5th Avenue, NY");
            _manager = new Employee("Alice", "Smith", 1, EmployeeRole.Manager);
        }

        [Test]
        public void RentBike_SuccessfulRental_UpdatesStatusAndCount()
        {
            _manager.ProcessRental(_customer, _bike);

            Assert.That(_bike.Status, Is.EqualTo(BicycleStatus.Rented));
            Assert.That(_customer.BicycleCount, Is.EqualTo(1));
        }

        [Test]
        public void RentBike_LimitReached_DeniesRental()
        {
            _customer.AddBicycle(new Bicycle { Id = 1 });
            _customer.AddBicycle(new Bicycle { Id = 2 });
            _customer.AddBicycle(new Bicycle { Id = 3 });

            Bicycle extraBike = new Bicycle { Id = 999};

            _manager.ProcessRental(_customer, extraBike);

            Assert.That(_customer.BicycleCount, Is.EqualTo(3));
            Assert.That(extraBike.Status, Is.EqualTo(BicycleStatus.Available));
        }
    }
}