using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class SparePart {
        public int PartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public bool DeductStock(int quantity) {
            if (QuantityInStock >= quantity) {
            QuantityInStock -= quantity;
                return true;
            }
            return false;
        }
    }
}
