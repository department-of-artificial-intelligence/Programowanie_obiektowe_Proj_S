using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class TicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool AddNewTicket(Performance performance, decimal price, Seat seat)
    {
        if (performance is null || seat is null || price < 0) return false;

        Ticket? ticket = performance.CreateTicket(price, seat);
        if (ticket is null) return false;

        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        return true;
    }
}
