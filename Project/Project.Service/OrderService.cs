using Project.DAL;
using Project.Model;

namespace Project.Service
{
    public class OrderService : CrudService<Order>
    {
        public OrderService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
