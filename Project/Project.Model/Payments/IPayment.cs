using Project.Model.People;

namespace Project.Model.Payments
{
    
    public interface IPayment
    {
        bool Pay(decimal amount, Customer customer);
    }
}