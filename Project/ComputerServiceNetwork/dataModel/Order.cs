using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Order : IReportable {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public decimal LaborCost { get; set; }

        public Client? Client { get; set; } = null!;
        public Technician? Technician { get; set; } = null!;
        public Device? Device { get; set; } = null!;
        public List<SparePart> UsedParts { get; set; } = new List<SparePart>();

        public decimal CalculateTotal()
        {
            return LaborCost + UsedParts.Sum(part => part.Price);
        }

        public string GenerateReceipt()
        {
            return $"Order #{Id} | Client: {Client?.FullName} | Device: {Device?.Model} | Price: {CalculateTotal()} PLN | Status: {Status}";
        }

        public string GetReportLine() => GenerateReceipt();
    }
}
