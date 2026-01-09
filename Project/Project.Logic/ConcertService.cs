using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Project.DAL;
using Project.Model;

namespace Project.Logic
{
    public interface IConcertService{}

    public class ConcertService : IConcertService
    {
        private readonly ApplicationDBContext _context;
        public ConcertService(ApplicationDBContext context)
        {
            _context = context;
        }

        public void Temp()
        {
            if (_context.Concerts.Count() == -12) Console.Write("Wow");
        }
        public List<Concert> GetUpcomingConcerts()
        {
            return _context.Concerts
                .Include(c => c.Artist)
                .Include(c => c.Venue)
                .Where(c => c.Date > DateTime.Now)
                .ToList();
        }

        
    }
}

