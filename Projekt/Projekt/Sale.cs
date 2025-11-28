using System;
using System.Collections.Generic;

namespace Projekt
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public Employee Seller { get; set; } 
        public List<SaleItem> Items { get; set; } 
        public float TotalAmount { get; set; } 

        
        public Sale(int id, Employee seller)
        {
            Id = id;
            Seller = seller ?? throw new ArgumentNullException(nameof(seller)); 
            SaleDate = DateTime.Now;
            Items = new List<SaleItem>();
            TotalAmount = 0;
        }

        
        public Sale()
        {
            Id = 0;
            Seller = null;
            SaleDate = DateTime.Now; 
            Items = new List<SaleItem>(); 
            TotalAmount = 0;
        }

      
        public void AddItem(SaleItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item)); 

            Items.Add(item);
            TotalAmount += item.Subtotal;
        }

       
        public override string ToString()
        {
            var itemsDescription = string.Join(", ", Items); 
            return $"Sale ID: {Id}, Date: {SaleDate}, Seller: {Seller?.FullName}, Items: {itemsDescription}, Total: {TotalAmount:C}";
        }
    }
}
