using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Client: User  {
        public List<Order> OrdersHistory { get; set; } = new List<Order>();

        public List<Order> GetActiveOrders() {
            return OrdersHistory.FindAll(o => o.Status != OrderStatus.Closed);
        }

        public override string GetRoleDescription() => "Customer";

        public override string GetContactInfo()
        {
            return $"[Client] {base.GetContactInfo()} | Total orders: {OrdersHistory.Count}";
        }
    }
}
