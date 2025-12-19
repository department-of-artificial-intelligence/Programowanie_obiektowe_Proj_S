using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services
{
    public class ManagerService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        private readonly PersonService _personService;

        public ManagerService(ApplicationDbContext applicationDbContext, PersonService personService)
        {
            this._applicationDbContext = applicationDbContext;
            this._personService = personService;
        }
        
        public async Task<bool> IsAnyManagerExistsAsync()
        {
            return await this._applicationDbContext.Managers.AnyAsync();
        }
        
        public async Task<List<Manager>> GetAllManagersAsync()
        {
            return await this._applicationDbContext.Managers
                .Include(x => x.Person)
                .ToListAsync();
        }
        
        public async Task<Manager> CreateManagerAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            var person = await this._personService.CreatePersonAsync(firstName, lastName, dateOfBirth);
            var manager = new Manager(person);
            
            await this._applicationDbContext.Managers.AddAsync(manager);
            await this._applicationDbContext.SaveChangesAsync();

            return manager;
        }
        
        public async Task FireManagerAsync(Manager manager)
        {
            this._applicationDbContext.Managers.Remove(manager);
            await this._applicationDbContext.SaveChangesAsync();
        }
    }
}