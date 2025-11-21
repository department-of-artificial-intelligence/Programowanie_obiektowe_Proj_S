using System;
using System.Collections.Generic;
using System.Linq;
using WypozyczalniaSamochodow.Model;

public class CustomerLogic : ICustomer
{
    private readonly List<Customer> _customers;

    public CustomerLogic(List<Customer> customers)
    {
        _customers = customers;
    }

    public void ShowCustomers()
    {
        if (_customers.Count == 0)
        {
            Console.WriteLine("Brak klientów.");
            return;
        }

        foreach (var customer in _customers)
        {
            Console.WriteLine(customer);
        }
    }

    public void AddCustomer(Customer customer)
    {
        int newId = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
        customer.Id = newId;
        _customers.Add(customer);
        Console.WriteLine($"Dodano klienta: {customer.FirstName} {customer.LastName}");
    }

    public void RemoveCustomer(int customerId)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == customerId);
        if (customer != null)
        {
            _customers.Remove(customer);
            Console.WriteLine($"Usunięto klienta {customer.FirstName} {customer.LastName}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono klienta.");
        }
    }
}