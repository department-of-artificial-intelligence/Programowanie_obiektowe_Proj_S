using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Client: User  {
        public List<Order> OrdersHistrory { get; set; } = new List<Order>();

        public List<Order> GetActiveOrders() {
            return OrdersHistory.FindAll(o => o.Status != OrderStatus.Closed);
        }
    }
}
