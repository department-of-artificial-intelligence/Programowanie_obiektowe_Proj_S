using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Project.Model
{
    public class Order
    {

        public required Guid OrderId { get; set; }

        public required string OrderNumber { get; set; }

        public required int NumberOfProducts { get; set; }

        public required DateTime DateOrder { get; set; }



        
        public required int CustomerId { get; set; }

        public required List<OrderItem> OrderItems { get; set; }


        public string CustomerName { get; set; }



        public Order(Guid _orderId, string _ordernumber, int _numberOfProducts, DateTime _dateOrder)
        {
            OrderId = _orderId;
            OrderNumber = _ordernumber;
            NumberOfProducts = _numberOfProducts;
            DateOrder = _dateOrder;


        }

    }
}
