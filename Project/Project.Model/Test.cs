using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Text;
using Project.Model;

namespace Project.Model
{
    [TestClass()]
    public class RentalSystemTests
    {
        private Station _station;
        private Customer _customer;
        private Employee _manager;
        private Bicycle _bike;
        public RentalSystemTests()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        [TestInitialize]
        public void Setup()
        {
            _station = new Station("Test Station", 25);
            _bike = new Bicycle {Id = 67, Model = "TestBike", Type = "City", Price = 50 };
            _station.AddBicycle(_bike);

            _customer = new Customer("Kowalski", "Test", "Test Adress");
            _manager = new Employee("Boss","Manager",1,EmployeeRole.Manager);
        }
        public void Test_RentBike()
        {
            _manager.ProcessRental(_customer, _bike);
            Assert.AreEqual(BicycleStatus.Rented, _bike.Status);
            Assert.AreEqual(1, _customer.BicycleCount);
        }
        [TestMethod]
        public void Test_RentBicycle_WhenLimitReached()
        {
            _customer.AddBicycle(new Bicycle { Id = 1 });
            _customer.AddBicycle(new Bicycle { Id = 2 });
            _customer.AddBicycle(new Bicycle { Id = 3 });
            _ = new Bicycle { Id = 4, Status = BicycleStatus.Available };
        }


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        
    }

    internal class TestClassAttribute : Attribute
    {
    }
}
