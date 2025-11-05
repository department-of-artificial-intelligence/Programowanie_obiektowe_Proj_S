using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();

        
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Nie można dodać pustego pracownika.");
            }

           
            if (!this.Employees.Contains(employee))
            {
                this.Employees.Add(employee);

                
                employee.Workplace = this;
                employee.StoreId = this.Id;
            }
        }

        
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Pracownik nie może być pusty.");
            }

            
            bool removed = this.Employees.Remove(employee);

            if (removed)
            {
               
                employee.Workplace = null;
                employee.StoreId = 0; 
            }
        }

        
        public int GetStockLevel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produkt nie może być pusty.");
            }

           
            var inventoryItem = this.Inventory.FirstOrDefault(item => item.ProductId == product.Id);

            
            return inventoryItem?.Quantity ?? 0;
        }

        
        public void UpdateStock(Product product, int newQuantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produkt nie może być pusty.");
            }

            if (newQuantity < 0)
            {
                throw new ArgumentException("Stan magazynowy nie może być ujemny.", nameof(newQuantity));
            }

           
            var inventoryItem = this.Inventory.FirstOrDefault(item => item.ProductId == product.Id);

            if (inventoryItem != null)
            {
               
                inventoryItem.Quantity = newQuantity;
            }
            else
            {
               
                var newItem = new InventoryItem
                {
                    Product = product,
                    ProductId = product.Id,
                    Store = this,       
                    StoreId = this.Id, 
                    Quantity = newQuantity
                };

                this.Inventory.Add(newItem);
            }
        }

        public override string ToString()
        {
            return $"{Name} (Id: {Id}) - {City}";
        }
    }

}
