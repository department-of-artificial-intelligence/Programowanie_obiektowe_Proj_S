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
    }
}

