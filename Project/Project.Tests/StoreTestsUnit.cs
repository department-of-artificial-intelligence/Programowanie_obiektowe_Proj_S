using Xunit;
using System;
using System.Linq;
using Project.Model;
using Project.Model.People;

namespace Project.Tests
{
    public class StoreTests
    {
        private Store CreateStore()
        {
            return new Store
            {
                Id = 1,
                Name = "Test Store",
                Address = "Test St",
                City = "City",
                Region = "Reg",
                PostalCode = "00-000",
                Country = "PL",
                PhoneNumber = "+48123123123"
            };
        }

        private Employee CreateEmployee(int id)
        {
            return new Employee(
                id,
                "Jan",
                "Test",
                "j@t.pl",
                "ul. Firmowa 1",
                "+48000000000",
                3000m,
                EmployeePosition.Manager
            );
        }

        private Product CreateProduct(int id)
        {
            return new Product
            {
                Id = id,
                Name = "Generic Product",
                Manufacturer = "Manufacturer",
                Price = 2000m,
                Category = ProductCategory.Computer
            };
        }

        [Fact]
        public void HireEmployee_ShouldAddEmployee_WhenNotExists()
        {
            var store = CreateStore();
            var emp = CreateEmployee(1);

            store.HireEmployee(emp);

            Assert.Single(store.Employees);
            Assert.Equal(1, store.Employees.First().EmployeeId);
        }

        [Fact]
        public void HireEmployee_ShouldThrowException_WhenEmployeeExists()
        {
            var store = CreateStore();
            var emp = CreateEmployee(1);
            store.HireEmployee(emp);

            Assert.Throws<InvalidOperationException>(() => store.HireEmployee(emp));
        }

        [Fact]
        public void FireEmployee_ShouldRemoveEmployee_WhenExists()
        {
            var store = CreateStore();
            var emp = CreateEmployee(1);
            store.HireEmployee(emp);

            store.FireEmployee(1);

            Assert.Empty(store.Employees);
        }

        [Fact]
        public void AddToInventory_ShouldAddNewItem_AndAssignStoreReference()
        {
            var store = CreateStore();
            var product = CreateProduct(10);

            store.AddToInventory(product, 5);

            Assert.Single(store.Inventory);
            var item = store.Inventory.First();

            Assert.Equal(5, item.Quantity);
            Assert.Equal(product.Id, item.ProductId);
            Assert.NotNull(item.Store);
            Assert.Equal(store.Id, item.Store.Id);
        }

        [Fact]
        public void AddToInventory_ShouldIncreaseQuantity_WhenItemExists()
        {
            var store = CreateStore();
            var product = CreateProduct(10);

            store.AddToInventory(product, 5);
            store.AddToInventory(product, 3);

            Assert.Equal(8, store.Inventory.First().Quantity);
        }

        [Fact]
        public void PhoneNumber_ShouldThrowException_WhenFormatIsInvalid()
        {
            var store = CreateStore();
            Assert.Throws<ArgumentException>(() => store.PhoneNumber = "12345");
        }
    }
}