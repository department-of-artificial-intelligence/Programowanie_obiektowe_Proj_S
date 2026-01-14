using Project.Model.Stores;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model.Orders
{
    public class OrderItem
    {
        public int OrderItemId { get; private set; } 

        public required Product Product { get; set; }


        public decimal UnitPrice { get; set; }


        private int _quantity;
        public required int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Quantity), "Ilość musi być większa niż zero!");
                _quantity = value;
            }
        }

        
        public OrderItem() { }


        [SetsRequiredMembers]
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            UnitPrice = product.Price;
        }


        public decimal GetLineTotal()
        {
            
            return UnitPrice * Quantity;
        }

        public override string ToString()
        {
            
            return $"{Product.Name} (x{Quantity}) - {GetLineTotal():C}";
        }
    }
}