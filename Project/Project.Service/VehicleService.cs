using Project.DAL;
using Project.Model;

namespace Project.Service
{
    public class VehicleService : CrudService<Vehicle>
    {
        public VehicleService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
