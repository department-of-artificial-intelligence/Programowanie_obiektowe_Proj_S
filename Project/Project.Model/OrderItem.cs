using System;



namespace Project.Model
{

    public class OrderItem
    {
        public int Id { get; set; } 

        
        public int OrderId { get; set; }
        public required Order Order { get; set; }

        
        public int ProductId { get; set; }
        public required Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

      

        public decimal CalculateLineTotal()
        {
            return UnitPrice * Quantity;
        }

        public override string ToString()
        {
            return $"{Product.Name} * {Quantity} = ({CalculateLineTotal():C})";
        }
    }



}

