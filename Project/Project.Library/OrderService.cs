
using Project.Model;


namespace Project.Services;

public class OrderService
{
    public void ProcessOrder(Order order, Storage storage)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));
        ArgumentNullException.ThrowIfNull(storage, nameof(storage));

        
        
    }
}
    

