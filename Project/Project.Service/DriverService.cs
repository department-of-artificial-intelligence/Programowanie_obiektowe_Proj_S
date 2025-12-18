using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Service
{
    public class DriverService : CrudService<Driver>
    {
        public DriverService(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Driver> GetDriversWithVehicles()
        {
            return _context.Drivers
                .Include(d => d.AssignedVehicle)
                .ToList();
        }
    }
}
