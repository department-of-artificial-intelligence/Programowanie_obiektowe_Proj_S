namespace Project.Model
{
    public interface IPayment
    {
        bool Pay(decimal amount, Customer customer);
    }
}