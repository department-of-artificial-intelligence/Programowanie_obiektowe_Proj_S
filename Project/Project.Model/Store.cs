using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Item> Inventory { get; set; } = new List<Item>();

        
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Nie można dodać pustego pracownika.");
            }

           
            if (!this.Employees.Contains(employee))
            {
                this.Employees.Add(employee);

                
                employee.WorkPlace = this;
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
               
                employee.WorkPlace = null;
                employee.StoreId = 0; 
            }
        }

        
        public int GetStockLevel(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Produkt nie może być pusty.");
            }

           
            var inventoryItem = this.Inventory.FirstOrDefault(item => item.Id == item.Id);


            return inventoryItem;
        }

        
        public void UpdateStock(Item product, int newQuantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produkt nie może być pusty.");
            }

            if (newQuantity < 0)
            {
                throw new ArgumentException("Stan magazynowy nie może być ujemny.", nameof(newQuantity));
            }

           
            var inventoryItem = this.Inventory.FirstOrDefault(item => item.Id == product.Id);

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

        public static implicit operator string(Store v)
        {
            throw new NotImplementedException();
        }
    }

}
