using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Model
{
    public class Store
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string PhoneNumber { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();

        public List<Order> Orders { get; set; } = new List<Order>();

        // --- ZARZĄDZANIE PRACOWNIKAMI ---

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            if (!Employees.Any(e => e.EmployeeId == employee.EmployeeId))
            {
                Employees.Add(employee);
                employee.WorkPlace = this;
                employee.StoreId = this.Id;
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            var empToRemove = Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (empToRemove != null)
            {
                Employees.Remove(empToRemove);
            }
        }

        // --- ZARZĄDZANIE MAGAZYNEM (Inventory) ---

        public int GetStockLevel(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var inventoryItem = Inventory.FirstOrDefault(item => item.ProductId == product.Id);
            return inventoryItem?.Quantity ?? 0;
        }

        public void UpdateStock(Product product, int newQuantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (newQuantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            var inventoryItem = Inventory.FirstOrDefault(item => item.ProductId == product.Id);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity = newQuantity;
            }
            else
            {
                var newItem = new InventoryItem
                {
                    Store = this,
                    StoreId = this.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = newQuantity
                };
                Inventory.Add(newItem);
            }
        }

        public override string ToString()
        {
            return $"{Name} (Id: {Id}) - {City}, {Country}";
        }
    }
}