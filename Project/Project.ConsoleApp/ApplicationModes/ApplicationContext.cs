using Project.DAL;
using Project.Services;

namespace Project.ConsoleApp.ApplicationModes
{
    public record ApplicationContext
    {
        public PersonService PersonService { get; private set; }
        
        public ManagerService ManagerService { get; private set; }
        
        public HotelService HotelService { get; private set; }

        public ApplicationContext(ApplicationDbContext dbContext)
        {
            this.PersonService = new PersonService(dbContext);
            this.ManagerService = new ManagerService(dbContext, this.PersonService);
            this.HotelService = new  HotelService(dbContext);
        }
    }
}