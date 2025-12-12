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

    public void ShowCustomers(Branch branch)
    {
        if (branch.Customers is null || branch.Customers.Count <= 0)
        {
            Console.WriteLine("\nBrak klientów w tym oddziale.");
            return;
        }

        foreach (var customer in branch.Customers)
        {
            Console.WriteLine(customer);
        }
    }

    public void AddCustomer(Customer customer, Branch branch)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName) || string.IsNullOrWhiteSpace(customer.LastName))
        {
            Console.WriteLine("Imię i nazwisko są wymagane.");
            return;
        }
        if (string.IsNullOrWhiteSpace(customer.LicenseNumber))
        {
            Console.WriteLine("Numer prawa jazdy jest wymagany.");
            return;
        }
        if (!customer.Email.Contains("@"))
        {
            Console.WriteLine("Nieprawidłowy adres email.");
            return;
        }
        if (customer.PhoneNumber.Length is not 9)
        {
            Console.WriteLine("Nieprawidłowy numer telefonu.");
            return;
        }

        customer.Id = _customers.Any() ? _customers.Max(c => c.Id) + 1 : 1;

        _customers.Add(customer);
        branch.Customers.Add(customer);

        Console.WriteLine($"\nDodano klienta: {customer.FirstName} {customer.LastName} do oddziału {branch.Name} {branch.City}");
    }

    public void RemoveCustomer(int customerId, Branch branch)
    {
        var customer = branch.Customers.FirstOrDefault(c => c.Id == customerId);
        if (customer is null)
        {
            Console.WriteLine("\nNie znaleziono klienta w tym oddziale.");
            return;
        }

        bool hasActiveRental = branch.Rentals.Any(r => r.Customer?.Id == customerId);
        if (hasActiveRental)
        {
            Console.WriteLine($"\nNie można usunąć klienta {customer.FirstName} {customer.LastName}, ponieważ posiada aktywne wypożyczenia.");
            return;
        }

        branch.Customers.Remove(customer);
        _customers.Remove(customer);

        Console.WriteLine($"\nUsunięto klienta {customer.FirstName} {customer.LastName} z oddziału {branch.Name} {branch.City}");
    }

    public bool HasCustomers(Branch branch)
    {
        return branch.Customers is not null && branch.Customers.Count > 0;
    }
}
