using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class Transaction : ID
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    

    public Transaction(int id, int workerId, int productId,
                          DateTime time, int quantity, decimal totalPrice)
        {
            Id = id;
            WorkerId = workerId;
            ProductId = productId;
            Time = time;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }
    }
   
}
