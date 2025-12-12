using WypozyczalniaSamochodow.Model;

public interface ICustomer
{
    void ShowCustomers(Branch branch);
    void AddCustomer(Customer customer, Branch branch);
    void RemoveCustomer(int customerId, Branch branch);
    bool HasCustomers(Branch branch);
}
