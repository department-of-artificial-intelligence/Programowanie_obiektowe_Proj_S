using System;

namespace Project.Model
{
    public class InventoryItem
    {
        public int Id { get; set; } 

        
        public int StoreId { get; set; }
        public required Store Store { get; set; }

        
        public int ProductId { get; set; }
        public required Product Product { get; set; }

        
        public int Quantity { get; set; }
    }
}