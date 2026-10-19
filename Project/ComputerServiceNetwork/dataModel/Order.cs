using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Order {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public decimal LaborCost { get; set; }

        public Client Client { get; set; }
        public Technician Technician { get; set; }
        public Device Device { get; set; }
        public List<SparePart> UsedParts { get; set; } = new List<SparePart>();

        public decimal CalculateTotal() {
            decimal total = LaborCost;
            foreach (var part in UsedParts) {
                total += part.Price;
            }
            return total;
        }

        public string GenerateReceipt() {
            return $"Order #{OrderId} | Client: {Client?.FullName} | Device: {Device?.Model} | Price: {CalculateTotal()} PLN | Status: {Status}";
        }
    }
}
