using WypozyczalniaSamochodow.Model;

public interface ICustomerService
{
    IEnumerable<Customer> GetCustomerByBranch(int branchId);
    void AddCustomer(Customer customer, int branchId);
    void UpdateCustomer(Customer customer);
    void RemoveCustomer(int customerId, int branchId);
    IEnumerable<Customer> SearchCustomers(int branchId, string? lastName, string? licenseNumber, int? minPoints);
}
