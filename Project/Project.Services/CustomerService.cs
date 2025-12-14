using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using System.Numerics;

namespace Project.Services;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetCustomers()
    {
        return _context.Customers
            .AsSplitQuery()
            .Include(c => c.Tickets)
                .ThenInclude(t => t.Performance)
                    .ThenInclude(p => p.Play)
            .Include(c => c.Tickets)
                .ThenInclude(t => t.Performance)
                    .ThenInclude(p => p.Hall)
            .Include(c => c.Tickets)
                .ThenInclude(t => t.Seat)
            .ToList();
    }

    public bool AddNewCustomer(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)) return false;

        Customer customer = new Customer(firstName, lastName);

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return true;
    }

    public bool ReserveTicket(Customer customer, Ticket ticket)
    {
        if (customer is null || ticket is null) return false;
        if (customer.ReserveTicket(ticket))
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool CancelReservation(Customer customer, Ticket ticket)
    {
        if (customer is null || ticket is null) return false;
        if (customer.CancelReservation(ticket)) 
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool BuyTicket(Customer customer, Ticket ticket)
    {
        if (customer is null || ticket is null) return false;
        if (customer.BuyTicket(ticket))
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool RefundTicket(Customer customer, Ticket ticket)
    {
        if (customer is null || ticket is null) return false;
        if (customer.RefundTicket(ticket)) 
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool BuyAllReserved(Customer customer)
    {
        if (customer is null) return false;
        if (customer.BuyAllReserved())
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool CancelAllReserved(Customer customer)
    {
        if (customer is null) return false;
        if (customer.CancelAllReserved())
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool RefundAllBought(Customer customer)
    {
        if (customer is null) return false;
        if (customer.RefundAllBought())
        {
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}