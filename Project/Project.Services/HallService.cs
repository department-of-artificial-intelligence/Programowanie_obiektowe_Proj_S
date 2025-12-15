using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class HallService
{
    private readonly ApplicationDbContext _context;

    public HallService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Hall? GetHallById(int id)
    {
        return _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefault(h => h.HallId == id);
    }

    public Seat? CreateNewSeat(int rowNumber, int seatNumber, Hall hall)
    {
        if (rowNumber < 0 || seatNumber < 0 || hall is null) return null;

        Seat? seat = hall.CreateSeat(rowNumber, seatNumber);
        if (seat is null) return null;

        _context.Seats.Add(seat);
        _context.SaveChanges();

        return seat;
    }

    public bool CreateNewSeats(int rows, int seatsPerRow, Hall hall)
    {
        if (rows < 0 || seatsPerRow < 0 || hall is null) return false;

        List<Seat>? createdSeats = hall.CreateSeats(rows, seatsPerRow);

        if (createdSeats is null || createdSeats.Count == 0) return false;

        _context.Seats.AddRange(createdSeats);
        _context.SaveChanges();

        return true;
    }
}
