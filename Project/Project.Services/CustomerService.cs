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

    public List<Customer> GetAllCustomers()
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
}