using Project.Model.People;

namespace Project.Model.Interfaces
{
    
    public interface IPayment
    {
        bool Pay(decimal amount, Customer customer);
    }
}