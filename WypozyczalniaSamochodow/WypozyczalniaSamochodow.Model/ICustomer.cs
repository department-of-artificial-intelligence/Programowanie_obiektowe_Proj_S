using WypozyczalniaSamochodow.Model;

public interface ICustomer
{
    void ShowCustomers();
    void AddCustomer(Customer customer);
    void RemoveCustomer(int customerId);
}