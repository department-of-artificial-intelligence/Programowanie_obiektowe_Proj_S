using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Model
{
    public class Store
    {
        private string _phoneNumber;

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }

        public required string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                string pattern = @"^\+\d{2}\d{9}$";

                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Numer telefonu sklepu musi być w formacie: +XXYYYYYYYYY (np. +48123456789)");
                }
                _phoneNumber = value;
            }
        }

        public List<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public void HireEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            if (Employees.Any(e => e.EmployeeId == employee.EmployeeId))
            {
                throw new InvalidOperationException($"Pracownik o ID {employee.EmployeeId} jest już zatrudniony.");
            }

            Employees.Add(employee);
        }

        public void FireEmployee(int employeeId)
        {
            var employee = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Nie znaleziono pracownika o ID {employeeId}.");
            }

            Employees.Remove(employee);
        }

        public void AddToInventory(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Ilość dodawanego towaru musi być dodatnia.");

            var existingItem = Inventory.FirstOrDefault(i => i.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                
                Inventory.Add(new InventoryItem
                {
                    Product = product,
                    ProductId = product.Id,
                    Quantity = quantity,
                    Store = this,
                    StoreId = this.Id
                });
            }
        }

        public void RemoveFromInventory(int productId, int quantityToRemove)
        {
            if (quantityToRemove <= 0) throw new ArgumentException("Ilość do usunięcia musi być dodatnia.");

            var item = Inventory.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                throw new InvalidOperationException($"Produkt o ID {productId} nie znajduje się w magazynie.");
            }

            if (item.Quantity < quantityToRemove)
            {
                throw new InvalidOperationException($"Niewystarczająca ilość towaru. Masz: {item.Quantity}, chcesz usunąć: {quantityToRemove}.");
            }

            item.Quantity -= quantityToRemove;

            if (item.Quantity == 0)
            {
                Inventory.Remove(item);
            }
        }


   

        public void UpdateStock(int productId, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new ArgumentException("Ilość towaru nie może być ujemna.");
            }

            var item = Inventory.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                throw new InvalidOperationException($"Produkt o ID {productId} nie znajduje się w magazynie. Użyj metody AddToInventory, aby go dodać.");
            }

            if (newQuantity == 0)
            {
                Inventory.Remove(item);
            }
            else
            {
                item.Quantity = newQuantity;
            }
        }


        public override string ToString()
        {
            return $"Sklep #{Id}: {Name} ({City}) | Tel: {PhoneNumber}";
        }
    }
}